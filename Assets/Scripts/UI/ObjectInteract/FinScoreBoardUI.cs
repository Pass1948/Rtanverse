using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinScoreBoardUI : PopUpUI
{
    GameObject player;
    GameObject reStratPoint;
    [SerializeField] private Transform scoreParent;
    private List<TMP_Text> rankTexts;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player");
        reStratPoint = GameObject.FindGameObjectWithTag("ReStartPoint");
        buttons["StartButton"].onClick.AddListener(() => { FencingSelect(); });
        buttons["CancelButton"].onClick.AddListener(() => { Back(); });
    }

    private void OnEnable()
    {
        GameManager.Data.SaveRoundScore(GameManager.Data.currentScore);
        DisplayTopScores();
    }


    public void FencingSelect()
    {
        Debug.Log("펜싱게임 재시작");
        StartCoroutine(StartRountine());
    }

    public void Back()
    {
        Debug.Log("뒤로가기");
        StartCoroutine(UILodingRountine());
    }

    IEnumerator UILodingRountine()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<SpearControl>().enabled = false;
        player.GetComponent<PlayerControl>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        player.transform.position = reStratPoint.transform.position;
        GameManager.UI.ClosePopUpUI();
    }

    IEnumerator StartRountine()
    {
        yield return new WaitForSeconds(0.3f);
        GameManager.UI.ClosePopUpUI();
        yield return new WaitForSeconds(0.2f);
        GameManager.UI.ShowPopUpUI<PopUpTimer>("Prefabs/UI/TimerAndScoreUI");
    }

    private void DisplayTopScores()
    {
        foreach (Transform child in scoreParent)
            Destroy(child.gameObject);

        for (int i = 0; i < GameManager.Data.topScores.Count; i++)
        {
            rankTexts.Add(GameManager.Resource.Instantiate<TMP_Text>("Prefab/UI/NumText", scoreParent));
        }

            for (int i = 0; i < rankTexts.Count; i++)
        {
            if (i < GameManager.Data.topScores.Count)
            {
                rankTexts[i].text = $"{i + 1}위: {GameManager.Data.topScores[i]}점";
            }
            else
            {
                rankTexts[i].text = $"{i + 1}위: -";
            }
        }
    }

}
