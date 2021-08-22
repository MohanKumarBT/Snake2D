using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI Snake1ScoreTxt;
   
    [SerializeField] public float Score1 = 0;
   

    
    void Update()
    {
        UpdateUI();
    }

    public void Score1Increment(float score)
    {
        Score1 += score;
        UpdateUI();
    }

   

    public void Score1Decrement(float score)
    {
        Score1 -= score;
        if (Score1 < 0)
        {
            Score1 = 0;
        }
        UpdateUI();
    }

  

    public void Score1Double(float score)
    {
        score *= 2;
        Score1 = score;
        UpdateUI();
    }

    

    public float WhatIsScore1()
    {
        return Score1;
    }

   

    private void UpdateUI()
    {
        Snake1ScoreTxt.text = " " + Score1;
        
    }
}
