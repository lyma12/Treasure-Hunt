using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.UI;
using System;

public static class SavePoint
{
    public static Vector3 pointPlayer;
}

public class ControlMain : MonoBehaviour
{
    [SerializeField]
    private GameObject PauseMenu;
    [SerializeField]
    private GameObject panelEndGame;
    [SerializeField]
    private Text textEndGame;
    [SerializeField]
    private string nameLevel;
    [SerializeField]
    private bool levelPass;
   
    public void Pause()
    {
        PauseMenu.SetActive(true);
    }
    public void Countinue()
    {
        PauseMenu.SetActive(false);
        panelEndGame.SetActive(false);
    }
    public void Home()
    {
        PauseMenu.SetActive(false);
        panelEndGame.SetActive(false);
        SceneManager.LoadScene("Maps");

    }

    private void Update()
    {
        if (!levelPass)
        {
            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                panelEndGame.SetActive(true);
                textEndGame.text = "YOU LOSE";
            }
            else
            {
                Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                if (player.getFinishGame())
                {
                    panelEndGame.SetActive(true);
                    textEndGame.text = "YOU PASS";
                }
            }
        }
    }

    public void reset()
    {
        SceneManager.LoadScene(nameLevel);
    }
}
