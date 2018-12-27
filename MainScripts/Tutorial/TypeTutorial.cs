using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypeTutorial : Tut
{
    private bool isCurrentTutorial = false;

    public override void CheckIfHappening()
    {
        isCurrentTutorial = true;
    }

    public override void InitiateTut()
    {
        Manager1.instance.movementSpeed = 300f;
        StartCoroutine(GameObject.Find("Player").GetComponent<CharController1>().Freeze());
    }

}
