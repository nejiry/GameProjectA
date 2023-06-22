using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabButton : MonoBehaviour
{
    public GameObject targetToggle;
    public GameObject parentObejct;

    void OnMouseDown()
    {
        Debug.Log("Pushed");
        AllClose();
        targetToggle.SetActive(true);
    }

    void AllClose(){
        Transform panels = parentObejct.GetComponentInChildren<Transform>();
        foreach (Transform panel in panels)
        {
            panel.gameObject.SetActive(false);
        }
    }
}
