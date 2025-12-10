
using UnityEngine;

public interface IGameBoardView
{
    void Init();
    void SetNumber(int row, int col, int number);
    void SetColor(int row, int col, Color color);
    void SetShowNumber(int row, int col, bool show);
}