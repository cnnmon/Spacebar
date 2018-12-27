using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Sentence {

    public string sentence;

    public SenDisplay display;

    public Sentence(string _sentence, SenDisplay _display, int indexNum)
    {
        sentence = _sentence;
        display = _display;

        if (sentence[indexNum] == ' ')
        {
            indexNum++;
        }

        display.typeIndex = indexNum;
        display.SetSentence(sentence);
    }

    public char NextChar()
    {
        if(display != null)
        {
            return sentence[display.typeIndex];
        }

        return ' ';
    }

    public void TypeChar(char character)
    {
        display.typeIndex++;
        display.AddLetter(character);
    }

    public bool SenTyped()
    {
        bool senTyped = (display.typeIndex >= sentence.Length);
        if (senTyped)
        {
            display.RemoveSentence();
        }
        return senTyped;
    }

}
