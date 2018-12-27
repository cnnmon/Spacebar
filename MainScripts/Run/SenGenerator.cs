using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SenGenerator : MonoBehaviour {
    public LoadTxt loadTxt;
    public static string[] senList;

    public void Update()
    {
        senList = loadTxt.splitString;
    }

    public static string GetSentence(int listIndex)
    {
        string sentence = senList[listIndex];
        return sentence;
    }
}
