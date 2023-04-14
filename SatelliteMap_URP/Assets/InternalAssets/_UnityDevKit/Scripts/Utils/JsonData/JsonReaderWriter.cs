using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityDevkit.Utils.JsonData
{
    public static class JsonReaderWriter<TData>
    {
        private static readonly string JsonFileDirectory = Application.streamingAssetsPath + Path.AltDirectorySeparatorChar + "PlayerData";

        private static string GetFullPath(string fileName)
        {
            return JsonFileDirectory + Path.AltDirectorySeparatorChar + fileName + ".json";
        }

        #region FileValidations
        private static void CheckDirectoryExistence()
        {
            if (!Directory.Exists(JsonFileDirectory))
            {
                Debug.Log("Directory not found. CREATING");
                Directory.CreateDirectory(JsonFileDirectory);
            }
        }

        private static void CheckFileExistence(string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                Debug.Log("File not found. CREATING");
                var fs = File.Create(fullPath);
                fs.Close();
            }
        }

        private static void CheckFilePaths(string fullPath)
        {
            CheckDirectoryExistence();
            CheckFileExistence(fullPath);
        }

        private static bool CanDeserializeFile(string file, out TData data)
        {
            try
            {
                data = JsonConvert.DeserializeObject<TData>(file);
                return true;
            }
            catch(ArgumentException e)
            {
                if (e.Data == null)
                {
                    throw new ArgumentNullException("Json file is empty!");
                }
                else
                {
                    Debug.Log(e.Message);
                }

                data = default;
                return false;
            }
        }
        #endregion FileValidations

        private static string ReadJson(string fileName)
        {
            using StreamReader reader = new StreamReader(GetFullPath(fileName));

            var data = reader.ReadToEnd();

            return data;
        }

        public static async Task SaveDataAsync(JsonHolder<TData> jsonHolder)
        {
            var fullPath = GetFullPath(jsonHolder.fileName);
            CheckFilePaths(fullPath);
            var json = JsonConvert.SerializeObject(jsonHolder.data);

            await using StreamWriter writer = new StreamWriter(fullPath);
            await writer.WriteAsync(json);
        }

        public static TData LoadData(JsonHolder<TData> jsonHolder)
        {
            var fullPath = GetFullPath(jsonHolder.fileName);

            CheckFilePaths(fullPath);

            var json = ReadJson(jsonHolder.fileName);
            
            var deserializable = CanDeserializeFile(json, out var outData);
            
            if (deserializable && jsonHolder.Validate(outData))
            {
                jsonHolder.data = outData;
            }
            else
            {
                jsonHolder.data = jsonHolder.defaultScriptableObject.data;
                var saveTask = Task.Run(async () =>
                {
                    await SaveDataAsync(jsonHolder);
                });
                
            }
            return jsonHolder.data;
        }
    }
}