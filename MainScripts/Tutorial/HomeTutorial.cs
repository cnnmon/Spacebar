using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeTutorial : Tut {

    private bool isCurrentTutorial = false;

    public override void CheckIfHappening()
    {
        isCurrentTutorial = true;
    }

    public override void InitiateTut()
    {
        ChangeAlpha("Base");
        ChangeAlpha("Arrow");
    }

    void ChangeAlpha(string name)
    {
        var tempColor = GameObject.Find(name).GetComponent<Image>().color;
        tempColor.a = 1f;
        GameObject.Find(name).GetComponent<Image>().color = tempColor;
    }

}
