using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginGame : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }
    public void OnBegin()
    {
        SceneManager.LoadScene(1);
        GlobalConstables.GetGlobalConstables().SetHighscoreTo(0);
    }

    public void EndCondition()
    {
        SceneManager.LoadScene(0);
    }
    public void OnExit()
    {
        Application.Quit();
    }

   
}
