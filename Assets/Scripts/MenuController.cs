using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(PlayGame);
    }

    public void PlayGame()
    {
        GameManager.Instance.PlayGame();
    }
}
