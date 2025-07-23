using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{

    private BaseScene curScene;
    public BaseScene CurScene
    {
        get
        {
            if (curScene == null)
                curScene = GameObject.FindObjectOfType<BaseScene>();

            return curScene;
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingCoroutine(sceneName));
    }

    IEnumerator LoadingCoroutine(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;
        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
        while (!oper.isDone)
        {
            yield return null;
        }

        while (CurScene.progress < 1f)
        {
            yield return null;
        }
        CurScene.LoadAsync();
        Time.timeScale = 1f;
        // Scene 로드완료될시 게임이 미리 세팅되어야할 부분이 있다면 해당 주석 아래로 로직추가


        yield return new WaitForSeconds(0.5f);


    }


}
