using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float scrollSpeed = 2f;  // Speed at which the ground scrolls
    public float tileSizeX;         // Width of one tile of the ground

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        // Calculate tile size based on the renderer bounds (useful for adjustments)
        tileSizeX = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Move the ground to the left by scrollSpeed over time
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeX);
        transform.position = startPosition + Vector3.left * newPosition;
    }
}
