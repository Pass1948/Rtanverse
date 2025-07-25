using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwich : MonoBehaviour
{
   public void SceneChange()
    {
        GameManager.Scene.LoadScene("MainScene");
    }
}
