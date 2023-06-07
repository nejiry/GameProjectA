using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatManager : MonoBehaviour
{

    public bool Opened = false;
 
    public void Open()
    {
        if(Opened == true){
            RestoreItems();
        }
        else if (Opened == false){
            GenerateInBox();
        }
    }


    void GenerateInBox(){
        Opened = true;
        var itemList = new List<string>() { "Cloth(crean)", "Cloth(dirty)", "Stone", "food", "Makimono", "WoodStick", "GoldenKey", "Rope" };
        GameObject parent = GameObject.Find("ItemStash");
        Transform Slots = parent.GetComponentInChildren<Transform>();
        int generateCount = 2;//Random.Range(1,5);

        foreach (Transform slot in Slots)
        {
            string itemname = itemList[Random.Range(0, itemList.Count)];
            GameObject obj = (GameObject)Resources.Load("Item/"+itemname);

            if(generateCount > 0){
        
                var iteminfomation = slot.GetComponent<SlotManager>();
                if(iteminfomation.storing == false)
                {
                    int horizontalSize = obj.GetComponent<Items>().HorizontalItemSize;
                    int verticalSize = obj.GetComponent<Items>().VerticalItemSize;
                    List<Transform> TGTs = iteminfomation.RelatedSlots(verticalSize, horizontalSize);
                    if(TGTs != null){
                        var newItem = Instantiate(obj);
                        newItem.transform.SetParent(slot.transform);
                        newItem.transform.position = slot.position;
                        newItem.name = itemname;
                        generateCount -= 1;
                    }
                    else if(TGTs == null){
                        continue;
                    }
                }
                else if (iteminfomation.storing == true){
                    continue;
                }
            }
            else if(generateCount <= 0){
                break;
            }
        }
    }

    void RestoreItems(){
        //セーブデータ読み込み
    }
    
}
