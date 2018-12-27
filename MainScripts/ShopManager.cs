using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour {

    public TextMeshProUGUI coinText;

    void Update()
    {
        coinText.text = GameManager.GM.coins.ToString("00000");
    }

}
