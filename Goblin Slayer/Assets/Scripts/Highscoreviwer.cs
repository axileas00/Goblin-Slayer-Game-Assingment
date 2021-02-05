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
        score = GlobalConstables.GetGlobalConstables().GetHighscore();
    }
    void Update()
    {
        if(score != GlobalConstables.GetGlobalConstables().GetHighscore())
        {
            score = GlobalConstables.GetGlobalConstables().GetHighscore();
            highScoreText.text = "Highscore: " + score.ToString(); 
        }
    }
}
