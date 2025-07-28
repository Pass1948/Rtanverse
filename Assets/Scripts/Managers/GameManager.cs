using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }


    // Managers=========================
    private static ResourceManager resourceManager;
    public static ResourceManager Resource { get { return resourceManager; } }

    private static SceneManager sceneManager;
    public static SceneManager Scene { get { return sceneManager; } }

    private static DataManager dataManager;
    public static DataManager Data { get { return dataManager; } }

    private static PoolManager poolManager;
    public static PoolManager Pool { get { return poolManager; } }

    private static UIManager uiManager;
    public static UIManager UI { get { return uiManager; } }

    private static Soundmanager soundmanager;
    public static Soundmanager Sound { get { return soundmanager; } }


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        InitManagers();
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    private void InitManagers()
    {
        GameObject resourceObj = new GameObject();
        resourceObj.name = "ResourceManager";
        resourceObj.transform.parent = transform;
        resourceManager = resourceObj.AddComponent<ResourceManager>();

        GameObject sceneObj = new GameObject();
        sceneObj.name = "SceneManager";
        sceneObj.transform.parent = transform;
        sceneManager = sceneObj.AddComponent<SceneManager>();

        GameObject dataObj = new GameObject();
        dataObj.name = "DataManager";
        dataObj.transform.parent = transform;
        dataManager = dataObj.AddComponent<DataManager>();

        GameObject poolObj = new GameObject();
        poolObj.name = "PoolManager";
        poolObj.transform.parent = transform;
        poolManager = poolObj.AddComponent<PoolManager>();

        GameObject uiObj = new GameObject();
        uiObj.name = "UIManager";
        uiObj.transform.parent = transform;
        uiManager = uiObj.AddComponent<UIManager>();

        GameObject soundObj = new GameObject();
        soundObj.name = "Soundmanager";
        soundObj.transform.parent = transform;
        soundmanager = soundObj.AddComponent<Soundmanager>();
    }

}
