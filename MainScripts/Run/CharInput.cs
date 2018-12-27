using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharInput : MonoBehaviour {

    public SenManager senManager;

    void Update()
    {
        foreach(char character in Input.inputString)
        {
            senManager.TypeChar(character);
        }
    }
}
