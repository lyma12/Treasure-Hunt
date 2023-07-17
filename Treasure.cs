using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Treasure : MonoBehaviour
{
    private int coins; 
    private string codeKey;
    [SerializeField]
    private bool lateTreasure;
    [SerializeField]
    private GameObject needKey;
    [SerializeField]
    private GameObject isOpen;
    

    private void Start()
    {
        coins = UnityEngine.Random.Range(100, 500);
    }
    public void setCodeKey(string code)
    {
        codeKey = code;
    }
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        GameObject gameObject = collider2D.gameObject;
        if(gameObject != null && gameObject.CompareTag("Player"))
        {
            if (!lateTreasure)
            {
                Player player = gameObject.GetComponent<Player>();
                var bag = player.getKeys();
                foreach (var i in bag)
                {
                    if (codeKey.Equals(i))
                    {
                        Animator anim = GetComponent<Animator>();
                        anim.SetTrigger("Open");
                        isOpen.SetActive(true);
                        player.setCoins(coins);
                        Invoke("DestroyTreasure", 1.5f);
               
                    }
                }
                needKey.SetActive(true);
            }
            else SceneManager.LoadScene("Treasue");
        }
    }
   
    
    private void DestroyTreasure()
    {
        Destroy(gameObject);
    }
}
