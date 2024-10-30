using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private List<Text> _highScores;

    private List<int> scores = new List<int>();
    private List<string> scoreNames = new List<string>();

    private int tempscore;
    private int highScoreToSet;
    private string tempName;
    private string scoreList;
    private string scoreNameList;

    public void SetHighScore(int score, string name)
    {
        for (int i = 0;i < scores.Count; i++)
        {
            if(highScoreToSet <= 1)
            {
                tempName = scoreNames[i];
                scoreNames[i] = name;
                name = tempName;

                tempscore = scores[i];
                scores[i] = score;
                score = tempscore;
                scoreList = "Score" + i;
                scoreNameList = "Scorename" + i;
                PlayerPrefs.SetInt(scoreList, 1);
                PlayerPrefs.SetString(scoreNameList, scoreNames[i]);
            }
        }
    }
}
