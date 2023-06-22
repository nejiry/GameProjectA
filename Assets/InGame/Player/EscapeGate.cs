using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EscapeGate : MonoBehaviour
{
    public GameObject TimeCounter;
    public GameObject EscapeTimePanel;
    public float timeRequired;
    public float startTime;
    public TextMeshProUGUI countTimeText;

    void OnTriggerEnter2D(Collider2D other){
        if (other.transform.tag == "EscapeGate"){
            EscapeTimePanel.GetComponent<EscapeTimePanel>().PopUpPanel();
            startTime = TimeCounter.GetComponent<TimeManager>().ingameTimes;
            Debug.Log("EnterGate");
        }
    } 

   void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "EscapeGate"){
            float countTime = startTime - TimeCounter.GetComponent<TimeManager>().ingameTimes;
            countTimeText.SetText("{0:1}",countTime);
            if(countTime > timeRequired){
                GameObject saveSystem = GameObject.Find("TemporarilySaveSystem");
                saveSystem.GetComponent<InGameSavingSystem>().Delete();
                //SceneManager.LoadScene("Menu");
                Debug.Log("Escape");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.transform.tag == "EscapeGate"){
            EscapeTimePanel.GetComponent<EscapeTimePanel>().PopUpPanel();
    }
    }
}
