using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{

    public static SaveManager Instance;
    public SaveData SaveData;

    private string dataFileName = "data.json";
    private List<ISaveable> savedBehaviors = new List<ISaveable>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

    }

    private void OnEnable()
    {
        Load();
    }

    private void OnDisable()
    {
        

        Save();
    }

    private void Start()
    {
        foreach (ISaveable savedBehavior in savedBehaviors)
        {
            savedBehavior.LoadData();
        }
    }

    public void AddToSavedBehaviors(ISaveable savedBehavior) => savedBehaviors.Add(savedBehavior);

    public void Save()
    {
        foreach (ISaveable savedBehavior in savedBehaviors)
        {
            savedBehavior.SaveData();
        }
        
        string filePath = Path.Combine(Application.persistentDataPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            string jsonSaveData = JsonConvert.SerializeObject(SaveData);

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
    public void Load()
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
