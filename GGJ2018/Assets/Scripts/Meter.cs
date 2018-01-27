using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Meter : MonoBehaviour {

    public RectTransform NeedleRect;

    public IntObject Score;

    public int Max = 200;

    public int Min = -200;

    float rate = 1.0f;

    private void Start()
    {
        Score.OnChanged += UpdateScore;
        Score.OnResetted += Reset;
    }

    public void SetRate(int min, int max)
    {
        rate = (Max - Min) / (max - min);
    }

    public void UpdateScore()
    {
        float moveDist = Score.Value * rate;
        if (moveDist >= Min && moveDist <= Max)
		{
            NeedleRect.anchoredPosition = new Vector2(NeedleRect.anchoredPosition.x, moveDist);
        }
    }

    public void Reset()
    {
        NeedleRect.anchoredPosition = new Vector2(NeedleRect.anchoredPosition.x, 0);
    }
}
