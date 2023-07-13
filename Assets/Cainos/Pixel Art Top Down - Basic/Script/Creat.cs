using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic{
    public class Creat : MonoBehaviour
    {
        // 箱が空いたかどうかを記録、テクスチャーの差し替え
        public bool isClosed = true;
        public Sprite openImage;

        void Update(){
            if(isClosed == false){
                GetComponent<SpriteRenderer>().sprite = openImage;
            }
        }
    }
}
