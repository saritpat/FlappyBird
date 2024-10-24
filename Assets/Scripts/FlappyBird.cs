using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlappyBird : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private GameController gameController;

    private float jumpHeight;
    private float gravityScale;
    private float fallingGravityScale;

    private float jumpForce;
    private bool isCollision;

    private void Awake()
    {
        jumpHeight = 10f;
        gravityScale = 6f;
        fallingGravityScale = 3f;
        _rb.bodyType = RigidbodyType2D.Kinematic;

        jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * _rb.gravityScale));
    }

    private void Update()
    {
        // Jumping
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && gameController.gameEnd == false)
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;

            _rb.velocity = Vector2.zero;
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            transform.eulerAngles = Vector3.forward * 30;
        }

        // Set gravity scale and Rotate
        if (_rb.velocity.y >= 0)
        {
            _rb.gravityScale = gravityScale;
        }
        else if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = fallingGravityScale;
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
        if (_rb.velocity.y >= 0)
        {
            transform.eulerAngles = _rb.velocity.y * 2 * Vector3.forward;
        }
        else if (_rb.velocity.y < 0)
        {
            transform.eulerAngles = _rb.velocity.y * -4 * Vector3.back;
        }
    }

    private void StopBird()
    {
        _rb.bodyType = RigidbodyType2D.Static;
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isCollision = true;
    }
}
