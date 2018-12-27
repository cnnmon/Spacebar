using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderBehavior : MonoBehaviour {

    public TextMeshProUGUI speedText;
    float valueAdd;

    public void Start()
    {
        valueAdd = 0;
        GetComponent<Slider>().value = valueAdd = GameManager.GM.speed;
        speedText.text = valueAdd.ToString();
    }

    public void OnSliderChange()
    {
        valueAdd = GetComponent<Slider>().value;
        GameManager.GM.speed = valueAdd;
        speedText.text = valueAdd.ToString();
    }

}
