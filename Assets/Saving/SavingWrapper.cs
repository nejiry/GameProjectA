using UnityEngine;

//セーブのキーコンフィグ


    public class SavingWrapper : MonoBehaviour
    {
    const string defaultSaveFile = "save";

    private void Start()
    {
        GetComponent<SavingSystem>().Load(defaultSaveFile);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }
    }
}

