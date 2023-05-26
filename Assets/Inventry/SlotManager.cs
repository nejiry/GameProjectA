using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public bool storing = false;
    public string ItemName;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Item" && storing == false)
            //対象のオブジェクトがアイテムで、スロットに何も入っていないなら
        {
            if (ItemName == "" || ItemName == other.transform.name)
                //アイテムネームが空またはアイテムネーム＝対象のオブジェクトの場合
            {
                if (other.GetComponent<Items>().boxFlag == false)
                    //対象アイテムが放されたら吸い込み処理
                {
                    other.transform.position = this.transform.position;
                    other.transform.SetParent(this.transform);
                    ItemName = other.transform.name;
                    other.GetComponent<Items>().ItemSet = true;
                    storing = true;
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
}
