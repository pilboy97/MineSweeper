using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting.Dependencies.NCalc;

public class GameBoardPresenter
{
    private readonly GameBoardModel _gameBoardModel = new();
    private readonly GameBoardView _gameBoardView;

    public GameBoardPresenter(GameBoardView gameBoardView)
    {
        _gameBoardView = gameBoardView;

        _gameBoardModel.OnUpdateGameBoard += RefreshView;
        GameManager.Instance.OnSwitchToPlayingStateHandler += _gameBoardModel.InitBoard;
    }

    public void HandleLeftClick(int row, int col)
    {
        var cell = _gameBoardModel.GetCell(row, col);

        if (cell.Value == -1)
        {
            if (GameManager.Instance.cheatMode)
            {
                Debug.Log("This is Bomb");
                return;
            }

            GameManager.Instance.Failed();
            return;
        }

        _gameBoardModel.SetCellShow(row, col, true);
        if (cell.Value == 0)
        {
            ShowZeroArea(cell.Pos.x, cell.Pos.y);
        }
        
        RefreshView();
    }

    void ShowZeroArea(int r, int c)
    {
        Queue<Vector2Int> queue = new();
        HashSet<Vector2Int> set = new();
        
        Vector2Int[] dir = new Vector2Int[]
        {
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            new Vector2Int(1, 1),
            new Vector2Int(1, 0),
            new Vector2Int(1, -1),
            new Vector2Int(-1, 1),
            new Vector2Int(-1, 0),
            new Vector2Int(-1, -1),
        };
        
        var start = new Vector2Int(r, c);
        
        queue.Enqueue(start);
        set.Add(start);

        while (queue.Count > 0)
        {
            var front =  queue.Dequeue();

            for (int i = 0; i < dir.Length; i++)
            {
                var _r = front.x + dir[i].x;
                var _c = front.y + dir[i].y;
                
                var p = new Vector2Int(_r, _c);
                
                if (!(0 <= _r && _r < GameManager.GridRow)) continue;
                if (!(0 <= _c && _c < GameManager.GridCol)) continue;
                
                if (set.Contains(p))  continue;
                
                set.Add(p);
                
                if (_gameBoardModel.GetCell(_r, _c).Value != 0) continue;
                queue.Enqueue(p);
            }
        }

        foreach (var v in set)
        {
            _gameBoardModel.SetCellShow(v.x, v.y, true);
        }
        
        RefreshView();
    }

    public void HandleRightClick(int row, int col)
    {
        var cell = _gameBoardModel.GetCell(row, col);
        if (cell.Show) return;

        var foundBomb = GameManager.Instance.foundBomb;
        GameManager.Instance.foundBomb = (!cell.IsFlagged) ? foundBomb + 1 : foundBomb - 1;
        _gameBoardModel.SetCellFlagged(row, col, !cell.IsFlagged);
        
        RefreshView();
    }

    void RefreshView()
    {
        for (int i = 0; i < GameManager.GridRow; i++)
        {
            for (int j = 0; j < GameManager.GridCol; j++)
            {
                var cell = _gameBoardModel.GetCell(i, j);
                if (cell.IsFlagged)
                {
                    _gameBoardView.SetColor(i, j, Color.green);
                    _gameBoardView.SetShowNumber(i, j, false);
                    continue;
                }

                if (cell.Show)
                {
                    _gameBoardView.SetShowNumber(i, j, true);
                    _gameBoardView.SetNumber(i, j, cell.Value);
                    _gameBoardView.SetColor(i, j, Color.white);

                    continue;
                }

                _gameBoardView.SetColor(i, j, Color.white);
                _gameBoardView.SetShowNumber(i, j, false);
            }
        }
    }

    void HandleGameBoardUpdate()
    {
        RefreshView();
    }
}
