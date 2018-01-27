using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class IntObject : ScriptableObject
{
    int value;
    public event Action OnChanged;
    public event Action OnSubmitted;

    public int Value
    {
        set
		{
			if (OnSubmitted != null)
			{
				OnSubmitted();
			}
            if (this.value != value)
			{
				if (OnChanged != null)
				{
					OnChanged();
				}
            }
            this.value = value;
        }
        get
        {
            return value;
        }
    }
}
