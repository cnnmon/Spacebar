using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {
    
    public GameObject TextBox;
    Canvas parentCanvas;
    public Dialogue[] tutorial;
    Button[] disabled;

    GameObject newTextBox;
    TextMeshProUGUI nameText;
    TextMeshProUGUI dialogueText;
    int menuVisit = 0;

    Queue<string> sentences;

    public void StartDialogue(Dialogue dialogue)
    {
        GameManager.GM.tutorialNum++;
        newTextBox = Instantiate(TextBox, new Vector2(1.1f, -1), Quaternion.identity, parentCanvas.transform);

        nameText = newTextBox.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        dialogueText = newTextBox.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        nameText.text = "Kitkat";
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Menu" && menuVisit == 0 && GameManager.GM.tutorialDone == false)
        {
            menuVisit++;
            TriggerDialogue();
            disabled = FindObjectsOfType<Button>();
            foreach (Button button in disabled)
            {
                if(button.name != "SKIP")
                {
                    button.enabled = false;
                }
            }
        }
    }

    public void TriggerDialogue()
    {
        sentences = new Queue<string>();
        if (GameObject.Find("!Canvas") != null)
        {
            parentCanvas = GameObject.Find("!Canvas").GetComponent<Canvas>();
            StartDialogue(tutorial[GameManager.GM.tutorialNum]);
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if(nameText != null)
        {
            if (Input.GetKeyDown(KeyCode.Space) && sentences.Count > 0)
            {
                DisplayNextSentence();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && sentences.Count == 0)
            {
                if(GameManager.GM.tutorialNum == tutorial.Length)
                {
                    //END TUTORIAL - DIALOGUEMANAGER
                    Destroy(newTextBox);
                    GameManager.GM.CompletedAllTutorials();
                    Destroy(this);
                }
                else
                {
                    TutorialManager.Instance.currentTutorial.InitiateTut();
                    Destroy(newTextBox);
                }
            }
        }
    }

    void DisplayNextSentence()
    {
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("END");
    }

}
