using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public float maxThirst = 100;
    public float maxHungry = 100;

    public float thirst;
    public float hungry;

    public float time;

    void Start()
    {
        hungry = maxHungry;
        thirst = maxThirst;
    }


    void Update()
    {
        GameObject TimeCounter = GameObject.Find("TimeManager");
        time = TimeCounter.GetComponent<TimeManager>().times;
        hungry = maxHungry - time / 5.0f;
        thirst = maxThirst - time / 5.0f; 
    }
}
