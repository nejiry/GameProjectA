using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

public class JsonSavingEntity : MonoBehaviour
{
    public JToken CaptureAsJtoken()//保存時
    {
        JObject state = new JObject();//新規Jオブジェクトの作成
        IDictionary<string, JToken> stateDict = state;//Jオブジェクトを辞書型に変換
        foreach (IJsonSaveable jsonSaveable in GetComponents<IJsonSaveable>())//各IJsonSaveableを実行して、保存データを集める
        {
            JToken token = jsonSaveable.CaptureAsJToken();//それぞれのCaptureAsJTokenを実行
            string component = jsonSaveable.GetType().ToString();//オブジェクトのタイプ（辞書型やオブジェクト型）をStringに変えてキャッシュ
            Debug.Log($"{name} Capture {component} = {token.ToString()}");//ログの出力
            stateDict[jsonSaveable.GetType().ToString()] = token;//{"オブジェクトタイプ":{"token"}}
        }
        return state;
    }

    public void RestoreFromJToken(JToken s) //読み込み時
    {
        JObject state = s.ToObject<JObject>();//JtokenをJオブジェクトに変換
        IDictionary<string, JToken> stateDict = state;//Jオブジェクトを辞書型に変換
        foreach (IJsonSaveable jsonSaveable in GetComponents<IJsonSaveable>())//IJsonSaveableを実行して、保存データを集める
        {
            string component = jsonSaveable.GetType().ToString();//オブジェクトのタイプ（辞書型やオブジェクト型）をStringに変えてキャッシュ
            if (stateDict.ContainsKey(component))//キーにキャッシュしたのがあれば
            {

                Debug.Log($"{name} Restore {component} =>{stateDict[component].ToString()}");//ログの出力
                jsonSaveable.RestoreFromJToken(stateDict[component]);//それぞれのRestoreFromJTokenで処理
            }
        }
    }
}