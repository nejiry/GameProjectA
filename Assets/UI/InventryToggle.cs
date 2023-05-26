using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�C���x���g���{�^���ŃC���x���g���̕\���E��\���̐؂�ւ����ł���B

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
