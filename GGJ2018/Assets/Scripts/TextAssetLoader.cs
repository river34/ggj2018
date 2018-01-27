using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class TextAssetLoader : MonoBehaviour {

	public static void GetWords(ref TextAsset libAsset, ref List<string> words)
	{
		if (libAsset == null) return;

		words = new List<string>();

		string fs = libAsset.text;
		string[] fLines = Regex.Split(fs, "\n|\r|\r\n");

		for (int i = 0; i < fLines.Length; i++)
		{
			fLines[i] = fLines[i].Trim();
			if (fLines[i] == "") continue;
			words.Add(fLines[i]);
		}
	}
}
