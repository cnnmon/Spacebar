using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SenManager : MonoBehaviour {

    public GameObject background;

    SenSpawner senSpawner;
    SenGenerator senGenerator;
    public LoadTxt loadTxt;

    public Color nightColor;
    public Color sunriseColor;
    public Color dayColor;

    public int senIndex;
    bool needNewSen = false;
    Image image;

    bool firstType = true;
    int charCount = 0;
    float wordsPerMin;
    IEnumerator timer;
    int ticks = 0;

    public Sentence sentence;

    private void Awake()
    {
        senSpawner = GetComponent<SenSpawner>();
        senGenerator = GetComponent<SenGenerator>();
        image = background.GetComponent<Image>();
        image.sprite = GameManager.GM.planet.background;
        image.color = nightColor;
        senIndex = PlayerPrefs.GetInt(GameManager.GM.planet.name);

        timer = Timer();
    }

    public void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        needNewSen = true;
    }

    public void AddSentence()
    {
        needNewSen = false;
        sentence = new Sentence(SenGenerator.GetSentence(senIndex), senSpawner.SpawnSen(), 10);
        senIndex++;
    }

    public void Update()
    {
        if (needNewSen == true && SenGenerator.GetSentence(senIndex) != null)
        { 
            AddSentence();
        }
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            ticks++;
            Manager.instance.wordsPerMin = wordsPerMin = Mathf.Round(((charCount / 5f) / (ticks / 60f)));
        }
    }

    public void TypeChar(char character)
    {
        if (firstType)
        {
            StartCoroutine(timer);
            firstType = false;
        }

        if (sentence.NextChar() == character)
        {
            sentence.display.Error(false);
            sentence.TypeChar(character);
            charCount++;
        }
        else
        {
            sentence.display.Error(true);
            GameManager.GM.TriggerSFX("errorSFX");
        }

        if (sentence.SenTyped())
        {
            float progress = (float)senIndex / (float)loadTxt.splitString.Length;

            if (progress <= 0.5)
            {
                image.color = Color.Lerp(nightColor, sunriseColor, progress * 2);
            }
            else
            {
                image.color = Color.Lerp(sunriseColor, dayColor, progress);
            }

            if (senIndex == loadTxt.splitString.Length)
            {
                Debug.Log("Planet Completed");
                StopCoroutine(timer);
                PlayerPrefs.SetInt(GameManager.GM.planet.name, 0);
                GameManager.GM.planetCompleted = true;
                Manager.instance.EndGame();
            }
            else
            {
                Manager.instance.SenDestroyed();
                needNewSen = true;
            }
        }
    }

}
