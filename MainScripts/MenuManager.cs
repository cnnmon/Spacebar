using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI topWPM;
    public Button still;
    public Button stationButton;
    Sprite currentSprite;

    void Start()
    {
        coinText.text = GameManager.GM.coins.ToString("00000");

        currentSprite = GameManager.GM.outfits[GameManager.GM.currentOutfit].still;
        still.GetComponent<Image>().sprite = currentSprite;

        if (GameManager.GM.tutorialDone)
        {
            GameManager.GM.SkipTutorial();
            stationButton.enabled = false;
            topWPM.gameObject.SetActive(true);
            topWPM.text = "best wpm: " + GameManager.GM.WPM.ToString("000");
        }
    }

    public void SkipTutorial()
    {
        GameManager.GM.tutorialDone = true;
        GameObject.Find("BlackFade").GetComponent<LevelChanger>().FadeToLevel("Menu");
    }

}
