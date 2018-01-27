using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class IntObject : ScriptableObject
{
    int value;
    int lastValue;
    public event Action OnChanged;
    public event Action OnSubmitted;

    public int Value
    {
        set
		{
			this.value = value;
            if (lastValue != value)
			{
				if (OnChanged != null)
				{
					OnChanged();
				}
            }
			if (OnSubmitted != null)
			{
				OnSubmitted();
			}
            lastValue = value;
        }
        get
        {
            return value;
        }
    }
}
