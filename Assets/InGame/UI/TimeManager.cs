using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    
    public float timeLimit = 1200;//1200 = 20min  //制限時間
    public float ingameTimes;//経過時間
    public float times;//基準になる時間

    void Start()
    {
        times = Time.deltaTime;
    }

    void Update()
    {
        times += Time.deltaTime;
        ingameTimes = timeLimit - times;
        int xxx = (int)ingameTimes;
        var span = new TimeSpan(0, 0, xxx);
        timeText.SetText(span.ToString(@"mm\:ss"));
    }
}
