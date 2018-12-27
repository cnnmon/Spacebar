using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[System.Serializable]
public class Planet : ScriptableObject {

    public string planetName = "Planet Name Here";
    public string genre;
    public string author;
    public TextAsset text;

    public Sprite background;
    public Sprite image;
    public Outfit outfit;

    public float totalSenNum;

}
