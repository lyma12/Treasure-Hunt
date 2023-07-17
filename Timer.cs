using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float currentTime;
    public float startingTime = 10f;
    private bool start = false;
    public GameObject button;
    public GameObject exit;
    public int ans;

    [SerializeField] Text countdownText;
    [SerializeField] Text ques;
    [SerializeField] MapMenu control;

    public void StartButton()
    {
        button.SetActive(false);
        exit.SetActive(false);
        currentTime = startingTime;
        start = true;
        //
        int x = UnityEngine.Random.Range(-100, 100);
        int y = UnityEngine.Random.Range(-100, 100);
        int ansR = x + y;
        int ansW = UnityEngine.Random.Range(-100, 100);
        if(UnityEngine.Random.Range(0,1) == 0)
        {
            ques.text = x.ToString() + " + " + y.ToString() + " = " + ansR.ToString();
            ans = 1;
        }
        else
        {
            ques.text = x.ToString() + " + " + y.ToString() + " = " + ansR.ToString();
            ans = 0;
        }
        //

    }
    public void checkAns(int i)
    {
        if (i == ans) control.Ans(false);
        else
        {
            currentTime = 0;
            ques.text = "";
            button.SetActive(true);
            exit.SetActive(true);
            start = false;
        }
    }

    void Update()
    {
        if (start)
        {
            currentTime += -1 * Time.deltaTime;
            countdownText.text = "Timer: " + currentTime.ToString("0");
        }

        if (currentTime <= 0)
        {
            currentTime = 0;
            ques.text = "";
            ans = -1;
            button.SetActive(true);
            exit.SetActive(true);
            start = false;
        }
    }
}
