using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CharController : MonoBehaviour {

    Animator anim;
    Rigidbody2D rb;
    Vector3 startingPos = new Vector3(-5, 3,-10);
    float sentencePos;

    public TextMeshProUGUI countdown;
    public GameObject background;

    public GameObject[] ObjVision;

    public GameObject loadBar;
    public GameObject previewBox;

    public LoadTxt loadTxt;
    public SenSpawner senSpawner;

    bool haveShuffled;
    int shuffledSen;

    AnimatorOverrideController playerAnim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerAnim = GameManager.GM.outfits[GameManager.GM.currentOutfit].newAnim;
        Debug.Log(GameManager.GM.outfits[GameManager.GM.currentOutfit].name);
        anim.runtimeAnimatorController = playerAnim;
        transform.position = startingPos;

        haveShuffled = false;

        StartCoroutine(Freeze());
        StartCoroutine(SpeedUp());
    }

    void FreezePos(bool ifFrozen)
    {
        if (ifFrozen)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            anim.SetTrigger("StartRunning");
        }
    }

    public IEnumerator Freeze()
    {
        TextMeshProUGUI cd = Instantiate(countdown, new Vector2(0, 0), Quaternion.identity);
        cd.transform.SetParent(background.transform);

        FreezePos(true);
        cd.text = "3";

        yield return new WaitForSeconds(1);
        cd.text = "2";

        yield return new WaitForSeconds(1);
        cd.text = "1";

        yield return new WaitForSeconds(1);
        Destroy(cd);

        FreezePos(false);
         
    }

    //REMAKE ONCE WPM IS SET//
    IEnumerator SpeedUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            GameManager.GM.speed += 2f;
        }
    }

    void Update()
    {
        rb.AddForce(transform.right * GameManager.GM.speed * Time.deltaTime);

        if (GameObject.FindGameObjectWithTag("sentences"))
        {
            sentencePos = GameObject.FindGameObjectWithTag("sentences").transform.position.y;
        }

        if(transform.position.y < (sentencePos - 3))
        {
            PlayerPrefs.SetInt(GameManager.GM.planet.name, senSpawner.senCount);
            GameManager.GM.speed = 0;
            Manager.instance.EndGame();
        }

        if (haveShuffled && senSpawner.senCount > (shuffledSen+1))
        {
            haveShuffled = false;
            ObjVision[1].SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "gems":
                GameManager.GM.TriggerSFX("coinSFX");
                Manager.instance.AddPoint();
                break;
            case "dark":
                GameManager.GM.TriggerSFX("powerDownSFX");
                StartCoroutine(DarkVision());
                break;
            case "shuffle":
                GameManager.GM.TriggerSFX("powerDownSFX");
                ShuffleText();
                break;
            case "slow":
                GameManager.GM.TriggerSFX("powerUpSFX");
                StartCoroutine(SlowSpeed());
                break;
            case "mult":
                GameManager.GM.TriggerSFX("powerUpSFX");
                StartCoroutine(MultSenBonus());
                break;
        }

        Destroy(collision.gameObject);
    }

    /// VISION SCRIPTS ///

    void ShuffleText()
    {
        ResetVision();
        haveShuffled = true;
        shuffledSen = senSpawner.senCount;

        var split = loadTxt.splitString[senSpawner.senCount].Split(' ');
        List<string> splitString = new List<string>();
        splitString = split.ToList();

        for (int i = 0; i < split.Length; i++)
        {
            string obj = split[i];
            int randomInt = Random.Range(0, split.Length);
            split[i] = split[randomInt];
            split[randomInt] = obj;
        }

        string newString = "";

        foreach (string word in split)
        {
            newString += word + " ";
        }

        loadTxt.splitString[senSpawner.senCount] = newString.Substring(0, newString.Length-1);
        ObjVision[1].SetActive(true);
    }

    IEnumerator MultSenBonus()
    {
        ResetVision();
        Manager.instance.multSen = true;
        ObjVision[3].SetActive(true);
        yield return new WaitForSeconds(3);
        ObjVision[3].SetActive(false);
    }

    IEnumerator DarkVision()
    {
        ResetVision();
        ObjVision[0].SetActive(true);
        loadBar.SetActive(true);
        previewBox.transform.localScale += new Vector3(-1, -1);
        yield return new WaitForSeconds(3);
        ObjVision[0].SetActive(false);
        loadBar.SetActive(false);
        previewBox.transform.localScale += new Vector3(1, 1);
    }

    IEnumerator SlowSpeed()
    {
        ResetVision();
        ObjVision[2].SetActive(true);
        loadBar.SetActive(true);
        GameManager.GM.speed -= 100f;
        yield return new WaitForSeconds(3);
        GameManager.GM.speed += 100f;
        loadBar.SetActive(false);
        ObjVision[2].SetActive(false);
    }

    void ResetVision()
    {
        foreach(GameObject obj in ObjVision)
        {
            obj.SetActive(false);
        }
    }

}
