using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

//âΩÇÉZÅ[ÉuÇ∑ÇÈÇ©

public class Stash : MonoBehaviour, IJsonSaveable
{
    public JToken CaptureAsJToken()
    {
        int i = 0;
        JObject state = new JObject();
        IDictionary<string, JToken> stateDict = state;
        GameObject parent = GameObject.Find("StashSlots");
        
        Transform Slots = parent.GetComponentInChildren<Transform>();

        foreach (Transform slot in Slots)
        {
            var iteminfomation = slot.GetComponent<SlotManager>();

            JObject itemState = new JObject();
            itemState["slot"] = slot.name;
            itemState["item"] = iteminfomation.ItemName;
            if (iteminfomation.ItemName != "")
            {
                GameObject Chil = slot.GetChild(0).gameObject;
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

    public void RestoreFromJToken(JToken s)
    {
        if (s is JObject stateObject)
        {
            IDictionary<string, JToken> stateDict = stateObject;
            JToken inventryDict = new JObject();
            inventryDict = stateDict["Stash"];
            int SlotCount = inventryDict["SlotCount"].ToObject<int>();
            GameObject parent = GameObject.Find("StashSlots");
            for (int i = 0; i < SlotCount; i++)
            {
                string number = i.ToString();
                string SlotName = inventryDict[number]["slot"].ToObject<string>();
                Transform Slot = parent.transform.Find(SlotName);

                string ItemName = inventryDict[number]["item"].ToObject<string>();
                if (ItemName != "")
                {
                    GameObject obj = (GameObject)Resources.Load(ItemName);
                    var iteminfomation = Slot.GetComponent<SlotManager>();
                    if (iteminfomation.ItemName == "")
                    {
                        var newItem = Instantiate(obj);
                        Vector3 itemPosition = Slot.position;
                        newItem.transform.position = itemPosition;
                        newItem.name = ItemName;
                        newItem.GetComponent<Items>().ItemCount = inventryDict[number]["itemCount"].ToObject<int>();
                    }
                    else if (iteminfomation.ItemName != "")
                    {
                        Slot.GetComponent<SlotManager>().SlotItemDelete();
                        var newItem = Instantiate(obj);
                        Vector3 itemPosition = Slot.position;
                        newItem.transform.position = itemPosition;
                        newItem.name = ItemName;
                        newItem.GetComponent<Items>().ItemCount = inventryDict[number]["itemCount"].ToObject<int>();
                    }
                }
                else if (ItemName == "")
                {
                    Slot.GetComponent<SlotManager>().SlotItemDelete();
                }
            }
        }


    }
}
