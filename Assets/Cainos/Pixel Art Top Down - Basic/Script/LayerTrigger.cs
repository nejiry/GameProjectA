using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    //when object exit the trigger, put it to the assigned layer and sorting layers
    //used in the stair objects for player to travel between layers
    public class LayerTrigger : MonoBehaviour
    {
        public string layer;//そのオブジェクトの階層
        public string sortingLayer;//そのオブジェクトの階層

//otherはプレイヤー、オブジェクトがトリガーを抜けると、割り当てられたレイヤーに移動し、レイヤーをソートする。
//プレイヤーがレイヤー間を移動するための階段オブジェクトで使用される

        private void OnTriggerExit2D(Collider2D other)
        {
            other.gameObject.layer = LayerMask.NameToLayer(layer);//右上のレイヤー

            other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;//スプライトのレイヤー群の変更

            SpriteRenderer[] srs = other.gameObject.GetComponentsInChildren<SpriteRenderer>();//プレイヤーの子要素のオブジェクトすべてのレイヤーを変える
            foreach ( SpriteRenderer sr in srs)
            {
                sr.sortingLayerName = sortingLayer;
            }
        }
        /*
        
        */

    }
}
