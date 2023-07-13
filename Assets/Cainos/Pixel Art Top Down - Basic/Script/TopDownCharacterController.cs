using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;//キャラクターのスピード
        [SerializeField] GameObject UIMenuBool;

        private Animator animator;
        public GameObject interactCreat;
        public GameObject interactObject;
        public GameObject RightPanel;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }


        private void Update()
        {

            //メインカメラ上のマウスカーソルのある位置からRayを飛ばす
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
            if (Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction)){
                //Rayが当たるオブジェクトがあった場合はそのオブジェクト名をログに表示
                Debug.Log(hit2d.collider.gameObject.name);
            }  

            Vector2 dir = Vector2.zero;//Vector2(0,0)をセット
            if(UIMenuBool.activeSelf != true){
                if (Input.GetKey(KeyCode.A))
                {
                    dir.x = -1;
                    animator.SetInteger("Direction", 3);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    dir.x = 1;
                    animator.SetInteger("Direction", 2);
                }

                if (Input.GetKey(KeyCode.W))
            {
                    dir.y = 1;
                    animator.SetInteger("Direction", 1);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    dir.y = -1;
                    animator.SetInteger("Direction", 0);
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if(UIMenuBool.activeSelf){
                    UIMenuBool.SetActive(false);
                }
                else{
                    UIMenuBool.SetActive(true);
                    if(interactObject){
                        RightPanel.SetActive(true);
                        if(interactObject.tag == "ItemCreat"){
                            if(interactObject.GetComponent<Creat>().isClosed){
                                GenerateInBox();
                                interactObject.GetComponent<Creat>().isClosed = false;
                            }
                            else{
                                return;//セーブデータからアイテム読み込み
                            }
                        }
                        else{
                            RightPanel.SetActive(false);//ドアだったり、別のインタラクトアイテムだった場合の処理
                        }
                    }
                    else{
                        RightPanel.SetActive(false);
                    }
                }
            }

            if(interactObject){
                if(Input.GetKeyDown(KeyCode.E)){
                    interactObject.GetComponent<UseObject>().UseAnyObject();
                }
            }

            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }

        ////////////////////////////////////////////////////////////////////////////////
        void GenerateInBox(){
            var itemList = new List<string>() { "Torch" };
            GameObject parent = GameObject.Find("ItemStash");
            Transform Slots = parent.GetComponentInChildren<Transform>();
            int generateCount = 1;//Random.Range(1,5);

            foreach (Transform slot in Slots)
            {
                Debug.Log("here");
                string itemname = itemList[Random.Range(0, itemList.Count)];
                GameObject obj = (GameObject)Resources.Load("Item/"+itemname);
                Debug.Log(obj);

                if(generateCount > 0){
            
                    var iteminfomation = slot.GetComponent<SlotManager>();
                    if(iteminfomation.storing == false)
                    {
                        int horizontalSize = obj.GetComponent<Items>().HorizontalItemSize;
                        int verticalSize = obj.GetComponent<Items>().VerticalItemSize;
                        List<Transform> TGTs = iteminfomation.RelatedSlots(verticalSize, horizontalSize);
                        if(TGTs != null){
                            var newItem = Instantiate(obj,Vector3.zero, Quaternion.identity, slot.transform);
                            newItem.transform.position = slot.position;
                            newItem.name = itemname;
                            generateCount -= 1;
                        }
                        else if(TGTs == null){
                            continue;
                        }
                    }
                    else if (iteminfomation.storing == true){
                        continue;
                    }
                }
                else if(generateCount <= 0){
                    break;
                }
            }
        }



    }
}
