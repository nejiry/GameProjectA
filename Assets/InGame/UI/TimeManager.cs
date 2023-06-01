using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    
    public float maxTimes = 60;//制限時間
    public float ingameTimes;//経過時間
    public float times;//基準になる時間

    void Start()
    {
        times = Time.deltaTime;
    }

    void Update()
    {
        times += Time.deltaTime;
        ingameTimes = maxTimes - times;
        timeText.SetText("{0:1}",ingameTimes);
    }
}
