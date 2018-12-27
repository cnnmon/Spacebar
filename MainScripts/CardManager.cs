using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class CardManager : MonoBehaviour {

    public Outfit thisOutfit;

    Image image;
    Color disabled = new Color(0.64f, 0.62f, 0.57f, 1f);
    TextMeshProUGUI nameObject;
    TextMeshProUGUI infoObject;

    GameObject costButton;
    GameObject togOutfit;
    GameObject lockPlanetText;

    void Start()
    {
        //manually set image

        //set name
        nameObject = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        nameObject.text = thisOutfit.outfitName;

        //set desc
        infoObject = gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        infoObject.text = thisOutfit.description;

        //get other objects
        togOutfit = gameObject.transform.GetChild(5).gameObject;
        image = gameObject.transform.GetChild(0).GetComponent<Image>();
        costButton = gameObject.transform.GetChild(4).gameObject;

        CheckBought(thisOutfit);
    }

    public void CheckBought(Outfit outfit)
    {
        //if not bought
        if (!GameManager.GM.outfitsBought.Contains(outfit))
        {
            //set locked visuals
            image.color = disabled;

            //set cost & buttons
            togOutfit.SetActive(false);
            if(costButton.activeSelf)
            {
                costButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = thisOutfit.cost.ToString();
            }
        }
        else 
        {
            //disable lock
            gameObject.transform.GetChild(3).gameObject.SetActive(false);

            //set buttons and image
            costButton.SetActive(false);
            togOutfit.SetActive(true);
            image.color = Color.white;
        }
    }

    public void BuyOutfit()
    {
        //if coins are enough, deducts, sets as bought, reupdates
        if(GameManager.GM.coins >= thisOutfit.cost)
        {
            GameManager.GM.coins -= thisOutfit.cost;
            GameManager.GM.outfitsBought.Add(thisOutfit);
            CheckBought(thisOutfit);
        }
        else
        {
            GameManager.GM.TriggerSFX("errorSFX");
        }
    }

    public void OnToggleChange()
    {
        GameManager.GM.currentOutfit = thisOutfit.arrayInt;
    }
   
}
