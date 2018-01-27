using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public InputField TextInputField;

    public Meter AffectionMeter;

    public IntObject Score;

    public TalkObject Talk;

    public GameObject FullScreen;

	public Sprite StartImage;
    public Sprite LossImage;
    public Sprite WinImage;

	[SerializeField]
	TextAsset BadWordsAsset = null;

	[SerializeField]
	TextAsset GoodWordsAsset = null;

    List<string> badWords = null;

    List<string> goodWords = null;

    const int ADD_BAD = -10;
    const int ADD_GOOD = 10;
    const int MAX = 100;
    const int MIN = -100;

	void Awake()
	{
		FullScreen.GetComponentInChildren<Image>().sprite = StartImage;
		LoadData();
        SetMeter();
		InitGame();
	}

    private void OnApplicationQuit()
    {
        InitGame();
    }

    void Update()
	{
		CheckInput();
	}

    void InitGame()
    {
        Score.Reset();
        Talk.Reset();
    }

    void LoadData()
    {
		TextAssetLoader.GetWords(ref BadWordsAsset, ref badWords);
		TextAssetLoader.GetWords(ref GoodWordsAsset, ref goodWords);

        // check load results
        Debug.Log("bad words # = " + badWords.Count);
        Debug.Log("good words # = " + goodWords.Count);
    }

    void SetMeter()
    {
        AffectionMeter.SetRate(MIN, MAX);
    }

    // callback when player clicks "Send"
    // handles player input
    public void OnSubmit()
    {
        // get player input as a string
        string input = TextInputField.text;
        if (input.Equals("")) return;
		Debug.Log("on submit: " + input);

		// update conversation
		Talk.CreateTalk(input, TalkObject.Side.Left);

        // clear player input field
        TextInputField.text = "";

        // regx match to bad word list - each bad word -1
        int numBadWords = 0;
        foreach (string word in badWords)
        {
            MatchCollection matches = Regex.Matches(input, @"\b" + word + @"\b");
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
					Debug.Log("Found bad word: " + word);
					
                }
            }
            numBadWords += matches.Count;
        }
        Debug.Log("bad words # = " + numBadWords);

		// regx match to goos word list - each good word +1
		int numGoodWords = 0;
		foreach (string word in goodWords)
		{
			MatchCollection matches = Regex.Matches(input, @"\b" + word + @"\b");
			if (matches.Count > 0)
			{
				foreach (Match match in matches)
				{
					Debug.Log("Found good word: " + word);

				}
			}
			numGoodWords += matches.Count;
		}
        Debug.Log("good words # = " + numGoodWords);

		// update score
		int scoreDiff = numBadWords * ADD_BAD + numGoodWords * ADD_GOOD;
        Score.Value = Score.Value + scoreDiff;

        // activate input
        TextInputField.ActivateInputField();

        CheckScore();
    }

    void CheckInput()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            OnSubmit();
        }
    }

    void CheckScore()
    {
        if (Score.Value <= MIN || Score.Value >= MAX)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Debug.Log("Game End");
        if (Score.Value <= MIN)
        {
            FullScreen.GetComponentInChildren<Image>().sprite = LossImage;
        }
        else
        {
            FullScreen.GetComponentInChildren<Image>().sprite = WinImage;
        }
        FullScreen.SetActive(true);
    }

    public void StartGame()
    {
		InitGame();
		FullScreen.SetActive(false);
    }
}
