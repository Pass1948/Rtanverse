using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class MiniGameSelectUI : WindowUI
{
    GameObject player;
    protected override void Awake()
    {
        base.Awake();
        buttons["FencingGameButton"].onClick.AddListener(() => { FencingSelect(); });
        buttons["FlappyBirdGameButton"].onClick.AddListener(() => { FlappyBirdSelect(); });
        buttons["BackButton"].onClick.AddListener(() => { Back(); });
    }

    private void Start()
    {
        buttons["FencingGameButton"].Select();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void FencingSelect()
    {
        StartCoroutine(FencingSelectLodingRountine());
    }

    public void FlappyBirdSelect()
    {
        StartCoroutine(FlappyBirdLodingRountine());
    }

    public void Back()
    {
        StartCoroutine(UILodingRountine());
    }


    IEnumerator UILodingRountine()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent <PlayerControl>()._activeSpeed = 5f;
        GameManager.UI.CloseWindowUI(this);
    }

    IEnumerator FencingSelectLodingRountine()
    {
        yield return new WaitForSeconds(0.3f);
        GameManager.UI.CloseWindowUI(this);
        GameManager.UI.ShowWindowUI<FencingGameInfo>("Prefabs/UI/FencingGameInfo");
        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator FlappyBirdLodingRountine()
    {
        yield return new WaitForSeconds(0.3f);
        GameManager.UI.CloseWindowUI(this);
        GameManager.UI.ShowWindowUI<FlappyBirdGameInfo>("Prefabs/UI/FlappyBirdGameInfo");
        yield return new WaitForSeconds(0.2f);
    }
}
