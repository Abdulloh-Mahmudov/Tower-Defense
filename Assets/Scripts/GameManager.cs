using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        SceneManager.LoadScene(0);
    }
}
