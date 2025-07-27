using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MiniGameSelectUI : WindowUI
{
    GameObject player;
    GameObject fencingCheckPoint;
    Transform[] children;
    protected override void Awake()
    {
        base.Awake();
        buttons["FencingGameButton"].onClick.AddListener(() => { FencingSelect(); });
        buttons["BackButton"].onClick.AddListener(() => { Back(); });
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fencingCheckPoint = GameObject.FindGameObjectWithTag("FencingCheckPoint");
        children = player.GetComponentsInChildren<Transform>(true);
        buttons["FencingGameButton"].Select();
    }

    public void FencingSelect()
    {
        Debug.Log("∆ÊΩÃ∞‘¿” º±≈√");
        StartCoroutine(FencingSelectLodingRountine());
    }

    public void Back()
    {
        Debug.Log("µ⁄∑Œ∞°±‚");
        StartCoroutine(UILodingRountine());
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
        player.GetComponent<SpearControl>().enabled = true;
        GameManager.UI.CloseWindowUI(this);
        player.transform.position = fencingCheckPoint.transform.position;
    }
    IEnumerator UILodingRountine()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.UI.CloseWindowUI(this);
    }
}
