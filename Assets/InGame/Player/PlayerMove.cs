using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GameObject UIMenuBool;
    Rigidbody2D rbody;
    float axisH = 0.0f;
    float axisV = 0.0f;
    public GameObject creat;

    // プレイヤーの動き
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.F))
        {
            MenuToggle(); 
            if(creat != null){
                creat.GetComponent<CreatManager>().Open();
            }
        }
    }

    void FixedUpdate(){
        rbody.velocity = new Vector2(axisH * 3.0f, axisV * 3.0f);
    }

    //クレートを開ける動作
    void OnTriggerStay2D(Collider2D other){
        Debug.Log("enter");
        if(other.transform.tag == "InteractArea"){
            creat = other.transform.parent.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.transform.tag == "InteractArea"){
            creat = null;
        }
    }

    public void MenuToggle(){
        if (UIMenuBool.activeSelf)
            {
            UIMenuBool.SetActive(false);
            }
             else
            {
            UIMenuBool.SetActive(true);
            }
    }
}
