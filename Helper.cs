using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Helper : MonoBehaviour
{
    [SerializeField]
    private Text textShowPass;
    [SerializeField]
    private GameObject panelChat;
    [SerializeField]
    private GameObject treaSure;

    private bool angryPeople;

    private Player player;
    private DataJson data;
    private string passWord;

    private void Start()
    {
        
        data = GameData.loadData("data.json");

        //DataStatic.data = data;
        passWord = data.passWordMaps[data.mapNow];
    }
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        angryPeople = UnityEngine.Random.Range(0, 3) == 0;
        if (collider2D.gameObject.CompareTag("Player"))
        {
            player = collider2D.gameObject.GetComponent<Player>();
            if (!angryPeople) panelChat.SetActive(true);
            else player.transform.position = SavePoint.pointPlayer;
        }
    }

    public void quitHome()
    {
        panelChat.SetActive(false);
        treaSure.SetActive(false);
        player.transform.position = SavePoint.pointPlayer;
    }
    public void suportHealth()
    {
        if (player.getCoins() > 10)
        {
            player.exceptCoins(10);
            player.TakeDamage(100f);
        }
    }

    public void showTreasurePass()
    {
        UnityEngine.Debug.Log("hi");
        UnityEngine.Debug.Log(player.getStars());
        if (player.getStars() > 100)
        {
            player.exceptStars(100);
            treaSure.SetActive(true);
            textShowPass.text = passWord;
        }
    }
}
