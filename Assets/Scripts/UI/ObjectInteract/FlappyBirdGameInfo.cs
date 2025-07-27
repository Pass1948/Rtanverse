using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdGameInfo : WindowUI
{

    GameObject player;
    GameObject fencingCheckPoint;
    Transform[] children;
    protected override void Awake()
    {
        base.Awake();
        buttons["StartButton"].onClick.AddListener(() => { FencingSelect(); });
        buttons["CancelButton"].onClick.AddListener(() => { Back(); });
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fencingCheckPoint = GameObject.FindGameObjectWithTag("FencingCheckPoint");
        children = player.GetComponentsInChildren<Transform>(true);
    }

    public void FencingSelect()
    {
        Debug.Log("∆ÊΩÃ∞‘¿” Ω√¿€");
        StartCoroutine(FencingSelectLodingRountine());
    }

    public void Back()
    {
        Debug.Log("µ⁄∑Œ∞°±‚");
        StartCoroutine(UILodingRountine());
    }
    IEnumerator UILodingRountine()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.UI.CloseWindowUI(this);
    }

    IEnumerator FencingSelectLodingRountine()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (Transform child in children)
        {
            if (child.name == "Front")
            {
                child.gameObject.SetActive(true);
                break;
            }
        }
        player.GetComponent<PlayerControl>().enabled = false;
        player.transform.position = fencingCheckPoint.transform.position;
        GameManager.UI.ShowPopUpUI<PopUpTimer>("Prefabs/UI/TimerAndScoreUI");
        GameManager.UI.CloseWindowUI(this);
    }

}
