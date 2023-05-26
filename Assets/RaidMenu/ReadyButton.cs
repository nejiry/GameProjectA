using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ReadyButton : MonoBehaviour
{
    public string MapName;
    [SerializeField] TextMeshProUGUI colorText;

    void Start()
    {
        if (MapName == "")
        {
            colorText.color = new Color(0.4f, 0.4f, 0.4f, 1.0f);
        }
    }

    void Update()
    {
        if (MapName != "")
        {
            colorText.color = new Color(1f, 1f, 1f, 1.0f);
        }
        else if (MapName == "")
        {
            colorText.color = new Color(0.4f, 0.4f, 0.4f, 1.0f);
        }
    }

    void OnMouseDown()
    {
        if(MapName != "")
        {
            SceneManager.LoadScene(MapName);
            Debug.Log(MapName);
        }
        else 
        {
            return;
        }
        
    }
}
