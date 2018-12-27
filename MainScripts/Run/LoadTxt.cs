using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.IO;

public class LoadTxt : MonoBehaviour {

    public string txtContents;
    public string[] splitString;

    public void Awake()
    {
        txtContents = GameManager.GM.planet.text.text;
        SplitString();
    }

    public void SplitString()
    {
        splitString = txtContents.Split(new string[] { "\r\n", "\n" },StringSplitOptions.RemoveEmptyEntries);
    }

}
