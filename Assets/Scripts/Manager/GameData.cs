using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using System;


public class GameData : MonoBehaviour
{
    [Serializable]
    public class SaveData
    {
        public List<int> addID = new List<int>();
        public List<int> inventoryItemsAmount = new List<int>();
        public List<string> inventoryItemsName = new List<string>();
    }
    public SaveData saveData;
    public static GameData instance;
    private void Awake()
    {
        if (instance == null) 
        { 
           
         DontDestroyOnLoad(this.gameObject);
            instance = this;
       
        }
        else if (instance != null)
        {
            Destroy(instance.gameObject);
            instance = this;
        }

    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath+"/player.dat",FileMode.Create);
        SaveData data = new SaveData();
        data = saveData;
        formatter.Serialize(file, data);    
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/player.dat"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
            saveData=formatter.Deserialize(file) as SaveData;
            file.Close();   


        }
    }
    public void ClearData()
    {
        if(File.Exists(Application.persistentDataPath + "/player.dat"))
        {
            File.Delete(Application.persistentDataPath + "/player.dat");
        }
    }
    public void ClearAllDataList()
    {
        saveData.addID.Clear();
        saveData.inventoryItemsName.Clear();    
        saveData.inventoryItemsAmount.Clear();
        Save();
    }
}
