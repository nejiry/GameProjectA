using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public bool storing = false;
    public string ItemName;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Item" && storing == false)
            //�Ώۂ̃I�u�W�F�N�g���A�C�e���ŁA�X���b�g�ɉ��������Ă��Ȃ��Ȃ�
        {
            if (ItemName == "" || ItemName == other.transform.name)
                //�A�C�e���l�[������܂��̓A�C�e���l�[�����Ώۂ̃I�u�W�F�N�g�̏ꍇ
            {
                if (other.GetComponent<Items>().boxFlag == false)
                    //�ΏۃA�C�e���������ꂽ��z�����ݏ���
                {
                    other.transform.position = this.transform.position;
                    other.transform.SetParent(this.transform);
                    ItemName = other.transform.name;
                    other.GetComponent<Items>().ItemSet = true;
                    storing = true;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)//�A�C�e�����X���b�g���痣�ꂽ���̏���
    {
        if (other.transform.tag == "Item")
        {
            if (other.GetComponent<Items>().ItemSet == true)
            //���ꂽ�A�C�e�����Z�b�g����Ă�A�C�e���������Ȃ�
            {
                if (ItemName == other.transform.name)
                //���̃A�C�e�����Z�b�g����Ă�A�C�e���Ɠ������O��������
                {
                    storing = false;
                    other.GetComponent<Items>().ItemSet = false;
                    ItemName = "";
                }
            }
        }
    }

    public void SlotItemDelete()
    {
        if (storing == true)
        {
            GameObject item = transform.GetChild(0).gameObject;
            storing = false;
            ItemName = "";
            Destroy(item.gameObject);
        }
    }
    void Update()
    {
        //if (this.transform.GetChild(0).gameObject)
        //{
        //    ItemName = "";
        //}
    }
}
