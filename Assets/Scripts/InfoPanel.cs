using System;
using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI leftBombText;
    [SerializeField] TextMeshProUGUI timerText;

    private void LateUpdate()
    {
        leftBombText.text = (GameManager.BombNum - GameManager.Instance.foundBomb).ToString();
        timerText.text = ((int)GameManager.Instance.timePast).ToString();
    }
}
