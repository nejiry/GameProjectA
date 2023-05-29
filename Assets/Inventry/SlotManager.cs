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
            if (storing == false && ItemName == "")//スロットが本当に空なら
            {
                //スロットの仲が空なら吸い込む
                other.transform.position = this.transform.position;
                other.transform.SetParent(this.transform);
                ItemName = other.transform.name;
                other.GetComponent<Items>().ItemSet = true;

                //複数マス目を持つアイテムの処理
                int horizontalSize = other.GetComponent<Items>().HorizontalItemSize;
                int verticalSize = other.GetComponent<Items>().VerticalItemSize;
                if (RelatedSlots(verticalSize, horizontalSize) != null)
                {
                    List<Transform> TGTs = RelatedSlots(verticalSize, horizontalSize);
                    foreach (Transform TGT in TGTs)
                    {
                        bool ItemStoring = TGT.GetComponent<SlotManager>().storing;
                        if (ItemStoring == true)
                        {
                            other.GetComponent<Items>().BackPosition();
                        }
                        else
                        {
                            TGT.GetComponent<SlotManager>().storing = true;
                        }
                    }
                }
                else
                {
                    other.GetComponent<Items>().BackPosition();
                }
            }

            else if (storing == true && ItemName == other.transform.name)//アイテムが中に入っていて同じアイテムなら
            {
                //吸い込み処理
                other.transform.position = this.transform.position;
                other.transform.SetParent(this.transform);
                ItemName = other.transform.name;

                //複数マス目のアイテムの処理
                int horizontalSize = other.GetComponent<Items>().HorizontalItemSize;
                int verticalSize = other.GetComponent<Items>().VerticalItemSize;
                if (RelatedSlots(verticalSize, horizontalSize) != null)
                {
                    List<Transform> TGTs = RelatedSlots(verticalSize, horizontalSize);
                    foreach (Transform TGT in TGTs)
                    {
                        TGT.GetComponent<SlotManager>().storing = true;
                    }
                }
                else
                {
                    other.GetComponent<Items>().BackPosition();
                }
            }

            else if (storing == true && ItemName == "")//格納中でアイテムネーム無し（複数スロットアイテムの範囲内のスロット）
            {
                other.GetComponent<Items>().BackPosition();
            }

            else if (storing == false && ItemName == other.transform.name)//アイテム回転時の挙動
            {
                other.transform.position = this.transform.position;
                other.transform.SetParent(this.transform);
                ItemName = other.transform.name;

                //複数マス目のアイテムの処理
                int horizontalSize = other.GetComponent<Items>().HorizontalItemSize;
                int verticalSize = other.GetComponent<Items>().VerticalItemSize;
                if (RelatedSlots(verticalSize, horizontalSize) != null)
                {
                    List<Transform> TGTs = RelatedSlots(verticalSize, horizontalSize);
                    foreach (Transform TGT in TGTs)
                    {
                        if (TGT.name != this.transform.name)
                        {
                            bool ItemStoring = TGT.GetComponent<SlotManager>().storing;
                            if (ItemStoring == true && TGT.GetComponent<SlotManager>().ItemName != "")
                            {
                                other.GetComponent<Items>().ResetSlotBool();
                                other.GetComponent<Items>().TurnItem();
                                other.GetComponent<Items>().BackPosition();
                            }
                            else
                            {
                                TGT.GetComponent<SlotManager>().storing = true;
                            }
                        }
                        else
                        {
                            TGT.GetComponent<SlotManager>().storing = true;
                        }
                    }
                }
                else
                {
                    other.GetComponent<Items>().BackPosition();
                }
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

                    int horizontalSize = other.GetComponent<Items>().HorizontalItemSize;
                    int verticalSize = other.GetComponent<Items>().VerticalItemSize;
                    if (RelatedSlots(verticalSize, horizontalSize) != null)
                    {
                        List<Transform> TGTs = RelatedSlots(verticalSize, horizontalSize);
                        foreach (Transform TGT in TGTs)
                        {
                            if (TGT.GetComponent<SlotManager>().ItemName == "")
                            {
                                TGT.GetComponent<SlotManager>().storing = false;
                            }
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
    public List<Transform> RelatedSlots(int verticalSize, int horizontalSize)
    {
        List<Transform> TGTs = new List<Transform>();
        GameObject parent = this.transform.parent.gameObject;
        int SlotsCount = parent.transform.childCount;//スロットの数
        int BottomSlotLine;//このエリアはアイテムが格納されるスロットにはできない
        Transform Slots = parent.GetComponentInChildren<Transform>();
        int i = 0;//アイテムが格納されるはずのスロットの場所

        if (parent.transform.name == "InventrySlots")//横列のスロット数取得。できればなくていい
        {
            foreach (Transform slot in Slots)//全スロットの取得
            {
                BottomSlotLine = (SlotsCount - 1) - 5 * (verticalSize - 1);//対象スロットがスロットの最下段からverticalSize分上にあるか
                if (i <= BottomSlotLine)
                {
                    if (slot.name == this.transform.name)//アイテムが格納されるはずのスロットを取得
                    {
                        for (int v = 0; verticalSize > v; v++)//縦方向のサイズ
                        {
                            for (int h = 0; horizontalSize > h; h++)//横方向のサイズ
                            {
                                TGTs.Add(parent.transform.GetChild(h + (5 * v)).transform);//i+5番目のスロットを選択して送り返す
                            }
                        }
                        return TGTs;
                    }
                }
                else if (i > BottomSlotLine)
                {
                    return TGTs = null;
                }
                i++;
            }
        }
        else if (parent.transform.name == "StashSlots")//横列のスロット数取得。できればなくていい
        {
            foreach (Transform slot in Slots)
            {
                if (slot.name == this.transform.name)//現在地の取得
                {
                    BottomSlotLine = SlotsCount - 10 * (verticalSize-1);//対象スロットがスロットの最下段からverticalSize分上にあるか
                    if (i <= BottomSlotLine)
                    {
                        for (int v = 0; verticalSize > v; v++)
                        {
                            for (int h = 0; horizontalSize > h; h++)
                            {
                                TGTs.Add(parent.transform.GetChild((i + h) + (10 * v)).transform);//i+5番目のスロットを選択して送り返す
                            }
                        }
                    }

                }
                i++;
            }
        }
        return TGTs;
    }


}
