using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreShmup : MonoBehaviour
{
    public GameObject score;
    public GameObject highScore;
    public int scoreValue = 0;

    void Start()
    {
        //GameObject highScore = GameObject.Find("HighScoreTXT");
        //FEATURE POINT: PlayerPrefs : used to save highscore
        //FEATURE POINT: Scoring System : keeps track of points player earns
        highScore.GetComponent<TextMeshProUGUI>().text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        //score = GetComponent<text>();
    }
    void update()
    {
        //score.text = scoreValue;
    }
    public void HitEnemy()
    {
        //GameObject highScore = GameObject.Find("HighScoreTXT");
        int number = Random.Range(1, 7);
        score.GetComponent<TextMeshProUGUI>().text = number.ToString();

        if (number > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", number);
            highScore.GetComponent<TextMeshProUGUI>().text = "High Score: " + number.ToString();
        }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey("HighScore");
        //GameObject highScore = GameObject.Find("HighScoreTXT");
        highScore.GetComponent<TextMeshProUGUI>().text = "0";
    }
}
