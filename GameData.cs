using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;



public class DataJson
{
    public int Coins;
    public int Stars;
    public int mapNow;
    public List<string> passWordMaps; 
    public List<Vector3> checkPointMaps ;
    public List<int> groupMaps;
}

// file firstData.json; file data.json;

public static class GameData
{
    
    public static DataJson loadData(string nameFile)
    {
        string filePath = Path.Combine(Application.persistentDataPath, nameFile);
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            DataJson loadedData = JsonUtility.FromJson<DataJson>(jsonData);
            return loadedData;
        }
        else
        {
            
            return null;
        }
    }
    
    public static void saveData(DataJson data)
    {
        //DataJson json = DataToJson(data);
        string jsonData = JsonUtility.ToJson(data);
        string filePath = Path.Combine(Application.persistentDataPath, "data.json");
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        File.WriteAllText(filePath, jsonData);
        string fileContent = File.ReadAllText(filePath);
    }
    
}
