using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _scoreBoardButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(PlayGame);
        _scoreBoardButton.onClick.AddListener(ScoreBoard);
    }

    public void PlayGame()
    {
        GameManager.Instance.PlayGame();
    }

    public void ScoreBoard()
    {
        GameManager.Instance.ScoreBoard();
    }
}
