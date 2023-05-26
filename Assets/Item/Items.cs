using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Items : MonoBehaviour
{
    public bool boxFlag = false;
    public bool ItemSet = false;
    public bool ItemCounted = false;
    public bool trigWorking = false;
    public Vector3 beginPosition;
    public int MaxCount = 20;
    public int ItemCount = 1;
    public int ItemVolume = 1;

    void OnMouseDown()
    {
        beginPosition = this.transform.position;
        //ItemSet = false;
    }
    

    void OnMouseDrag()//�h���b�O���̃A�j���[�V����
    {
        //�h���b�O���͋z������ł͂���
        boxFlag = true;
        //�ȉ��l�s�̓h���b�O�������ɃI�u�W�F�N�g�𓮂����R�[�h
        Vector3 thisPosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(thisPosition);
        worldPosition.z = 0f;
        this.transform.position = worldPosition;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        trigWorking = true;
        if (other.transform.tag == "Item" && boxFlag == false )
        {
            if (other.transform.name == this.transform.name)//�A�C�e���̃X�^�b�N
            {
                if (ItemSet == false && ItemCounted == false)//�ړ������悤�Ƃ����A�C�e��
                {
                    int otherCount = other.GetComponent<Items>().ItemCount;
                    other.GetComponent<Items>().ItemCount = 0;
                    int itemCount = ItemCount + otherCount;
                    ItemCounted = true;
                    if (itemCount > MaxCount)
                    {
                        ItemCount = itemCount - MaxCount;
                        other.GetComponent<Items>().ItemCount = MaxCount;
                        BackPosition();
                    }
                    else if (itemCount <= MaxCount)
                    {
                        other.GetComponent<Items>().ItemCount = itemCount;
                        DestroyItem();
                    }
                }
            }
        }
        if (other.transform.tag == "ItemSlot" && boxFlag == false)
        {
            
        }
        else
        {
            if (boxFlag == false && ItemSet == false)
            {
                this.transform.position = beginPosition;
            }
        }
    }
    
    void Update()
    {
        this.GetComponent<TextMeshProUGUI>().text = ItemCount.ToString();
        if (ItemCount <= 0)
        {
            DestroyItem();
        }
    }

    void OnTriggerExit2D()
    {
        trigWorking = false;
    }
    
    void OnMouseUp()//���N�����グ����boxflag��false�ɂ��ăg���K�[����������B�������Ȃ�������A�C�e�������̈ʒu�ɖ߂�
    {
        boxFlag = false;
        ItemCounted = false;
        if (trigWorking == false)
        {
            this.transform.position = beginPosition;
            Debug.Log("return");
        }
    }

    public void BackPosition()
    {
        this.transform.position = beginPosition;
    }

    public void DestroyItem()
    {
        GameObject slot = transform.parent.gameObject;
        slot.GetComponent<SlotManager>().storing = false;
        slot.GetComponent<SlotManager>().ItemName = "";
        Destroy(this.gameObject);
    }
}
