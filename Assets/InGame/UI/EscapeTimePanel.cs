using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeTimePanel : MonoBehaviour
{
    public bool PopUp = false;
    public bool trigger = false;

    void FixedUpdate()
    {
        if(trigger == true){
            if(PopUp == false){
                if(transform.localPosition.y > 260){
                    transform.Translate(0, -0.1f, 0); 
                    Debug.Log(this.transform.position.y);
                }
                else{
                    Debug.Log(this.transform.position.y);
                    trigger = false;
                    PopUp = true;
                }
            }
            else if(PopUp == true){
                 if(transform.localPosition.y < 320){
                    transform.Translate(0, 0.1f, 0); 
                }
                else{
                    Debug.Log(this.transform.position.y);
                    trigger = false;
                    PopUp = false;
                }
            }
        }

    }

   public void PopUpPanel(){
        trigger = true;
   }
}
