using System;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.OnSwitchToPlayingStateHandler += () =>
        {
            gameObject.SetActive(false);
        };
        GameManager.Instance.OnSwitchToVictoryStateHandler += () =>
        {
            gameObject.SetActive(true);
        };
    }
}