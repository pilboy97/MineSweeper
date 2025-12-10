using UnityEngine;

public interface ICellView
{
    void Init(GameBoardPresenter presenter, int row, int col);
    void SetNumber(int number);
    void SetColor(Color color);
    void SetShowNumber(bool show);
}