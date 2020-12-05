using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isNextStage;
    public GameObject[] target;

    void Update()
    {
        if (isNextStage)
        {
            Instantiate(target[Random.Range(0,6)], transform.position, Quaternion.identity);
            isNextStage = false;
        }
    }
}
