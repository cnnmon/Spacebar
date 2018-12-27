using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour {

    //Manages selection planet screen//

    readonly float speed = 0.05f;
    public GameObject GoUp;
    public GameObject GoDown;
    public GameObject Navigate;

    private void Start()
    {
        GoUp.SetActive(false);
        GoDown.SetActive(false);
        StartCoroutine("WaitAndFade", Navigate);
    }

    IEnumerator WaitAndFade(GameObject arrow)
    {
        arrow.SetActive(true);
        yield return new WaitForSeconds(3);
        arrow.SetActive(false);
    }

    void Update()
    {
        float yAxisValue = Input.GetAxis("Vertical") * speed;

        if (Camera.main.transform.position.y <= -0.55f && yAxisValue < 0)
        {
            StartCoroutine("WaitAndFade", GoUp);
        }
        else if (Camera.main.transform.position.y >= 10 && yAxisValue > 0)
        {
            StartCoroutine("WaitAndFade", GoDown);
        }
        else
        {
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + yAxisValue, transform.position.z);
        }
    }

}