using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private AudioSource changeScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            changeScene = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayGame()
    {
        changeScene.Play();
        SceneManager.LoadScene("Gameplay");
    }

    public void ScoreBoard()
    {
        changeScene.Play();
        SceneManager.LoadScene("ScoreBoard");
    }

    public void GoToMenu()
    {
        changeScene.Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        changeScene.Play();

        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
