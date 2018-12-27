using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    public List<Tut> Tutorials = new List<Tut>();
    DialogueManager dialogueManager;
    public int arrayNum;
    public Tut currentTutorial;
    int menuVisit = 0;

    private static TutorialManager instance;
    public static TutorialManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TutorialManager>();
            }

            return instance;
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Intro" && menuVisit == 0)
        {
            menuVisit++;
            arrayNum = Tutorials.Count;
            SetNextTutorial(arrayNum-1);
        }

        dialogueManager = GetComponent<DialogueManager>();
    }

    void Update()
    {
        if (currentTutorial)
        {
            currentTutorial.CheckIfHappening();
        }
    }

    public void CompletedTutorial()
    {
        SetNextTutorial(arrayNum-1);
        dialogueManager.TriggerDialogue();
    }

    public void SetNextTutorial(int currentOrder)
    {
        currentTutorial = Tutorials[currentOrder];
        arrayNum--;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(currentTutorial != null)
        {
            if (scene.name == "StationTutorial" && currentTutorial.tutName == "NaviTutorial" || scene.name == "Score" && currentTutorial.tutName == "Type2Tutorial")
            {
                CompletedTutorial();
            }
            else if (scene.name == "Menu" && currentTutorial.tutName == "HomeTutorial")
            {
                //END TUTORIAL - TUTORIALMANAGER
                dialogueManager.TriggerDialogue();
            }
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
