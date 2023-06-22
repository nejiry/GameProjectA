using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

//�Z�[�u���̃t�@�C������

    public class InGameSavingSystem : MonoBehaviour
    {
        private const string extension = ".json";
        private const string saveFile = "TemporarilySaveData";

        public JObject GetState(){
            JObject state = LoadJsonFromFile();
            return state;
        }




        public void TemporarilySaveFileAsJSon(JObject state)
        {
            string path = GetPathFromSaveFile();
            print("Saving to " + path);
            using (var textWriter = File.CreateText(path))
            {
                using (var writer = new JsonTextWriter(textWriter))
                {
                    writer.Formatting = Formatting.Indented;
                    state.WriteTo(writer);
                    print("Saved");
                }
            }
        }
        private string GetPathFromSaveFile()
        {
            return Path.Combine(Application.persistentDataPath, saveFile + extension);
        }

        public void Delete()
        {
            File.Delete(GetPathFromSaveFile());
        }

        private JObject LoadJsonFromFile()
        {
            string path = GetPathFromSaveFile();
            if (!File.Exists(path))
            {
                return new JObject();
            }

            using (var textReader = File.OpenText(path))
            {
                using (var reader = new JsonTextReader(textReader))
                {
                    reader.FloatParseHandling = FloatParseHandling.Double;

                    return JObject.Load(reader);
                }
            }
        }
    }