using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public bool boxFlag = false;

    void OnMouseDrag()
    {
        boxFlag = true;
    }
    void OnMouseUp(){
        boxFlag = false;
    }
    //もしバックパックのスロット出なければ元の位置に戻す
}
