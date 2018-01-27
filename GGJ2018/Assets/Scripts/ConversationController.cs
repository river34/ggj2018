using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConversationController : MonoBehaviour {

    public TalkObject Talk;

    public GameObject BubbleLeftPrefab;

    public GameObject BubbleRightPrefab;

    public int MoveDist = 200;

    public float MoveSpeed = 200;

    int positionY = 0;

    RectTransform rect;

    Vector2 position;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        position = rect.anchoredPosition;
    }

    private void Start()
    {
        Talk.OnChanged += AddTalk;
        Talk.OnResetted += ResetPosition;
    }

    void AddTalk()
    {
        // add a talk to the conversation panel
        GameObject go;

        if (Talk.side == TalkObject.Side.Left)
        {
            go = Instantiate(BubbleLeftPrefab, transform);
        }
        else
		{
			go = Instantiate(BubbleRightPrefab, transform);
		}

        Bubble bubble = go.GetComponent<Bubble>();
        bubble.Text = Talk.text;

		RectTransform goRect = go.GetComponent<RectTransform>();
		goRect.anchoredPosition = new Vector2(goRect.anchoredPosition.x, positionY);
		positionY -= MoveDist;

        // update conversation panel position
        // rect.anchoredPosition += new Vector2(0, MoveDist);
        if (Talk.side == TalkObject.Side.Right)
		{
			StopAllCoroutines();
			StartCoroutine(Move(MoveDist * 2));
        }
    }

    IEnumerator Move(float dist)
    {
        float currDist = 0;
        while (currDist < dist)
        {
            currDist += Time.deltaTime * MoveSpeed;
            rect.anchoredPosition += new Vector2(0, Time.deltaTime * MoveSpeed);
            yield return new WaitForEndOfFrame();
        }
    }

    void ResetPosition()
    {
        rect.anchoredPosition = position;
    }
}
