using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour {

    public IntObject Score;

    int lastScore;

    private void Start()
    {
        Score.OnChanged += UpdateResponse;
    }

    void UpdateResponse()
    {
        // positive feedback
        if (Score.Value > lastScore)
        {
            
        }

        // negative feedback
        else
        {
            
        }

        lastScore = Score.Value;
    }
}
