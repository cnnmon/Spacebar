using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Type2Tutorial : Tut {

    private bool isCurrentTutorial = false;

    public override void CheckIfHappening()
    {
        isCurrentTutorial = true;
    }

    public override void InitiateTut()
    {
        Time.timeScale = 1;
        GameObject.Find("BlackFade").GetComponent<LevelChanger>().FadeToLevel("StationTutorial");
    }

}
