using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeBackground : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private List<Sprite> sprites;

    private Renderer targetRenderer;
    private SpriteRenderer spriteRenderer;

    private float duration;
    private float timer;
    private bool changeBackground;
    private bool cycleCompleted = true;
    private int step = 1;
    private int nextSpriteIndex = 1;

    private void Awake()
    {
        timer = 0f;
        duration = 2f;
        targetRenderer = GetComponent<Renderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (gameController.currentScore != 0 && gameController.currentScore % 20 == 0 && cycleCompleted)
        {
            changeBackground = true;
            cycleCompleted = false;
        }

        if (changeBackground)
        {
            Transition();
        }
    }

    private void Transition()
    {
        timer += Time.deltaTime / duration;

        if (step == 1)
        {
            // Step 1: Transition from white to black
            targetRenderer.material.color = Color.Lerp(Color.white, new Color(0f, 0.53f, 0.58f), timer);

            if (timer >= 1f)
            {
                timer = 0f;
                step = 2;
            }
        }
        else if (step == 2)
        {
            // Step 2: Change sprite
            ChangeSprite();
            timer = 0f;
            step = 3;
        }
        else if (step == 3)
        {
            // Step 3: Transition from black to white
            targetRenderer.material.color = Color.Lerp(new Color(0f, 0.53f, 0.58f), Color.white, timer);

            if (timer >= 1f)
            {
                timer = 0f;
                step = 1;
                changeBackground = false;
                cycleCompleted = true;
            }
        }
    }

    private void ChangeSprite()
    {
        if (spriteRenderer != null && sprites != null)
        {
            if (nextSpriteIndex == 1)
            {
                spriteRenderer.sprite = sprites[nextSpriteIndex];
                nextSpriteIndex--;
            }
            else
            {
                spriteRenderer.sprite = sprites[nextSpriteIndex];
                nextSpriteIndex++;
            }
        }
    }
}
