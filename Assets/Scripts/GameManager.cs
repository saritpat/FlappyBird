using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Rigidbody2D _pipeUp;
    [SerializeField] public Rigidbody2D _pipeDown;
    [SerializeField] private PipeSpawner _pipeSpawner;

    private bool gameStart;
    private float pipeSpeed;
    private float x;
    private float y;
    private Vector2 spawnPosition;
    private Vector2 gap;


    private void Awake()
    {
        pipeSpeed = -5f;
        gap = new Vector2(0, 10);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            IsGameStart();
        }
    }

    #region GameState
    private void IsGameStart()
    {
        if (gameStart == false)
        {
            StartCoroutine(SpawnPipe());
        }
        gameStart = true;
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
        while (true)
        {
            RandomPosition();
            Rigidbody2D pipeUp = Instantiate(_pipeUp, spawnPosition, Quaternion.identity);
            pipeUp.velocity = new Vector2(pipeSpeed, 0);

            Rigidbody2D pipeDown = Instantiate(_pipeDown, spawnPosition - gap, Quaternion.identity);
            pipeDown.velocity = new Vector2(pipeSpeed, 0);
            yield return new WaitForSeconds(1f);
        }
    }

    private void DestroyPipe()
    {
        // if pipe.position.x < camera.position.x -> Destroy pipe
    }

    #endregion
}
