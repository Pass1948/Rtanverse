using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    private void Awake()
    {
        SceneNum = 1;
    }
    protected override IEnumerator LoadingCoroutine()
    {
        progress = 0.0f;
        yield return null;
        progress = 1.0f;
    }
}
