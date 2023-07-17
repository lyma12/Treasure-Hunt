using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class TreasurePass  : MonoBehaviour
{
    [SerializeField]
    private Text textPW;

    private DataJson data;
    private string passWordMap;
    
    void Start()
    {
       
        data = GameData.loadData("data.json");
       // data = DataStatic.data;
        passWordMap = data.passWordMaps[data.mapNow];
    }
    public void checkPassWord()
    {
        string pass = textPW.text;
        if(pass != null && pass.Equals(passWordMap) )
        {
            textPW.text = "+ 30000 Coins";
            data.Coins += 30000;
            int passNew = Random.Range(1234, 999999);
            data.passWordMaps[data.mapNow] = passNew.ToString();
            GameData.saveData(data);
            SceneManager.LoadScene("Maps");
        }
        else
        {
            textPW.text = "WRONG!";
            SceneManager.LoadScene("Maps");
        }
    }
    public void deletePassWord()
    {
        string pass = textPW.text;
        if(pass != null && pass.Length > 0)
        {
            int length = pass.Length;
            pass = pass.Substring(0, length - 1);
            textPW.text = pass;
        }
    }
    public void insertPass(int i)
    {
        textPW.text += i.ToString();
    }
}
