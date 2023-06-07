using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public Vector3 itemPosition;


    void OnMouseDown()
    {
        var itemList = new List<string>() { "Cloth(crean)", "Cloth(dirty)", "Stone", "food", "Makimono", "WoodStick", "GoldenKey", "Rope" };
        string itemname = itemList[Random.Range(0, itemList.Count)];
        GameObject obj = (GameObject)Resources.Load("Item/"+itemname);
        GameObject parent = GameObject.Find("InventrySlots");
        Transform Slots = parent.GetComponentInChildren<Transform>();
        foreach (Transform slot in Slots)
        {
            var iteminfomation = slot.GetComponent<SlotManager>();
            if(iteminfomation.storing == false)
            {
                print("generate" + slot);
                var newItem = Instantiate(obj);
                itemPosition = slot.position;
                newItem.transform.position = itemPosition;
                newItem.name = itemname;
                break;
            }
        }
    }
}

