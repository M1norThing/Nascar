using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TMP_Text textField;
    [SerializeField] float scoreMultiplayer;

    public const string highScoreKey = "HighScore";

    float score;
 
    void Update()
    {
        score += Time.deltaTime * scoreMultiplayer;
        textField.text = Mathf.FloorToInt(score).ToString();
    }
    private void OnDestroy()
    {
        int currentScore = PlayerPrefs.GetInt(highScoreKey, 0);
        if (score > currentScore)
        {
            PlayerPrefs.SetInt(highScoreKey, Mathf.FloorToInt(score));
        }


    }
}
