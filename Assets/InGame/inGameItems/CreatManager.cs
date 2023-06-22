using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class CreatManager : MonoBehaviour
{

    public bool Opened = false;
 
    public void Open()
    {
        if(Opened == true){
            this.GetComponent<InGameSavingEntity>().LoadSaveData();
        }
        else if (Opened == false){
            GenerateInBox();
            Opened = true;
        }
    }


    void GenerateInBox(){
        var itemList = new List<string>() { "Torch" };
        GameObject parent = GameObject.Find("ItemStash");
        Transform Slots = parent.GetComponentInChildren<Transform>();
        int generateCount = 1;//Random.Range(1,5);

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
                        var newItem = Instantiate(obj,Vector3.zero, Quaternion.identity, slot.transform);
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

    public JToken SaveItems()
    {
        GameObject player = GameObject.Find("Player");
        string CreatID = this.GetComponent<InGameSavingEntity>().GetUniqueIdentifier();

        int i = 0;
        JObject state = new JObject();
        IDictionary<string, JToken> stateDict = state;
        GameObject parent = GameObject.Find("ItemStash");
        Transform Slots = parent.GetComponentInChildren<Transform>();
        foreach (Transform slot in Slots)
        {
            var iteminfomation = slot.GetComponent<SlotManager>();
            JObject itemState = new JObject();
            itemState["slot"] = slot.name;
            itemState["item"] = iteminfomation.ItemName;
            if (iteminfomation.ItemName != "")
            {
                Transform Chil = slot.GetChild(0).transform;
                itemState["itemCount"] = Chil.GetComponent<Items>().ItemCount;
            }
            else
            {
                itemState["itemCount"] = 0;
            }
            stateDict[i.ToString()] = itemState;
            i++;
        }
        stateDict["SlotCount"] = i;
        return state;
    }

    public void RestoreItems(JToken state)
    {
        if (state is JObject stateObject)
        {
            IDictionary<string, JToken> inventryDict = stateObject;
            int SlotCount = inventryDict["SlotCount"].ToObject<int>();
            GameObject parent = GameObject.Find("ItemStash");
            for (int i = 0; i < SlotCount; i++) {
               
                string number = i.ToString();
                string SlotName = inventryDict[number]["slot"].ToObject<string>();
                Transform Slot = parent.transform.Find(SlotName);
                string ItemName = inventryDict[number]["item"].ToObject<string>();
                if (ItemName != "") {
                    GameObject obj = (GameObject)Resources.Load("Item/"+ItemName);
                    var iteminfomation = Slot.GetComponent<SlotManager>();
                    if (iteminfomation.ItemName == "")
                    {

                        var newItem = Instantiate(obj,Vector3.zero, Quaternion.identity, Slot.transform);
                        Vector3 itemPosition = Slot.position;
                        newItem.transform.position = itemPosition;
                        newItem.name = ItemName;
                        newItem.GetComponent<Items>().ItemCount = inventryDict[number]["itemCount"].ToObject<int>();
                        
                    }
                    else if(iteminfomation.ItemName != "")
                    {

                        var newItem = Instantiate(obj,Vector3.zero, Quaternion.identity, Slot.transform);
                        Vector3 itemPosition = Slot.position;
                        newItem.transform.position = itemPosition;
                        newItem.name = ItemName;
                        newItem.GetComponent<Items>().ItemCount = inventryDict[number]["itemCount"].ToObject<int>();
                    }
                }
            }
        }
    }
}
