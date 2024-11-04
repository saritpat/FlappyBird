using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlappyBird : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator fly;

    private float jumpHeight;
    private float gravityScale;
    private float fallingGravityScale;

    private float jumpForce;
    private bool isCollision;
    
    private AudioSource[] audioBird;

    private void Awake()
    {
        jumpHeight = 10f;
        gravityScale = 6f;
        fallingGravityScale = 3f;
        rb.bodyType = RigidbodyType2D.Kinematic;

        jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));

        audioBird = GetComponents<AudioSource>();
    }

    private void Update()
    {
        // Jumping
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && gameController.gameEnd == false)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;

            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            transform.eulerAngles = Vector3.forward * 30;

            // play sound "swing"
            audioBird[0].Play();
        }

        // Set gravity scale and Rotate
        if (rb.velocity.y >= 0)
        {
            rb.gravityScale = gravityScale;
        }
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallingGravityScale;
        }

        RotateFlappyBird();

        // Check collision
        if (isCollision)
        {
            gameController.EndGame();
            StopBird();
        }
    }

    private void RotateFlappyBird()
    {
        if (rb.velocity.y >= 0)
        {
            transform.eulerAngles = rb.velocity.y * 2 * Vector3.forward;
        }
        else if (rb.velocity.y < 0)
        {
            transform.eulerAngles = rb.velocity.y * -4 * Vector3.back;
        }
    }

    private void StopBird()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // play sound "hit"
        audioBird[1].Play();
        fly.enabled = false;

        isCollision = true;
    }
}
