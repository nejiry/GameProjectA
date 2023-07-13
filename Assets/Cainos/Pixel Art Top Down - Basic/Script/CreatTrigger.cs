using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic{
    public class CreatTrigger : MonoBehaviour
    {
        public bool trigger = false;
        private GameObject obj;

        private void Start(){
            obj =  this.transform.parent.gameObject;
        }

        private void OnTriggerEnter2D(Collider2D collider){
            if(collider.gameObject.tag == "Player"){
                trigger = true;
                Debug.Log(obj.GetComponent<InGameSavingEntity>().uniqueIdentifier);
                collider.GetComponent<TopDownCharacterController>().interactCreat = obj;
            }
        }

        private void OnTriggerExit2D(Collider2D collider){
            if(collider.gameObject.tag == "Player"){
                trigger = false;
                collider.GetComponent<TopDownCharacterController>().interactCreat = null;
            }
        }
        
    }
}
