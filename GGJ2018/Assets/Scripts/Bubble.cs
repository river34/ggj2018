﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour {

    public Text UIText;

    public string Text
    {
        set
        {
            UIText.text = value;
        }
    }
}
