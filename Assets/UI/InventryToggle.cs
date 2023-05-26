using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//インベントリボタンでインベントリの表示・非表示の切り替えができる。

public class InventryToggle : MonoBehaviour
{
    [SerializeField] GameObject inventry;

    void OnMouseDown()
    {
        if (inventry.activeSelf)
        {
            inventry.SetActive(false);
        }
        else
        {
            inventry.SetActive(true);
        }
    }
}
