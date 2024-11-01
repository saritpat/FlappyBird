using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    private readonly float scrollSpeed = 4f;
    private float tileSize;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        // Calculate tile size based on the renderer bounds
        tileSize = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        if (!gameController.gameEnd)
        {
            MoveTiles();
        }
    }

    private void MoveTiles()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
        transform.position = startPosition + Vector3.left * newPosition;
    }
}
