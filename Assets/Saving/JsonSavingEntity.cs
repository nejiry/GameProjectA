using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

public class JsonSavingEntity : MonoBehaviour
{
    public JToken CaptureAsJtoken()//�ۑ���
    {
        JObject state = new JObject();//�V�KJ�I�u�W�F�N�g�̍쐬
        IDictionary<string, JToken> stateDict = state;//J�I�u�W�F�N�g�������^�ɕϊ�
        foreach (IJsonSaveable jsonSaveable in GetComponents<IJsonSaveable>())//�eIJsonSaveable�����s���āA�ۑ��f�[�^���W�߂�
        {
            JToken token = jsonSaveable.CaptureAsJToken();//���ꂼ���CaptureAsJToken�����s
            string component = jsonSaveable.GetType().ToString();//�I�u�W�F�N�g�̃^�C�v�i�����^��I�u�W�F�N�g�^�j��String�ɕς��ăL���b�V��
            Debug.Log($"{name} Capture {component} = {token.ToString()}");//���O�̏o��
            stateDict[jsonSaveable.GetType().ToString()] = token;//{"�I�u�W�F�N�g�^�C�v":{"token"}}
        }
        return state;
    }

    public void RestoreFromJToken(JToken s) //�ǂݍ��ݎ�
    {
        Debug.Log("JsonSavingEntity");
        JObject state = s.ToObject<JObject>();//Jtoken��J�I�u�W�F�N�g�ɕϊ�
        IDictionary<string, JToken> stateDict = state;//J�I�u�W�F�N�g�������^�ɕϊ�
        foreach (IJsonSaveable jsonSaveable in GetComponents<IJsonSaveable>())//IJsonSaveable�����s���āA�ۑ��f�[�^���W�߂�
        {
            string component = jsonSaveable.GetType().ToString();
            Debug.Log(component);
            if (stateDict.ContainsKey(component))//�L�[�ɃL���b�V�������̂������
            {

                Debug.Log($"{name} Restore {component} =>{stateDict[component].ToString()}");//���O�̏o��
                jsonSaveable.RestoreFromJToken(stateDict[component]);//���ꂼ���RestoreFromJToken�ŏ���
            }
        }
    }
}