using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class InteractObjectTrigger : MonoBehaviour
    {
        public bool trigger = false;
        private GameObject obj;

        private void Start(){
            obj =  this.transform.parent.gameObject;
        }

        private void OnTriggerEnter2D(Collider2D collider){
            if(collider.gameObject.tag == "Player"){
                trigger = true;
                collider.GetComponent<TopDownCharacterController>().interactObject = obj;
            }
        }

        private void OnTriggerExit2D(Collider2D collider){
            if(collider.gameObject.tag == "Player"){
                trigger = false;
                collider.GetComponent<TopDownCharacterController>().interactObject = null;
            }
        }
    }
}