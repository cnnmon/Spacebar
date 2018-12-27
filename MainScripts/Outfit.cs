using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Outfit : ScriptableObject {

    public string outfitName = "Item Name Here";
    public int cost = 50;
    public string description;
    public AnimatorOverrideController newAnim;
    public Sprite still;
    public int arrayInt;
}
