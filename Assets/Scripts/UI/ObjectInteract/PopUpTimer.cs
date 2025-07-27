using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpTimer : PopUpUI
{
    [SerializeField] TMP_Text text_time;// 시간을 표시할 text
    [SerializeField] TMP_Text count;// 시간을 표시할 text
    [SerializeField] TMP_Text score;// 점수표시
    [SerializeField] float inPutTime; // 시간.
    float time;
    GameObject player;
    private bool TimeOut = true;
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        StartCoroutine(StartRoutine());
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        TimerCheck();
    }

    void TimerCheck()
    {
        if (TimeOut)return;
        else
        {
            if (time > 0)  
            {
                time -= Time.deltaTime;
                text_time.text = (time).ToString("N2");
                score.text = (GameManager.Data.currentScore).ToString();
            }

            else   // TimeOut
            {
                text_time.text = (time).ToString("N2");
                TimeOut = true;
                player.GetComponent<SpearControl>().enabled = false;
                GameManager.UI.ClosePopUpUI();
                GameManager.UI.ShowPopUpUI<FinScoreBoardUI>("Prefabs/UI/FinScoreBoardUI");
            }
        }
    }

    IEnumerator StartRoutine()
    {
        GameManager.Data.currentScore = 0;
        count.gameObject.SetActive(true);
        time = inPutTime;
        count.text = "3";
        yield return new WaitForSeconds(1f);
        count.text = "2";
        yield return new WaitForSeconds(1f);
        count.text = "1";
        yield return new WaitForSeconds(1f);
        count.text = "START";
        yield return new WaitForSeconds(1f);
        player.GetComponent<SpearControl>().enabled = true;
        count.gameObject.SetActive(false);
        TimeOut = false;
    }
}
