using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public bool storing = false;
    public string ItemName;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Item" && other.GetComponent<Items>().boxFlag == false)
        {
            if (storing == false && ItemName == "")//�X���b�g���{���ɋ�Ȃ�
            {
                //�X���b�g�̒�����Ȃ�z������
                other.transform.SetParent(this.transform);

                Vector3 ItemTransform = this.transform.position;
                other.transform.position = ItemTransform;

                ItemName = other.transform.name;
                other.GetComponent<Items>().ItemSet = true;

                //�����}�X�ڂ����A�C�e���̏���
                int horizontalSize = other.GetComponent<Items>().HorizontalItemSize;
                int verticalSize = other.GetComponent<Items>().VerticalItemSize;
                if (RelatedSlots(verticalSize, horizontalSize) != null)
                {
                    List<Transform> TGTs = RelatedSlots(verticalSize, horizontalSize);
                    foreach (Transform TGT in TGTs)
                    {
                        bool ItemStoring = TGT.GetComponent<SlotManager>().storing;
                        if (ItemStoring == true)
                        {
                            other.GetComponent<Items>().BackPosition();
                            Debug.Log("35"+ItemStoring);
                        }
                        else
                        {
                            TGT.GetComponent<SlotManager>().storing = true;
                            Debug.Log(ItemStoring);
                        }
                    }
                }
                else
                {
                    other.GetComponent<Items>().BackPosition();
                    Debug.Log("Items40");
                }
            }

            else if (storing == true && ItemName == other.transform.name)//�A�C�e�������ɓ����Ă��ē����A�C�e���Ȃ�
            {
                //�z�����ݏ���
                other.transform.position = this.transform.position;
                other.transform.SetParent(this.transform);
                ItemName = other.transform.name;

                //�����}�X�ڂ̃A�C�e���̏���
                int horizontalSize = other.GetComponent<Items>().HorizontalItemSize;
                int verticalSize = other.GetComponent<Items>().VerticalItemSize;
                if (RelatedSlots(verticalSize, horizontalSize) != null)
                {
                    List<Transform> TGTs = RelatedSlots(verticalSize, horizontalSize);
                    foreach (Transform TGT in TGTs)
                    {
                        TGT.GetComponent<SlotManager>().storing = true;
                    }
                }
                else
                {
                    other.GetComponent<Items>().BackPosition();
                    Debug.Log("Items60");
                }
            }

            else if (storing == true && ItemName == "")//�i�[���ŃA�C�e���l�[�������i�����X���b�g�A�C�e���͈͓̔��̃X���b�g�j
            {
                other.GetComponent<Items>().BackPosition();
                Debug.Log("Items70");
            }

            else if (storing == false && ItemName == other.transform.name)//�A�C�e����]���̋���
            {
                other.transform.position = this.transform.position;
                other.transform.SetParent(this.transform);
                ItemName = other.transform.name;

                //�����}�X�ڂ̃A�C�e���̏���
                int horizontalSize = other.GetComponent<Items>().HorizontalItemSize;
                int verticalSize = other.GetComponent<Items>().VerticalItemSize;
                if (RelatedSlots(verticalSize, horizontalSize) != null)
                {
                    List<Transform> TGTs = RelatedSlots(verticalSize, horizontalSize);
                    foreach (Transform TGT in TGTs)
                    {
                        if (TGT.name != this.transform.name)
                        {
                            bool ItemStoring = TGT.GetComponent<SlotManager>().storing;
                            if (ItemStoring == true && TGT.GetComponent<SlotManager>().ItemName != "")
                            {
                                other.GetComponent<Items>().ResetSlotBool();
                                other.GetComponent<Items>().TurnItem();
                                other.GetComponent<Items>().BackPosition();
                                Debug.Log("Items100");
                            }
                            else
                            {
                                TGT.GetComponent<SlotManager>().storing = true;
                            }
                        }
                        else
                        {
                            TGT.GetComponent<SlotManager>().storing = true;
                        }
                    }
                }
                else
                {
                    other.GetComponent<Items>().BackPosition();
                    Debug.Log("Items110");
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

                    int horizontalSize = other.GetComponent<Items>().HorizontalItemSize;
                    int verticalSize = other.GetComponent<Items>().VerticalItemSize;
                    if (RelatedSlots(verticalSize, horizontalSize) != null)
                    {
                        List<Transform> TGTs = RelatedSlots(verticalSize, horizontalSize);
                        foreach (Transform TGT in TGTs)
                        {
                            if (TGT.GetComponent<SlotManager>().ItemName == "")
                            {
                                TGT.GetComponent<SlotManager>().storing = false;
                                Debug.Log("SlotManager");
                            }
                        }
                    }
                }
            }
        }
    }

    public void SlotItemDelete()
    {
        if (storing == true)
        {
            GameObject item = transform.GetChild(0).gameObject;
            Destroy(item.gameObject);
            storing = false;
            ItemName = "";
        }
    }


    public List<Transform> RelatedSlots(int verticalSize, int horizontalSize)
    {
        List<Transform> TGTs = new List<Transform>();
        GameObject parent = this.transform.parent.gameObject;
        int horizontalSlotsCount = parent.GetComponent<SlotsBundler>().HorizontalSlotsCount;
        int SlotsCount = parent.transform.childCount;//�X���b�g�̐�
        int BottomSlotLine = (SlotsCount -1) - horizontalSlotsCount * (verticalSize - 1);//���̃G���A�̓A�C�e�����i�[�����X���b�g�ɂ͂ł��Ȃ�
        int RightsideSlotLine;
        int SlotsLine = SlotsCount / horizontalSlotsCount;
        List<int> RightLimitOfSlots = new List<int>();
        Transform Slots = parent.GetComponentInChildren<Transform>();
        int i = 0;//�A�C�e�����i�[�����͂��̃X���b�g�̏ꏊ


            foreach (Transform slot in Slots)//�S�X���b�g�̎擾
            {
                //�E�[�̏���
                for(int l = 0; l < SlotsLine; l++)//1�񂲂Ƃ̃X���b�g���C���ɕ���
                {
                    RightsideSlotLine = (l + 1) * horizontalSlotsCount - 1;//�E�[���w��ARightsideSlotLine - horizontalSize - 1 = nonaggressionSlot
                        for (int rl = 0; rl < horizontalSize - 1; rl++)//�E�[�̃X���b�g����rl�������Ă��炵�ď����������
                        {
                            RightLimitOfSlots.Add(RightsideSlotLine - rl);//��̍ŉE�[�܂œ���ł�������T�C�Y���X���b�g�擾���āA
                        }
                }

                //�ŉ��̏���
                if (i <= BottomSlotLine)//�A�C�e�����i�[�͈͓��Ȃ�Έȉ��̏���
                {
                    if (slot.name == this.transform.name)//�A�C�e�����i�[�����͂��̃X���b�g���擾
                    {
                        if (RightLimitOfSlots.Contains(i) == false)
                        {
                            for (int v = 0; verticalSize > v; v++)//�c�����̃T�C�Y
                            {
                                for (int h = 0; horizontalSize > h; h++)//�������̃T�C�Y
                                {
                                    TGTs.Add(parent.transform.GetChild(i + (h + (horizontalSlotsCount * v)) ).transform);//i+5�Ԗڂ̃X���b�g��I�����đ���Ԃ�
                                }
                            }
                            return TGTs;
                        }
                        else if (RightLimitOfSlots.Contains(i) == true)
                        {
                            return TGTs = null;
                        }
                    }
                }
                else if (i > BottomSlotLine)
                {
                    return TGTs = null;
                }
                i++;
            }
        return TGTs;
    }


}
