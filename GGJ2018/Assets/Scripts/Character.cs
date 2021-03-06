﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    public IntObject Score;

	public List<ConditionObject> conditions;

    public Image Photo;

	// public Text BubbleTalk;

	public TalkObject Talk;

	[SerializeField]
	TextAsset GoodResponsesAsset = null;

	[SerializeField]
	TextAsset BadResponsesAsset = null;

	[SerializeField]
	TextAsset NeutralResponsesAsset = null;

	List<string> badResponses = null;

	List<string> goodResponses = null;

    List<string> neutralResponses = null;

    int lastScore;

    private void Awake()
    {
        LoadDate();
    }

    private void Start()
    {
        Score.OnSubmitted += UpdateResponse;
    }

    void LoadDate()
	{
		TextAssetLoader.GetWords(ref BadResponsesAsset, ref badResponses);
		TextAssetLoader.GetWords(ref GoodResponsesAsset, ref goodResponses);
        TextAssetLoader.GetWords(ref NeutralResponsesAsset, ref neutralResponses);

		// check load results
		Debug.Log("bad responses # = " + badResponses.Count);
		Debug.Log("good responses # = " + goodResponses.Count);
		Debug.Log("neutral responses # = " + neutralResponses.Count);
    }

    void UpdateResponse()
    {
        string response = ""; 

        // positive feedback
        if (Score.Value > lastScore)
        {
            if (goodResponses.Count > 0)
            {
                response = goodResponses[UnityEngine.Random.Range(0, goodResponses.Count)];
            }
        }

        // negative feedback
        else if (Score.Value < lastScore)
		{
			if (badResponses.Count > 0)
			{
				response = badResponses[UnityEngine.Random.Range(0, badResponses.Count)];
			}
        }

        // neutral feedback
        else
        {
			if (neutralResponses.Count > 0)
			{
				response = neutralResponses[UnityEngine.Random.Range(0, neutralResponses.Count)];
			}
        }

        // BubbleTalk.text = response;
        Talk.CreateTalk(response, TalkObject.Side.Right);

        lastScore = Score.Value;

        UpdateConditions();
    }

    void UpdateConditions()
    {
        if (conditions.Count > 0)
        {
            foreach (ConditionObject condition in conditions)
            {
                if (Score.Value >= condition.min && Score.Value <= condition.max)
                {
                    Photo.sprite = condition.image;
                    break;
                }
            }
        }
    }
}
