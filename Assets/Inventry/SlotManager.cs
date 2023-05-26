using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public bool storing = false;
    public string ItemName;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Item" && other.GetComponent<Items>().boxFlag == false)
        {
            if(storing == false && ItemName == "")
            {
                other.transform.position = this.transform.position;
                other.transform.SetParent(this.transform);
                ItemName = other.transform.name;
                other.GetComponent<Items>().ItemSet = true;
                storing = true;
                string itemVolume = other.GetComponent<Items>().ItemVolume;
                if (itemVolume == "2x1")
                {
                    Transform TGTs = RelatedSlots(other.transform);
                    bool ItemStoring = TGTs.GetComponent<SlotManager>().storing;
                    if (ItemStoring == true)
                    {
                        other.GetComponent<Items>().BackPosition();
                        Debug.Log("what");
                    }
                    else
                    {
                        TGTs.GetComponent<SlotManager>().storing = true;
                        Debug.Log(ItemStoring+TGTs.name);
                    }
                }
            }
            else if (ItemName == other.transform.name)
            {
                other.transform.position = this.transform.position;
                other.transform.SetParent(this.transform);
                ItemName = other.transform.name;
                storing = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)//アイテムがスロットから離れた時の処理
    {
        if (other.transform.tag == "Item")
        {
            if (other.GetComponent<Items>().ItemSet == true)
            //離れたアイテムがセットされてるアイテムだったなら
            {
                if (ItemName == other.transform.name)
                //そのアイテムがセットされてるアイテムと同じ名前だったら
                {
                    storing = false;
                    other.GetComponent<Items>().ItemSet = false;
                    ItemName = "";

                    string itemVolume = other.GetComponent<Items>().ItemVolume;
                    if (itemVolume == "2x1")
                    {
                         Transform TGTs = RelatedSlots(other.transform);
                        if (TGTs.GetComponent<SlotManager>().ItemName == "")
                        {
                            TGTs.GetComponent<SlotManager>().storing = false;
                        }
                    }
                }
            }
        }
    }

    public void SlotItemDelete()
    {
        if (storing == true)
        {
            GameObject item = transform.GetChild(0).gameObject;
            storing = false;
            ItemName = "";
            Destroy(item.gameObject);
        }
    }
    void Update()
    {
        //if (this.transform.GetChild(0).gameObject)
        //{
        //    ItemName = "";
        //}
    }
    public Transform RelatedSlots(Transform other)
    {
        Transform TGTs = null;
        GameObject parent = this.transform.parent.gameObject;
        if (parent.transform.name == "InventrySlots")//横列のスロット数取得。できればなくていい
        {
            Transform Slots = parent.GetComponentInChildren<Transform>();
            int i = 0;
            foreach (Transform slot in Slots)
            {
                if (slot.name == this.transform.name)
                {
                    TGTs = parent.transform.GetChild(i + 5).transform;
                }
                i++;
            }
        }
        else if (parent.transform.name == "StashSlots")//横列のスロット数取得。できればなくていい
        {
            Transform Slots = parent.GetComponentInChildren<Transform>();
            int i = 0;
            foreach (Transform slot in Slots)
            {
                if (slot.name == this.transform.name)
                {
                    TGTs = parent.transform.GetChild(i + 10).transform;
                }
                i++;
            }
        }
        return TGTs;
    }
}
