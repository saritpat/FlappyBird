using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Game object")]
    [SerializeField] private Rigidbody2D _pipeUp;
    [SerializeField] private Rigidbody2D _pipeDown;
    [SerializeField] private PipeSpawner pipeSpawner;

    [Header("UI")]
    [SerializeField] private GameObject restartScreen;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button returnToMenuButton;

    [Header("Bird")]
    [SerializeField] private FlappyBird flappyBird;

    public bool gameStart;
    public bool gameEnd;

    public float currentScore;
    public NumberRenderer currentScoreRenderer;
    public NumberRenderer highScoreRenderer;

    private float pipeSpeed;
    private float x;
    private float y;
    private Vector2 spawnPosition;
    private Vector2 gap;
    private HashSet<Rigidbody2D> activePipes = new HashSet<Rigidbody2D>();
    private HashSet<Rigidbody2D> passedPipes = new HashSet<Rigidbody2D>();

    private AudioSource[] audioGame;

    private void Awake()
    {
        pipeSpeed = -5f;
        gap = new Vector2(0, 10f);

        restartButton.onClick.AddListener(RestartGame);
        returnToMenuButton.onClick.AddListener(GoToMenu);

        // render start score
        currentScoreRenderer.DisplayNumber(0);

        if (PlayerPrefs.HasKey("SavedHighScore"))
        {
            highScoreRenderer.DisplayNumber((int)PlayerPrefs.GetFloat("SavedHighScore"));
        }
        else
        {
            highScoreRenderer.DisplayNumber(0);
        }

        audioGame = GetComponents<AudioSource>();
    }

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && gameEnd == false)
        {
            StartGame();
        }

        if (gameStart && !gameEnd)
        {
            CheckPipePass();
        }
    }

    #region GameState
    private void StartGame()
    {
        if (gameStart == false)
        {
            StartCoroutine(SpawnPipe());
        }

        gameStart = true;
        restartScreen.SetActive(false);
    }



    public void EndGame()
    {
        if (gameEnd == false)
        {
            gameEnd = true;
            restartScreen.SetActive(true);

            // stop pipe
            StopAllPipes();

            HighScoreUpdate();

            // play sound "die"
            audioGame[1].Play();
        }

    }

    public void GoToMenu()
    {
        GameManager.Instance.GoToMenu();
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    #endregion

    #region Score
    private void CheckPipePass()
    {
        foreach (Rigidbody2D pipe in activePipes)
        {
            // Check if FlappyBird has passed the pipe and the pipe is not already scored
            if (pipe != null && flappyBird.transform.position.x > pipe.transform.position.x && !passedPipes.Contains(pipe))
            {
                currentScore+=0.5f;
                // Mark this pipe as passed
                passedPipes.Add(pipe);

                // render score
                currentScoreRenderer.DisplayNumber((int)currentScore);

                // play sound "point"
                audioGame[0].Play();
            }
        }
    }

    private void HighScoreUpdate()
    {
        // Check exist highscore
        if (PlayerPrefs.HasKey("SavedHighScore"))
        {
            if (currentScore > PlayerPrefs.GetFloat("SavedHighScore"))
            {
                PlayerPrefs.SetFloat("SavedHighScore", currentScore);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("SavedHighScore", currentScore);
        }
    }

    #endregion

    #region Pipe
    private void RandomPosition()
    {
        x = pipeSpawner.transform.position.x;
        y = Random.Range(2.5f, 7.5f);
        //y = Random.Range(100f, 101f);

        spawnPosition = new Vector2(x, y);
    }

    private IEnumerator SpawnPipe()
    {
        while (!gameEnd)
        {
            RandomPosition();
            Rigidbody2D pipeUp = Instantiate(_pipeUp, spawnPosition, Quaternion.identity);
            pipeUp.velocity = new Vector2(pipeSpeed, 0);
            activePipes.Add(pipeUp);

            Rigidbody2D pipeDown = Instantiate(_pipeDown, spawnPosition - gap, Quaternion.identity);
            pipeDown.velocity = new Vector2(pipeSpeed, 0);
            activePipes.Add(pipeDown);

            yield return new WaitForSeconds(1f);
        }
    }

    private void StopAllPipes()
    {
        foreach (Rigidbody2D pipe in activePipes)
        {
            if (pipe != null)
            {
                pipe.velocity = Vector2.zero;
            }
        }
    }

    #endregion
}
