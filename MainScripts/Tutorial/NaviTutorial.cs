using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NaviTutorial : Tut {

    private bool isCurrentTutorial = false;
    //private InitiateTut initiateTut;

    public override void CheckIfHappening()
    {
        isCurrentTutorial = true;
    }

    public override void InitiateTut()
    {
        ChangeAlpha("Base");
        ChangeAlpha("Arrow");
        Button station = GameObject.Find("Station").GetComponent<Button>();
        station.enabled = true;
    }

    void ChangeAlpha(string name)
    {
        var tempColor = GameObject.Find(name).GetComponent<Image>().color;
        tempColor.a = 1f;
        GameObject.Find(name).GetComponent<Image>().color = tempColor;
    }

}
