using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BirdScoreBoardUI : PopUpUI
{
    [SerializeField] private Transform scoreParent;
    [SerializeField] TMP_Text score;// 점수표시
    GameObject bgLooper;
    GameObject player;
    GameObject jumpStartPoint;
    GameObject reStratPoint;

    private List<TMP_Text> rankTexts = new List<TMP_Text>();

    private const int MaxRankCount = 5; // 등수 5위 까지

    protected override void Awake()
    {
        base.Awake();
        buttons["StartButton"].onClick.AddListener(() => { BirdSelect(); });
        buttons["CancelButton"].onClick.AddListener(() => { Back(); });
        InitRankTexts();
    }

    private void OnEnable()
    {
        GameManager.Data.BirdRoundScore(GameManager.Data.currentBirdScore);
        score.text = (GameManager.Data.currentBirdScore).ToString();
        DisplayTopScores();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        jumpStartPoint = GameObject.FindGameObjectWithTag("JumpStartPoint");
        reStratPoint = GameObject.FindGameObjectWithTag("ReStartPoint");
        bgLooper = GameObject.FindGameObjectWithTag("BgLooper");
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
            if (i < GameManager.Data.birdTopScores.Count)
            {
                rankTexts[i].text = $"{i + 1}위: {GameManager.Data.birdTopScores[i]}점";
            }
            else
            {
                rankTexts[i].text = $"{i + 1}위: -";
            }
        }
    }
    public void BirdSelect()
    {
        StartCoroutine(StartRountine());
    }

    public void Back()
    {
        StartCoroutine(BridUILodingRountine());
    }
    IEnumerator BridUILodingRountine()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.UI.ClosePopUpUI();
        player.GetComponent<JumpControl>().enabled = false;
        player.GetComponent<PlayerControl>().enabled = true;
        player.transform.position = reStratPoint.transform.position;
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator StartRountine()
    {
        yield return new WaitForSeconds(0.3f);
        bgLooper.GetComponent<BgLooper>().enabled = false;
        player.GetComponent<JumpControl>().enabled = false;
        player.transform.position = jumpStartPoint.transform.position;
        player.GetComponent<JumpControl>().playerDead = false;
        yield return new WaitForSeconds(0.1f);
        GameManager.UI.ClosePopUpUI();
        GameManager.UI.ShowPopUpUI<PopUpJumpScoreUI>("Prefabs/UI/JumpScoreUI");
    }

}
