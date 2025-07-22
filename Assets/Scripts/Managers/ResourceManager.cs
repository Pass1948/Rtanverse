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

        if(resource == null) Debug.LogWarning($"ResourceManager2D: ���ҽ� �ε� ���� - {key}");

        else resources.Add(key, resource);

        return resource;
    }

    public T Instantiate<T>(T original, Vector2 position, Quaternion rotation, Transform parent) where T : Object
    {
        Vector3 pos3D = new Vector3(position.x, position.y, 0f);                // 2D ���ӿ����� Z���� 0���� ����
        return Object.Instantiate(original, pos3D, rotation, parent);
    }

    //Vector2 ��ġ�� Quaternion ȸ���� ����
    public T Instantiate<T>(T original, Vector2 position, Quaternion rotation) where T : Object
    {
        return Instantiate<T>(original, position, rotation, null);
    }


    // �θ� Transform�� ����
    public new T Instantiate<T>(T original, Transform parent) where T : Object
    {
        return Instantiate<T>(original, Vector2.zero, Quaternion.identity, parent);
    }
    // �θ� Transform ���� �⺻ ��ġ�� ȸ������ ����
    public T Instantiate<T>(T original) where T : Object
    {
        return Instantiate<T>(original, Vector2.zero, Quaternion.identity, null);
    }

    // ���(path)�� ���� ������Ʈ�� �����ϴ� ���
    public T Instantiate<T>(string path, Vector2 position, Quaternion rotation, Transform parent) where T : Object
    {
        T original = Load<T>(path);
        return Instantiate<T>(original, position, rotation, parent);
    }
    // ��ο� Vector2 ��ġ, ȸ���� ����
    public T Instantiate<T>(string path, Vector2 position, Quaternion rotation) where T : Object
    {
        return Instantiate<T>(path, position, rotation, null);
    }
    // ��ο� �θ� Transform�� ����
    public T Instantiate<T>(string path, Transform parent) where T : Object
    {
        return Instantiate<T>(path, Vector2.zero, Quaternion.identity, parent);
    }
    // ��θ� �����ϰ� �⺻ ��ġ�� ȸ������ ����
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
