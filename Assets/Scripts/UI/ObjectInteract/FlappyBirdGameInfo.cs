using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdGameInfo : WindowUI
{

    GameObject player;
    GameObject jumpStartPoint;
    protected override void Awake()
    {
        base.Awake();
        buttons["StartButton"].onClick.AddListener(() => { FlappyBirdSelect(); });
        buttons["CancelButton"].onClick.AddListener(() => { Back(); });
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        jumpStartPoint = GameObject.FindGameObjectWithTag("JumpStartPoint");
    }

    public void FlappyBirdSelect()
    {
        Debug.Log("플래피 버드게임 시작");
        StartCoroutine(FlappyBirdSelectLodingRountine());
    }

    public void Back()
    {
        Debug.Log("뒤로가기");
        StartCoroutine(UILodingRountine());
    }
    IEnumerator UILodingRountine()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<PlayerControl>()._activeSpeed = 5f;
        GameManager.UI.CloseWindowUI(this);
    }

    IEnumerator FlappyBirdSelectLodingRountine()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<PlayerControl>().enabled = false;
        player.transform.position = jumpStartPoint.transform.position;
        GameManager.UI.ShowPopUpUI<PopUpJumpScoreUI>("Prefabs/UI/JumpScoreUI");
        GameManager.UI.CloseWindowUI(this);
    }
}
