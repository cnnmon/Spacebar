using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class SetPlanet : MonoBehaviour {

    //Manages each individual planet in the selection screen//
    //progress, name, info, etc.//

    public Planet thisPlanet;
    public GameObject planetImage;
    public GameObject NameTxt;
    public GameObject infoTxt;
    public GameObject progressBar;
    public GameObject progressTxt;
    public Sprite[] bars;

    float totalText;
    float completedText;

    private void Start()
    {
        GetComponentInChildren<PlayButton>().planet = thisPlanet;
        planetImage.GetComponent<SpriteRenderer>().sprite = thisPlanet.image;
        NameTxt.GetComponent<TextMeshPro>().text = thisPlanet.planetName;
        infoTxt.GetComponent<TextMeshPro>().text = "Genre: " + thisPlanet.genre + System.Environment.NewLine + "Author: " + thisPlanet.author;

        BarInit();
        BarTextInit();
    }

    //calculates percentage of lines finished and total lines
    float SenPercentage()
    {
        thisPlanet.totalSenNum = totalText = thisPlanet.text.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Length;
        completedText = PlayerPrefs.GetInt(thisPlanet.name);

        return completedText / totalText;
    }

    //sets bar to percentage
    void BarInit()
    {
        float arrayNum = SenPercentage() * bars.Length;
        progressBar.GetComponent<SpriteRenderer>().sprite = bars[(int)Mathf.Round(arrayNum)];
    }

    void BarTextInit()
    {
        string percTxt = completedText.ToString() + "/" + totalText.ToString();
        progressTxt.GetComponent<TextMeshPro>().text = percTxt;
    }

}
