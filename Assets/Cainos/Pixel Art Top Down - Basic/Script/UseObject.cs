using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic{
    public class UseObject : MonoBehaviour
    {

        public bool isClosed = true;
        public Sprite openImage;
        public Sprite closeImage;

        public void UseAnyObject(){
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            if(isClosed){
                isClosed = false;
                GetComponent<SpriteRenderer>().sprite = openImage;
                collider.enabled = false;
            }
            else if(isClosed == false){
                isClosed = true;
                GetComponent<SpriteRenderer>().sprite = closeImage;
                collider.enabled = true;
            }
        }
    }
}
