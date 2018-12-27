using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Manager : MonoBehaviour {

    public static Manager instance;
    public TextMeshProUGUI Score;
    public GameObject scoreUpPrefab;
    public GameObject Canvas;
    public GameObject player;
    public TextMeshProUGUI WPM;
    public SenSpawner senSpawner;

    public float wordsPerMin;
    public int score;

    public int senCount;
    bool firstSen;
    public bool multSen = false;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        WPM.text = "WPM: " + GameManager.GM.thisWPM.ToString("000");
        score = GameManager.GM.score;
        Score.text = (Mathf.RoundToInt(score)).ToString();

        firstSen = false;
        AddPoint();

        GameManager.GM.ChangeMusic("runTheme");
    }

    public void SenDestroyed()
    {
        if (!multSen)
        {
            score = Mathf.RoundToInt(score * 1.5f);
            ScoreUp("150%");
        }
        else
        {
            score = Mathf.RoundToInt(score * 3f);
            ScoreUp("300%");
            multSen = false;
        }
    }

    public void EndGame()
    {
        GameManager.GM.thisWPM = wordsPerMin;
        GameManager.GM.score = Mathf.RoundToInt(score);
        SceneManager.LoadScene("Score");
    }

    public void AddPoint()
    {
        if (!firstSen)
        {
            firstSen = true;
            score++;
        }
        else
        {
            score++;
            ScoreUp("+1");
        }
    }

    IEnumerator WaitAndDestroy(GameObject Obj)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(Obj);
    }

    public void ScoreUp(string Incr)
    {
        GameObject newScoreUp = Instantiate(scoreUpPrefab, new Vector2(450, 200), Quaternion.identity);
        newScoreUp.transform.SetParent(Canvas.transform);

        TextMeshProUGUI newScoreUpText = newScoreUp.GetComponent<TextMeshProUGUI>();
        newScoreUpText.text = Incr;

        Score.text = (Mathf.RoundToInt(score)).ToString();
        StartCoroutine(WaitAndDestroy(newScoreUp));
    }

}
