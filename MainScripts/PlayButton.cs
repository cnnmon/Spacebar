using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class PlayButton : MonoBehaviour, IPointerClickHandler,
                                  IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Planet planet;
    LevelChanger levelChanger;

    void Start()
    {
        Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        levelChanger = FindObjectOfType<LevelChanger>();
        AddEventSystem();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.GM.score = 0;
        GameManager.GM.planet = planet;
        levelChanger.FadeToLevel("Load");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<TextMeshPro>().color = Color.black;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<TextMeshPro>().color = new Color(0.11f, 0.63f, 0.27f, 1f);
        GameManager.GM.planet = planet;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<TextMeshPro>().color = Color.white;
    }

    void AddEventSystem()
    {
        GameObject eventSystem = null;
        GameObject tempObj = GameObject.Find("EventSystem");
        if (tempObj == null)
        {
            eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
        else
        {
            if ((tempObj.GetComponent<EventSystem>()) == null)
            {
                tempObj.AddComponent<EventSystem>();
            }

            if ((tempObj.GetComponent<StandaloneInputModule>()) == null)
            {
                tempObj.AddComponent<StandaloneInputModule>();
            }
        }
    }

}
