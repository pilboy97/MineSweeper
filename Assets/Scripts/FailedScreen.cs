using System;
using UnityEngine;

public class FailedScreen : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.OnSwitchToPlayingStateHandler += () =>
        {
            gameObject.SetActive(false);
        };
        GameManager.Instance.OnSwitchToFailedStateHandler += () =>
        {
            gameObject.SetActive(true);
        };
    }
}