using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic{
    public class UseObject : MonoBehaviour
    {

        public bool isClosed = true;
        public Sprite openImage;

        public void UseAnyObject(){
            bool isClosed = false;
            GetComponent<SpriteRenderer>().sprite = openImage;
        }
    }
}
