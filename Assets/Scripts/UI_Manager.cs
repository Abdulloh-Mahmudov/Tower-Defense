using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Text _funds;
    [SerializeField] private Text _wave;
    [SerializeField] private Text _lives;

    public void UpdateFunds(int funds)
    {
       _funds.text = funds.ToString();
    }

    public void UpdateWaves(int current, int max)
    {
        _wave.text = current + "/" + max;
    }

    public void UpdateLives(int lives)
    {
        _lives.text = lives.ToString();
    }
}
