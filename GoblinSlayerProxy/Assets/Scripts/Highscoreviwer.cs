using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscoreviwer : MonoBehaviour
{
    [SerializeField] Text highScoreText;

    [SerializeField]int score;

    private void Start()
    {
        score = GlobalConstables.Highscore;
    }
    void Update()
    {
        if(score != GlobalConstables.Highscore)
        {
            score = GlobalConstables.Highscore;
            highScoreText.text = "Highscore: " + score.ToString(); 
        }
    }
}
