using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SenDisplay : MonoBehaviour {

    public TextMeshPro UnText;
    public TextMeshPro Text;
    TextMeshProUGUI TextPreviewTyped;
    TextMeshProUGUI TextPreview;

    SenManager senManager;

    public string senString;
    public string senStringTyped;

    public int typeIndex = 10;

    public void Start()
    {
        senManager = FindObjectOfType<SenManager>();

        TextPreview = GameObject.Find("PreviewText").GetComponent<TextMeshProUGUI>();
        TextPreviewTyped = GameObject.Find("PreviewTextTyped").GetComponent<TextMeshProUGUI>();

        senString = SenGenerator.GetSentence(senManager.senIndex - 1);
        senStringTyped = senString.Substring(0, typeIndex);
    }

    public void SetSentence(string sentence)
    {
        UnText.text = senString;
    }

    public void Update()
    {
        Text.text = senStringTyped;
        UnText.text = senString;

        TextPreviewTyped.text = senStringTyped;
        TextPreview.text = senString;
    }

    public void AddLetter(char character)
    {
        senStringTyped += character;
    }

    public void RemoveSentence()
    {
        Destroy(gameObject);
    }

    public void Error(bool isWrong)
    {
        if (isWrong)
        {
            TextPreviewTyped.color = Color.red;
        }
        else
        {
            TextPreviewTyped.color = Color.white;
        }
    }

} 
