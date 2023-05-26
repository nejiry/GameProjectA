using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChoice : MonoBehaviour
{
    public ReadyButton readyButton;
    public string mapName;

    void Start()
    {
        this.GetComponent<Renderer>().material.color = new Color(0.4f, 0.4f, 0.4f, 1.0f);
    }

    void Update()
    {
        if (readyButton.MapName != mapName)
        {
            this.GetComponent<Renderer>().material.color = new Color(0.4f, 0.4f, 0.4f, 1.0f);
        }
    }

    void OnMouseDown()
    {
        if (readyButton.MapName != mapName)
        {
            readyButton.MapName = mapName;
            this.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1.0f);
        }
        else if (readyButton.MapName == mapName)
        {
            readyButton.MapName = "";
            this.GetComponent<Renderer>().material.color = new Color(0.4f, 0.4f, 0.4f, 1.0f);
        }
    }
}
