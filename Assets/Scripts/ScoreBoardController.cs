using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardController : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private NumberRenderer _highScoreRenderer;

    private void Awake()
    {
        _backButton.onClick.AddListener(GoToMenu);

        ShowHighScore();
    }

    public void GoToMenu()
    {
        GameManager.Instance.GoToMenu();
    }

    private void ShowHighScore()
    {
        if (PlayerPrefs.HasKey("SavedHighScore"))
        {
            _highScoreRenderer.DisplayNumber((int)PlayerPrefs.GetFloat("SavedHighScore"));
        }
        else
        {
            _highScoreRenderer.DisplayNumber(0);
        }
    }
}
