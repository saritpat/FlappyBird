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

    private string defaultName = "FlappyBird";
    private int defaultScore = 0;

    public void SetHighScore(int score, string name)
    {
        for (int i = 0;i < scores.Count;i++)
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

    private void LoadScores()
    {
        scores.Clear();
        scoreNames.Clear();

        for(int i = 0;i < 10;i++)
        {
            scoreList = "Score" + i;
            scoreNameList = "ScoreName" + i;
            scores.Add(PlayerPrefs.GetInt(scoreList));
            scoreNames.Add(PlayerPrefs.GetString(scoreNameList));
        }
    }

    private void CheckNoscores()
    {
        if(scores.Count == 0)
        {
            for(int i = 0;i < 10;i++)
            {
                //defaultScore -= i;
                scoreList = "Score" + i;
                scoreNameList = "scoreName" + i;
                PlayerPrefs.SetInt(scoreList, defaultScore);
                PlayerPrefs.SetString(scoreNameList, defaultName);
            }
        }
    }

    public int CheckScore(int score)
    {
        LoadScores();
        highScoreToSet = 99;

        for(int i = 0;i < scores.Count; i++)
        {
            if (score > scores[i])
            {
                highScoreToSet = i;
                i = 10;
            }
        }

        return highScoreToSet;
    }
}
