using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameManager : MonoBehaviour {

    public static GameManager GM;
    public int coins = 0;
    public float speed = 450f;
    public int tutorialNum;

    public Planet planet;
    public int score;
    public float WPM;
    public float thisWPM;

    public bool tutorialDone = false;
    public bool resetOnce = false;
    public bool planetCompleted = false;

    public Outfit[] outfits;
    public List<Outfit> outfitsBought;
    public int currentOutfit = 0;

    [Header("Music")]
    public AudioSource musicManager;
    public AudioClip mainTheme;
    public AudioClip runTheme;

    [Header("SFX")]
    public AudioSource sfxManager;
    public AudioClip errorSFX;
    public AudioClip powerUpSFX;
    public AudioClip powerDownSFX;
    public AudioClip coinSFX;

    private void Awake()
    {
        tutorialNum = 0;
        JustMonika();
        DontDestroyOnLoad(gameObject);

        if (tutorialDone)
        {
            SkipTutorial();
        }

        //Saves to C:\Users\[NAME]\AppData\LocalLow\DefaultCompany\MicrosoftWord
        dataPath = Path.Combine(Application.persistentDataPath, "CharacterData.txt");
        saveData = LoadData(dataPath);

        //GET DATA
        coins = saveData.coins;
        tutorialDone = saveData.tutorialDone;
        outfitsBought = saveData.outfitsBought;
        WPM = saveData.WPM;
    }

    private void Start()
    {
        musicManager.clip = mainTheme;
        musicManager.Play();
    }

    void JustMonika()
    {
        if(GM == null)
        {
            GM = this;
        }
        else
        {
            if(GM != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SkipTutorial()
    {
        //END TUTORIAL - GAMEMANAGER
        CompletedAllTutorials();
        Destroy(GetComponent<DialogueManager>());
        Destroy(GetComponent<TutorialManager>());

        //checks if player has any coins after skipping tut (replaces tutorial money)
        if(coins == 0)
        {
            coins += 50;
        }
    }

    public void CompletedAllTutorials()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("tutorial");
        tutorialDone = true;

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }

    ///  MUSIC ///

    public void ChangeMusic(string name)
    {
        if (name == "mainTheme")
        {
            musicManager.clip = mainTheme;
            musicManager.Play();
        }
        else if (name == "runTheme")
        {
            musicManager.clip = runTheme;
            musicManager.Play();
        }
    }

    public void TriggerSFX(string name)
    {
        if (name == "errorSFX")
        {
            sfxManager.clip = errorSFX;
        }
        else if (name == "powerUpSFX")
        {
            sfxManager.clip = powerUpSFX;
        }
        else if (name == "powerDownSFX")
        {
            sfxManager.clip = powerDownSFX;
        }
        else if (name == "coinSFX")
        {
            sfxManager.clip = coinSFX;
        }

        sfxManager.Play();
    }

    public void MuteAudio(string name, bool ifMute)
    {
        if (ifMute) //MUTES MUSIC/AUDIO
        {
            if (name == "music")
            {
                musicManager.mute = true;
            }
            else if (name == "sfx")
            {
                sfxManager.mute = true;
            }
        }
        else //UNMUTES MUSIC/AUDIO
        {
            if (name == "music")
            {
                TriggerSFX("coinSFX");
                musicManager.mute = false;
            }
            else if (name == "sfx")
            {
                TriggerSFX("coinSFX");
                sfxManager.mute = false;
            }
        }
    }

    /// DATA ///

    [Serializable]
    public class PlayData
    {
        public int coins;
        public bool tutorialDone;
        public List<Outfit> outfitsBought = new List<Outfit>();
        public float WPM;
    }

    public PlayData saveData;
    string dataPath;

    void OnApplicationQuit()
    {
        saveData.coins = coins;
        saveData.tutorialDone = tutorialDone;
        saveData.outfitsBought = outfitsBought;
        saveData.WPM = WPM;

        SaveData(saveData, dataPath);
    }

    public void ResetData()
    {
        //SAVE DATA
        PlayerPrefs.DeleteAll();

        currentOutfit = 0;
        saveData.coins = coins = 0;
        saveData.tutorialDone = tutorialDone = false;
        saveData.outfitsBought = outfitsBought = new List<Outfit>(new Outfit[] { outfits[0] });
        saveData.WPM = WPM = 0f;

        SaveData(saveData, dataPath);
        Application.Quit();
    }

    static void SaveData(PlayData data, string path)
    {
        string jsonString = JsonUtility.ToJson(data);

        using (StreamWriter streamWriter = File.CreateText(path))
        {
            streamWriter.Write(jsonString);
        }
    }

    static PlayData LoadData(string path)
    {
        using (StreamReader streamReader = File.OpenText(path))
        {
            string jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<PlayData>(jsonString);
        }
    }


}

