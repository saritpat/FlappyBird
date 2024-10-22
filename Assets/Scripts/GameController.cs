using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{   
    [SerializeField] private Rigidbody2D _pipeUp;
    [SerializeField] private Rigidbody2D _pipeDown;
    [SerializeField] private PipeSpawner _pipeSpawner;

    [SerializeField] private GameObject _restartSceen;
    [SerializeField] public Button _restartButton;
    [SerializeField] public Button _returnToMenuButton;

    public FlappyBird flappyBird;

    private bool gameStart;
    public bool gameEnd;

    private int score;

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
    }

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && gameEnd == false)
        {
            StartGame();
        }

        if (gameStart && !gameEnd)
        {
            ScoreCount();
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
        _restartSceen.SetActive(false);
    }

    public void ScoreCount()
    {
        //
        //if (flappyBird.transform.position.x == _pipeUp.transform.position.x)
        //{
        //    score += 1;
        //    Debug.Log(score);
        //}
    }

    public void EndGame()
    {
        if (gameEnd == false)
        {
            gameEnd = true;
            _restartSceen.SetActive(true);

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
