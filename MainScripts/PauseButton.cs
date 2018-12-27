using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

    public GameObject pauseScreen;

    public void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitRun()
    {
        Time.timeScale = 1;
        GameManager.GM.ChangeMusic("mainTheme");
        GameObject.Find("BlackFade").GetComponent<LevelChanger>().FadeToLevel("Menu");
    }

}
