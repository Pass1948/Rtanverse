using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class MiniGameSelectUI : WindowUI
{

    protected override void Awake()
    {
        base.Awake();
        buttons["FencingGameButton"].onClick.AddListener(() => { FencingSelect(); });
        buttons["BackButton"].onClick.AddListener(() => { Back(); });
    }

    private void Start()
    {
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


    IEnumerator UILodingRountine()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.UI.CloseWindowUI(this);
    }

    IEnumerator FencingSelectLodingRountine()
    {
        yield return new WaitForSeconds(0.3f);
        GameManager.UI.ShowWindowUI<FencingGameInfo>("Prefabs/UI/FencingGameInfo");
        yield return new WaitForSeconds(0.2f);
        GameManager.UI.CloseWindowUI(this);
    }
}
