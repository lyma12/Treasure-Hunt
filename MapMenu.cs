using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.IO;

public class MapMenu : MonoBehaviour
{
    // UI
    [SerializeField]
    private GameObject questionBox;
    [SerializeField]
    private GameObject settingBox;
    [SerializeField]
    private Text textShowCoins;
    [SerializeField]
    private Text textShowStars;

    //Object
    [SerializeField]
    private List<MapObject> mapObjects;



    // cuc bo
    private int mapToChangeGroup;
    private string level;
    private int preGroup;
    private int mapNow;
    private DataJson dataUseInScene;


    // IU void
    public void quitQuestionBox()
    {
        questionBox.SetActive(false);
    }
    public void showSettingBox(bool show)
    {
        settingBox.SetActive(show);
    }

    public void changeMapShow(bool left)
    {
        if (left)
        {
            mapNow = (mapNow - 1 < 0) ? mapObjects.Count - 1 : mapNow - 1;
        }
        else
        {
            mapNow = (mapNow + 1 >= mapObjects.Count) ? 0 : mapNow + 1;
        }
        foreach (var map in mapObjects)
        {
            map.gameObject.SetActive(false);
        }
        mapObjects[mapNow].gameObject.SetActive(true);
        dataUseInScene.mapNow = mapNow;
    }

    public void changeGroup(int group)
    {
        if (mapObjects[mapToChangeGroup].groupNow == group)
        {
            SceneManager.LoadScene(level);
        }
        else
        {
            questionBox.SetActive(true);
            preGroup = group;
        }
    }
    public void setMapToChangeGroup(int map)
    {
        mapToChangeGroup = map;
    }
    public void setLevel(string name)
    {
        level = name;
    }


    public void Ans(bool useCoins)
    {
        quitQuestionBox();
        if (useCoins)
        {
            if (dataUseInScene.Coins - 1000 > 0) dataUseInScene.Coins -= 1000;
            load(dataUseInScene);
        }

        MapObject map = mapObjects[mapToChangeGroup];
        map.checkPoint.transform.position = map.boats[preGroup].transform.position;
        dataUseInScene.checkPointMaps[mapToChangeGroup] = map.checkPoint.transform.position;
        dataUseInScene.groupMaps[mapToChangeGroup] = preGroup;
        GameData.saveData(dataUseInScene);
        SceneManager.LoadScene(level);
    }

    public void resetGame()
    {

        DataJson first = GameData.loadData("firstData.json");
        GameData.saveData(first);
        load(first);
        /*
        dataUseInScene = new DataJson();
        dataUseInScene.Coins = 3000;
        dataUseInScene.Stars = 3000;
        dataUseInScene.mapNow = 0;
        dataUseInScene.checkPointMaps = new List<Vector3>();
        dataUseInScene.passWordMaps = new List<string>();
        dataUseInScene.passWordMaps.Add("34567");
        dataUseInScene.passWordMaps.Add("4590");
        dataUseInScene.groupMaps = new List<int>();
        foreach (var i in mapObjects)
        {
            dataUseInScene.checkPointMaps.Add(i.checkPoint.transform.position);
            dataUseInScene.groupMaps.Add(0);
        }
       
        load(dataUseInScene);
        DataStatic.data = dataUseInScene;
        */
    }

    public void load(DataJson dataLoad)
    {
        textShowCoins.text = "Coins: " + dataLoad.Coins.ToString();
        textShowStars.text = "Stars: " + dataLoad.Stars.ToString();
        for (int i = 0; i < dataLoad.checkPointMaps.Count; i++)
        {
            mapObjects[i].checkPoint.transform.position = dataLoad.checkPointMaps[i];
            mapObjects[i].groupNow = dataLoad.groupMaps[i];
        }
        mapNow = dataLoad.mapNow;
        foreach (var i in mapObjects)
        {
            i.gameObject.SetActive(false);
        }
        mapObjects[mapNow].gameObject.SetActive(true);
    }

    private void Start()
    {

        try
        {
            dataUseInScene = GameData.loadData("data.json");
            load(dataUseInScene);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e);
            DataJson t = new DataJson();
            t.Coins = 3000;
            t.Stars = 3000;
            t.mapNow = 0;
            t.checkPointMaps = new List<Vector3>();
            t.passWordMaps = new List<string>();
            t.passWordMaps.Add("34567");
            t.passWordMaps.Add("4590");
            t.groupMaps = new List<int>();
            foreach (var i in mapObjects)
            {
                t.checkPointMaps.Add(i.checkPoint.transform.position);
                t.groupMaps.Add(0);
            }
            string jsonData = JsonUtility.ToJson(t);
            string filePath = Path.Combine(Application.persistentDataPath, "firstData.json");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            File.WriteAllText(filePath, jsonData);
            string fileContent = File.ReadAllText(filePath);
            GameData.saveData(t);
            dataUseInScene = GameData.loadData("data.json");
            load(dataUseInScene);
            /*
            if (DataStatic.data != null) load(DataStatic.data);
            else
            {
                resetGame();
            }*/
        }
    }
}
