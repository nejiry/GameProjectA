using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackSlot : MonoBehaviour
{
    public bool storing = false;
    public string ItemName;
    public GameObject BackPackSlots;

   void OnTriggerStay2D(Collider2D other)//other アイテム　this スロット
    {
         if (other.transform.tag == "Backpack"){
            GameObject Backpack = other.transform.parent.gameObject;
            Backpack.GetComponent<Items>().trigWorking = true;
            if(other.GetComponent<Equipment>().boxFlag == false){
                Backpack.transform.SetParent(this.transform);
                Backpack.transform.localPosition = new Vector3(-40, 40, 1);
                Backpack.GetComponent<Items>().ItemSet = true;
                ItemName = Backpack.transform.name;
            }
         }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.transform.tag == "Backpack"){
            ItemName = "";
        }
    }

    void Update(){ 
        if(ItemName == "Backpack"){
            BackPackSlots.gameObject.SetActive(true);
        }
        else if(ItemName == ""){
            BackPackSlots.gameObject.SetActive(false);
        }

    }
}
