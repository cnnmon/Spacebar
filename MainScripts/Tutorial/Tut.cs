using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tut : MonoBehaviour {

    public string tutName;

    void Awake()
    {
        TutorialManager.Instance.Tutorials.Add(this);
    }

    public virtual void InitiateTut(){}

    public virtual void CheckIfHappening(){}

}
