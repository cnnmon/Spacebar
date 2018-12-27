using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour {

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI runWPM;
    public TextMeshProUGUI bestWPM;
    public GameObject tutMask;
    int newCoins;

    public GameObject prizeRays;

	void Start () {

        GameManager.GM.ChangeMusic("mainTheme");
        scoreText.text = GameManager.GM.score.ToString("000000");
        newCoins = Mathf.RoundToInt(GameManager.GM.score / Random.Range(5f, 15f));

        if(GameManager.GM.thisWPM > GameManager.GM.WPM)
        {
            GameManager.GM.WPM = GameManager.GM.thisWPM;
            Debug.Log("New record!");
        }

        runWPM.text = "WPM: " + GameManager.GM.thisWPM.ToString("000");
        bestWPM.text = "Best WPM: " + GameManager.GM.WPM.ToString("000");

        coinText.text = newCoins.ToString("00000");
        GameManager.GM.coins += newCoins;

        if (!GameManager.GM.tutorialDone)
        {
            tutMask.SetActive(true);
        }

        //checks if planet was completed and if the planet's outfit is not already obtained
        if (GameManager.GM.planetCompleted && !GameManager.GM.outfitsBought.Contains(GameManager.GM.planet.outfit))
        {
            GiftOutfit();
        }

    }

    void GiftOutfit ()
    {
        prizeRays.SetActive(true);
        prizeRays.transform.GetChild(2).GetComponent<Image>().sprite = GameManager.GM.planet.outfit.still;
        GameManager.GM.outfitsBought.Add(GameManager.GM.planet.outfit);
    }

}
