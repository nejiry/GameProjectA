using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class InGameSavingEntity : MonoBehaviour
{
    [SerializeField]  public string uniqueIdentifier = "";
    static Dictionary<string, InGameSavingEntity> globalLookup = new Dictionary<string, InGameSavingEntity>();

    public string GetUniqueIdentifier()
    {
        return uniqueIdentifier;
    }

    public void TemporarilySaveCoreSystem(){
        GameObject saveSystem = GameObject.Find("TemporarilySaveSystem");
        JObject state = saveSystem.GetComponent<InGameSavingSystem>().GetState();
        IDictionary<string, JToken> stateDict = state;
        JToken token = this.GetComponent<CreatManager>().SaveItems();
        stateDict[uniqueIdentifier] = token;
        saveSystem.GetComponent<InGameSavingSystem>().TemporarilySaveFileAsJSon(state);
    }

    public void LoadSaveData() 
    {
        GameObject saveSystem = GameObject.Find("TemporarilySaveSystem");
        JObject state = saveSystem.GetComponent<InGameSavingSystem>().GetState();
        IDictionary<string, JToken> stateDict = state;
        string component = uniqueIdentifier;
        if (stateDict.ContainsKey(component)){
            Debug.Log($"{name} Restore {component} =>{stateDict[component].ToString()}");
            this.GetComponent<CreatManager>().RestoreItems(stateDict[component]);
        }
    }
    
    #if UNITY_EDITOR
        private void Update() {
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");
            
            if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            globalLookup[property.stringValue] = this;
        }

    #endif
        private bool IsUnique(string candidate)
        {
            if (!globalLookup.ContainsKey(candidate)) return true;

            if (globalLookup[candidate] == this) return true;

            if (globalLookup[candidate] == null)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            if (globalLookup[candidate].GetUniqueIdentifier() != candidate)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            return false;
        }
    

}