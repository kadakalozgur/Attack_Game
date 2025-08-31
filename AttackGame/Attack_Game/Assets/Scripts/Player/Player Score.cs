using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{

    public TMP_Text scoreText;

    public int score = 0;
    public void addScore(int points)
    {

        score += points;

        updateScoreText();

    }

    private void updateScoreText()
    {

        scoreText.text = "Score : " + score;

    }

}
