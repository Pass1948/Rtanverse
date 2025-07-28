using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpJumpScoreUI : PopUpUI
{
    GameObject bgLooper;
    [SerializeField] TMP_Text count;// 시간을 표시할 text
    [SerializeField] TMP_Text score;// 점수표시
    GameObject player;

    GameObject miniGame2;
    GameObject jumpOdj;
    static GameObject prfab;

    Transform jumpParent;
    protected override void Awake()
    {
        base.Awake();
        jumpOdj = GameManager.Resource.Load<GameObject>("Prefabs/BackObject/JumpObjects");
    }

    private void OnEnable()
    {
        StartCoroutine(StartRoutine());
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bgLooper = GameObject.FindGameObjectWithTag("BgLooper");
        miniGame2 = GameObject.FindGameObjectWithTag("MiniGame2");
        jumpParent = miniGame2.transform;
        if (prfab == null)
        {
            Debug.Log("또생성됨");
            prfab = GameManager.Resource.Instantiate(jumpOdj, jumpParent);
        }
        else
        {
            GameObject.Destroy(prfab);
            prfab = GameManager.Resource.Instantiate(jumpOdj, jumpParent);
        }
    }

    private void Update()
    {
        score.text = (GameManager.Data.currentBirdScore).ToString();
    }

    IEnumerator StartRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        count.gameObject.SetActive(true);
        count.text = "3";
        yield return new WaitForSeconds(1f);
        GameManager.Data.currentBirdScore = 0;
        count.text = "2";
        yield return new WaitForSeconds(1f);
        count.text = "1";
        yield return new WaitForSeconds(1f);
        count.text = "START";
        yield return new WaitForSeconds(1f);
        bgLooper.GetComponent<BgLooper>().enabled = true;
        player.GetComponent<JumpControl>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
        count.gameObject.SetActive(false);
    }
}
