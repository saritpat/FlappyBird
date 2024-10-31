using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberRenderer : MonoBehaviour
{
    // Prefab with SpriteRenderer for each digit
    public GameObject digitPrefab;
    // Array to hold digit sprites from 0 to 9
    public Sprite[] numberSprites; 
    // List of active digit GameObjects
    private List<GameObject> digits = new List<GameObject>(); 

    // Method to display a number
    public void DisplayNumber(int number)
    {
        ClearDigits();

        string numberStr = number.ToString();
        // Adjust based on sprite size
        float digitWidth = 0.25f;

        for (int i = 0; i < numberStr.Length; i++)
        {
            int digit = numberStr[i] - '0';
            GameObject newDigit = Instantiate(digitPrefab, transform);
            newDigit.GetComponent<SpriteRenderer>().sprite = numberSprites[digit];

            // Set the position of each digit based on its order
            newDigit.transform.localPosition = new Vector3(i * digitWidth, 0, 0);
            digits.Add(newDigit);
        }
    }

    // Method to clear existing digits
    private void ClearDigits()
    {
        foreach (GameObject digit in digits)
        {
            Destroy(digit);
        }
        digits.Clear();
    }
}
