
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameBoardModel
{
    private readonly Cell[,] _cells = new Cell[GameManager.GridRow, GameManager.GridCol];

    public Action OnUpdateGameBoard = () => { };

    public GameBoardModel()
    {
        OnUpdateGameBoard += EndGame;
    }
    
    public Cell GetCell(int row, int col)
    {
        return _cells[row, col];
    }

    public void SetCell(int row, int col, Cell cell)
    {
        _cells[row, col] = cell;
        OnUpdateGameBoard();
    }

    public void SetCellShow(int row, int col, bool show)
    {
        _cells[row, col].Show = show;
        OnUpdateGameBoard();
    }

    public void SetCellFlagged(int row, int col, bool flag)
    {
        _cells[row, col].IsFlagged = flag;
        OnUpdateGameBoard();
    }
    
    public void InitBoard()
    {
        Debug.Log("initBoard");
        for (int i = 0; i < GameManager.GridRow; i++)
        {
            for (int j = 0; j < GameManager.GridCol; j++)
            {
                _cells[i, j] = new();
                _cells[i, j].Pos = new Vector2Int(i, j);
            }
        }

        for (int i = 0; i < GameManager.BombNum; i++)
        {
            do
            {
                int r, c;
                
                r = Random.Range(0, GameManager.GridRow);
                c = Random.Range(0, GameManager.GridCol);
                
                if (_cells[r,c].Value == -1) continue;
                
                _cells[r, c].Value = -1;
            } while (false);
        }

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

        for (int i = 0; i < GameManager.GridRow; i++)
        {
            for (int j = 0; j < GameManager.GridCol; j++)
            {
                if (_cells[i, j].Value == -1) continue;
                
                int cnt = 0;

                for (int k = 0; k < dir.Length; k++)
                {   
                    var r = i +  dir[k].x;
                    var c = j + dir[k].y;
                    
                    if (!(0 <= r && r < GameManager.GridRow)) continue;
                    if (!(0 <= c && c < GameManager.GridCol)) continue;

                    if (_cells[r, c].Value == -1) cnt++;
                }
                
                _cells[i, j].Value = cnt;
            }
        }

        OnUpdateGameBoard();
    }

    void EndGame()
    {
        if (GameManager.Instance.foundBomb == GameManager.BombNum)
        {
            for (int i = 0; i < GameManager.GridRow; i++)
            {
                for (int j = 0; j < GameManager.GridCol; j++)
                {
                    if (_cells[i, j].Value == -1 && !_cells[i, j].IsFlagged)
                    {
                        GameManager.Instance.Failed();
                        return;
                    }
                }
            }
            GameManager.Instance.Victory();
            return;
        }

        int cnt = 0;

        for (int i = 0; i < GameManager.GridRow; i++)
        {
            for (int j = 0; j < GameManager.GridCol; j++)
            {
                if (!_cells[i, j].Show)
                    cnt++;
            }
        }

        if (cnt != GameManager.BombNum) return;
        
        GameManager.Instance.Victory();
    }
}