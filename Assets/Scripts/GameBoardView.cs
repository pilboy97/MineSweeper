using System;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardView : MonoBehaviour, IGameBoardView
{
    private GameBoardPresenter _gameBoardPresenter;
    private ICellView[,] _cellViews;

    private void Awake()
    {
        _gameBoardPresenter = new GameBoardPresenter(this);

        var cellList = new List<ICellView>();

        int cnt = 0;
        foreach (Transform child in transform)
        {
            var iCellView = child.GetComponent<ICellView>();
            cnt++;
            
            cellList.Add(iCellView);
        }
        
        _cellViews = new ICellView[GameManager.GridRow, GameManager.GridCol];

        for (int i = 0; i < cellList.Count; i++)
        {
            _cellViews[i / GameManager.GridCol, i % GameManager.GridCol] = cellList[i];
        }
        
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < GameManager.GridRow; i++)
        {
            for (int j = 0; j < GameManager.GridCol; j++)
            {
                _cellViews[i, j].Init(_gameBoardPresenter, i, j);
            }
        }
    }

    public void SetNumber(int row, int col, int number)
    {
        _cellViews[row, col].SetNumber(number);
    }

    public void SetColor(int row, int col, Color color)
    {
        _cellViews[row, col].SetColor(color);
    }

    public void SetShowNumber(int row, int col, bool show)
    {
        _cellViews[row, col].SetShowNumber(show);
    }
}