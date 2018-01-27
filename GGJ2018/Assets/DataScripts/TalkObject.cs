using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class TalkObject : ScriptableObject {

    public string text;
    public enum Side { Right, Left };
    public Side side;
    public event Action OnChanged;
    public event Action OnResetted;

    public void CreateTalk(string _text, Side _side)
    {
        Debug.Log("CreateTalk: " + _text);
        text = _text;
        side = _side;
        if (OnChanged != null)
        {
            OnChanged();
        }
    }

    public void Reset()
    {
        if (OnResetted != null)
        {
            OnResetted();
        }
    }
}
