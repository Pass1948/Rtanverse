using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinScoreBoardUI : PopUpUI
{
    [SerializeField] private Transform scoreParent;
    [SerializeField] TMP_Text score;// 점수표시

    GameObject player;
    GameObject fencingCheckPoint;
    GameObject reStratPoint;
    Transform[] children;
    private List<TMP_Text> rankTexts = new List<TMP_Text>();

    private const int MaxRankCount = 5; // 등수 5위 까지

    protected override void Awake()
    {
        base.Awake();
        buttons["StartButton"].onClick.AddListener(() => { FencingSelect(); });
        buttons["CancelButton"].onClick.AddListener(() => { Back(); });
        InitRankTexts();
    }

    private void OnEnable()
    {
        GameManager.Data.FencingRoundScore(GameManager.Data.currentFencingScore);
        score.text = (GameManager.Data.currentFencingScore).ToString();
        DisplayTopScores();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fencingCheckPoint = GameObject.FindGameObjectWithTag("FencingCheckPoint");
        reStratPoint = GameObject.FindGameObjectWithTag("ReStartPoint");
        children = player.GetComponentsInChildren<Transform>(true);
    }

    private void InitRankTexts()
    {
        rankTexts.Clear();
        for (int i = 0; i < MaxRankCount; i++)
        {
            TMP_Text rankText = GameManager.Resource.Instantiate<TMP_Text>("Prefabs/UI/NumText", scoreParent);
            rankTexts.Add(rankText);
        }
    }

    private void DisplayTopScores()
    {
        for (int i = 0; i < MaxRankCount; i++)
        {
            if (i < GameManager.Data.fencingTopScores.Count)
            {
                rankTexts[i].text = $"{i + 1}위: {GameManager.Data.fencingTopScores[i]}점";
            }
            else
            {
                rankTexts[i].text = $"{i + 1}위: -";
            }
        }
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
        foreach (Transform child in children)
        {
            if (child.name == "Front")
            {
                child.gameObject.SetActive(false);
                break;
            }
        }
        player.transform.position = reStratPoint.transform.position;
        GameManager.UI.ClosePopUpUI();
    }

    IEnumerator StartRountine()
    {
        yield return new WaitForSeconds(0.3f);
        player.GetComponent<SpearControl>().enabled = false;
        GameManager.UI.ClosePopUpUI();
        GameManager.UI.ShowPopUpUI<PopUpTimer>("Prefabs/UI/TimerAndScoreUI");
        player.transform.position = fencingCheckPoint.transform.position;
    }
}
