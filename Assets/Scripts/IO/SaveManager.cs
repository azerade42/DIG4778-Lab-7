using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public List<ISaveable> SaveableObjects = new List<ISaveable>();
    public static SaveData SaveData;

    private string dataFileName = "data.json";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void OnEnable()
    {
        LoadFromFile();
    }

    private void Start()
    {
        foreach (ISaveable saveable in SaveableObjects)
        {
            saveable.LoadData();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            foreach (ISaveable saveable in SaveableObjects)
            {
                saveable.SaveData();
            }
            SaveToFile();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadFromFile();
            foreach (ISaveable saveable in SaveableObjects)
            {
                saveable.LoadData();
            }
        }
    }

    public void SaveToFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            string jsonSaveData = JsonConvert.SerializeObject(SaveData, settings);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(jsonSaveData);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Exception while saving data to file: {filePath}\n{e}");
        }
    }

    public void LoadFromFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, dataFileName);

        if (!File.Exists(filePath))
        {
            Debug.LogError($"File not found when trying to load data: {filePath}");
            return;
        }

        try
        {
            string jsonSaveData = "";
            
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    jsonSaveData = reader.ReadToEnd();
                }
            }
            
            SaveData = JsonConvert.DeserializeObject<SaveData>(jsonSaveData);
        }
        catch (Exception e)
        {
            Debug.LogError($"Exception while loading data from file: {filePath}\n{e}");
        }
    }
}
