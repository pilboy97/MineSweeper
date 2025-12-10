
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellView : MonoBehaviour, ICellView, IPointerDownHandler
{
    [SerializeField] TextMeshProUGUI numberText;
    [SerializeField] Image image;
    [SerializeField] Vector2Int pos;

    private GameBoardPresenter _presenter;
    
    public void Init(GameBoardPresenter presenter, int row, int col)
    {
        _presenter = presenter;
        pos = new Vector2Int(row, col);
        
        SetColor(Color.white);
        numberText.enabled = false;
    }

    public void SetNumber(int number)
    {
        numberText.text = number.ToString();
    }

    public void SetColor(Color color)
    {
        image.color = color;
    }

    public void SetShowNumber(bool show)
    {
        numberText.enabled = show;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            _presenter.HandleRightClick(pos.x, pos.y);
            return;
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _presenter.HandleLeftClick(pos.x, pos.y);
        }
    }
}