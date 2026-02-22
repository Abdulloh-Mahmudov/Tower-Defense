using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ResumeTime();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }
    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }
    public void SpeedUpTime()
    {
        Time.timeScale = 2f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        StopTime();
        UI_Manager.Instance.GameOver();
    }

    public void GameWon()
    {
        StopTime();
        UI_Manager.Instance.GameWon();
        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
