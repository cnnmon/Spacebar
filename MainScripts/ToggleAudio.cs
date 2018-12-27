using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAudio : MonoBehaviour {

    Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    public void Toggle(string name)
    {
        if (toggle.isOn)
        {
            GameManager.GM.MuteAudio(name, false);
        }
        else
        {
            GameManager.GM.MuteAudio(name, true);
        }
    }

}
