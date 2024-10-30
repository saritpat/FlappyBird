using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
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

    private float score = 0f;
    public NumberRenderer numberRenderer;

    private HashSet<Rigidbody2D> passedPipes = new HashSet<Rigidbody2D>();

    private float pipeSpeed;
    private float x;
    private float y;
    private Vector2 spawnPosition;
    private Vector2 gap;
    private List<Rigidbody2D> activePipes = new List<Rigidbody2D>();

    private void Awake()
    {
        pipeSpeed = -5f;
        gap = new Vector2(0, 10);

        _restartButton.onClick.AddListener(RestartGame);
        _returnToMenuButton.onClick.AddListener(GoToMenu);

        // render start score
        numberRenderer.DisplayNumber(0);
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
                score+=0.5f;
                Debug.Log("Score: " + score);
                // Mark this pipe as passed
                passedPipes.Add(pipe);

                // render score
                numberRenderer.DisplayNumber((int)score);
            }
        }
    }

    #endregion

    #region Pipe
    private void RandomPosition()
    {
        x = _pipeSpawner.transform.position.x;
        y = Random.Range(3f, 7f);

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
