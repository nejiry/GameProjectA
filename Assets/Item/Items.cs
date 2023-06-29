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
    public bool beginItemTurn;
    public int MaxCount = 20;
    public int ItemCount = 1;
    public int VerticalItemSize;
    public int HorizontalItemSize;
    public bool itemTurn = false;
    public TextMeshProUGUI ItemCountText;
    public TextMeshProUGUI ItemNameText;
    public Vector3 beginMousePosition;

    void OnMouseDown()
    {
        Debug.Log("Pushing");
        beginPosition = this.transform.position;
        beginItemTurn = itemTurn;
        beginMousePosition = Input.mousePosition;
    }
    
    void Start(){
        ItemNameText.SetText(this.transform.name);
        
    }

    void OnMouseDrag()
    {
        boxFlag = true;

        Vector3 itemPosition = beginPosition;
        Vector3 thisPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 itemWorldPosition = Camera.main.ScreenToWorldPoint(beginMousePosition);
        Vector3 NewWorldPosition = itemPosition + (thisPosition - itemWorldPosition);//
        NewWorldPosition.z = 1f;
        this.transform.position = NewWorldPosition;
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
        else //if (other.transform.tag == "ItemSlot")
        {
            if (boxFlag == false && ItemSet == false)
            {
               //this.transform.position = beginPosition;
            }
        }
    }
    
    void Update()
    {
        if (boxFlag == true && Input.GetKeyDown(KeyCode.T))
        {
            //�e�v�f�̃X���b�g�擾���Ă��̃X���b�g�Ƃ��̃A�C�e���͈̔͂��擾���͈̔͂�bool��ύX���Ă����]����
            ResetSlotBool();
            TurnItem();
        }

        ItemCountText.SetText(ItemCount.ToString());

        if (ItemCount <= 0)
        {
            DestroyItem();
            Debug.Log("itemの個数がゼロなので消去");
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
            Debug.Log("トリガーが動いてないので元の位置に戻す");
        }
    }

    public void BackPosition()
    {
        this.transform.position = beginPosition;
        if (itemTurn != beginItemTurn)
        {
            TurnItem();
        }
        
    }

    public void DestroyItem()
    {
        GameObject slot = transform.parent.gameObject;
        slot.GetComponent<SlotManager>().storing = false;
        slot.GetComponent<SlotManager>().ItemName = "";
        Destroy(this.gameObject);
    }

    public void TurnItem()
    {
        int horizontalSize = HorizontalItemSize;
        int verticalSize = VerticalItemSize;
        if (itemTurn == false)
        {
            itemTurn = true;
            this.transform.Rotate(0f, 0f, 90f);
            HorizontalItemSize = verticalSize;
            VerticalItemSize = horizontalSize;
        }
        else if(itemTurn == true)
        {
            itemTurn = false;
            this.transform.Rotate(0f, 0f, -90f);
            HorizontalItemSize = verticalSize;
            VerticalItemSize = horizontalSize;
        }
    }

    public void ResetSlotBool()
    {
        int horizontalSize = HorizontalItemSize;
        int verticalSize = VerticalItemSize;
        GameObject slot = this.transform.parent.gameObject;
        List<Transform> TGTs = slot.GetComponent<SlotManager>().RelatedSlots(verticalSize, horizontalSize);
        foreach (Transform TGT in TGTs)
        {
            Debug.Log("storingFalse");
            TGT.GetComponent<SlotManager>().storing = false;
        }

    }
}
