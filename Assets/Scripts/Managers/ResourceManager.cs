using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    Dictionary<string, Object> resources = new Dictionary<string, Object>();
    public T Load<T>(string path) where T : Object
    {
        string key = $"{typeof(T)}.{path}";

        if (resources.ContainsKey(key))
            return resources[key] as T;

        T resource = Resources.Load<T>(path);

        if(resource == null) Debug.LogWarning($"ResourceManager2D: 리소스 로드 실패 - {key}");

        else resources.Add(key, resource);

        return resource;
    }

    public T Instantiate<T>(T original, Vector2 position, Quaternion rotation, Transform parent) where T : Object
    {
        Vector3 pos3D = new Vector3(position.x, position.y, 0f);                // 2D 게임에서는 Z축을 0으로 설정
        return Object.Instantiate(original, pos3D, rotation, parent);
    }

    //Vector2 위치와 Quaternion 회전만 지정
    public T Instantiate<T>(T original, Vector2 position, Quaternion rotation) where T : Object
    {
        return Instantiate<T>(original, position, rotation, null);
    }


    // 부모 Transform만 지정
    public new T Instantiate<T>(T original, Transform parent) where T : Object
    {
        return Instantiate<T>(original, Vector2.zero, Quaternion.identity, parent);
    }
    // 부모 Transform 없이 기본 위치와 회전으로 생성
    public T Instantiate<T>(T original) where T : Object
    {
        return Instantiate<T>(original, Vector2.zero, Quaternion.identity, null);
    }

    // 경로(path)에 따라 오브젝트를 생성하는 기능
    public T Instantiate<T>(string path, Vector2 position, Quaternion rotation, Transform parent) where T : Object
    {
        T original = Load<T>(path);
        return Instantiate<T>(original, position, rotation, parent);
    }
    // 경로와 Vector2 위치, 회전만 지정
    public T Instantiate<T>(string path, Vector2 position, Quaternion rotation) where T : Object
    {
        return Instantiate<T>(path, position, rotation, null);
    }
    // 경로와 부모 Transform만 지정
    public T Instantiate<T>(string path, Transform parent) where T : Object
    {
        return Instantiate<T>(path, Vector2.zero, Quaternion.identity, parent);
    }
    // 경로만 지정하고 기본 위치와 회전으로 생성
    public T Instantiate<T>(string path) where T : Object
    {
        return Instantiate<T>(path, Vector2.zero, Quaternion.identity, null);
    }

    public void Destroy(GameObject go)
    {
            GameObject.Destroy(go);
    }

    public void Destroy(GameObject go, float delay)
    {
            GameObject.Destroy(go, delay);
    }

    public void Destroy(Component component)
    {
        Component.Destroy(component);
    }

    public void Destroy(Component component, float delay = 0f)
    {
        Component.Destroy(component, delay);
    }

}
