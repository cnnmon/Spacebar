using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SenSpawner : MonoBehaviour
{
    public GameObject textPrefab;
    public GameObject gemPrefab;
    public GameObject[] powerDowns;
    public GameObject[] powerUps;
    GameObject bonusPrefab;
    GameObject obsPrefab;

    Vector2 Pos;
    public int senCount;
    int lastSenCount;

    GameObject senObj;
    public GameObject player;
    public GameObject spawns;

    Transform senObjChild;
    Renderer rend;

    Vector2 playerPos;
    SenDisplay senDisplay;
    int unitCount = 0;
    List<float> lastPos = new List<float>();

    public void Start()
    {
        senCount = PlayerPrefs.GetInt(GameManager.GM.planet.name);
        lastSenCount = PlayerPrefs.GetInt(GameManager.GM.planet.name);
    }
    
    public void Update()
    {
        playerPos = player.transform.position;

        if (senObj && lastSenCount != senCount) 
        {
            StartCoroutine(Wait());
        }

        if(lastSenCount != senCount)
        {
            DestroySpawns();
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);

        senObjChild = senObj.transform.GetChild(1);
        rend = senObjChild.GetComponent<Renderer>();
        SpawnObj(rend);
    }

    public SenDisplay SpawnSen()
    {
        if (senCount == (PlayerPrefs.GetInt(GameManager.GM.planet.name)))
        {
            Pos = new Vector3(playerPos.x + 4, playerPos.y - 3f, -10);
            senCount++;
        }
        else
        {
            Pos = new Vector3(playerPos.x + (330 + GameManager.GM.speed/15) * Time.deltaTime, playerPos.y - 5f, -10);
            senCount++;
        }

        senObj = Instantiate(textPrefab, Pos, Quaternion.identity);

        senDisplay = senObj.GetComponent<SenDisplay>();
        return senDisplay;
    }

    public void SpawnObj(Renderer render)
    {
        unitCount = 0;

        for (float i = render.bounds.min.x; i < render.bounds.max.x; i++)
        {
            //spawn objects !!!
            //powerDowns [Bomb, Poison]
            //powerUps [Clock, Apple]

            if ((!lastPos.Contains(i)) && (i < playerPos.x == false))
            {
                if (unitCount % 3 == 0)
                {
                    Vector3 instPos = new Vector3(i, render.bounds.max.y + 0.7f, -10);
                    Instantiate(gemPrefab, instPos, Quaternion.identity, spawns.transform);
                    lastPos.Add(i);
                } else if (unitCount % 17 == 0)
                {
                    if (UnityEngine.Random.Range(0, 10) <= 7)
                    {
                        bonusPrefab = powerUps[0];
                    }
                    else
                    {
                        bonusPrefab = powerUps[1];
                    }

                    Vector3 instPos = new Vector3(i, render.bounds.max.y + 0.7f, -10);
                    Instantiate(bonusPrefab, instPos, Quaternion.identity, spawns.transform);
                    lastPos.Add(i);
                }
                else if (unitCount % 20 == 0 && UnityEngine.Random.Range(0, 10) > 4)
                {
                    if(UnityEngine.Random.Range(0, 10) <= 7)
                    {
                        obsPrefab = powerDowns[0];
                    }
                    else
                    {
                        obsPrefab = powerDowns[1];
                    }

                    Vector3 instPos = new Vector3(i, render.bounds.max.y + 0.7f, -10);
                    Instantiate(obsPrefab, instPos, Quaternion.identity, spawns.transform);
                    lastPos.Add(i);
                }
            }
            unitCount++;
        }

    }

    public void DestroySpawns()
    {
        foreach(Transform child in spawns.transform)
        {
            Destroy(child.gameObject);
        }

        lastSenCount++;
    }

}
