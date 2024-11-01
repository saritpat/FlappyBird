using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{   
    [SerializeField] private Rigidbody2D _pipeUp;
    [SerializeField] private Rigidbody2D _pipeDown;
    [SerializeField] private PipeSpawner _pipeSpawner;

    [SerializeField] private GameObject _restartScreen;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _returnToMenuButton;

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

    private void Awake()
    {
        pipeSpeed = -5f;
        gap = new Vector2(0, 9.5f);

        _restartButton.onClick.AddListener(RestartGame);
        _returnToMenuButton.onClick.AddListener(GoToMenu);

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
        _restartScreen.SetActive(false);
    }



    public void EndGame()
    {
        if (gameEnd == false)
        {
            gameEnd = true;
            _restartScreen.SetActive(true);

            // stop pipe
            StopAllPipes();

            HighScoreUpdate();
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
        x = _pipeSpawner.transform.position.x;
        //y = Random.Range(2.5f, 7.5f);
        y = Random.Range(100f, 101f);

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
