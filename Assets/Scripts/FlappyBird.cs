using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlappyBird : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [Header("Jumper")]
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _fallingGravityScale;

    private float jumpForce;

    private void Awake()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;

        jumpForce = Mathf.Sqrt(_jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {  
            rb.bodyType = RigidbodyType2D.Dynamic;

            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            transform.eulerAngles = Vector3.forward * 30;
        }

        if (rb.velocity.y >= 0)
        {
            rb.gravityScale = _gravityScale;
        }
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = _fallingGravityScale;
        }

        if (rb.velocity.y <= -10)
        {
            transform.eulerAngles = Vector3.back * 60;
        }
    }
}
