using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMove : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()       // late는 다른 작업이 전부 완료된뒤 마지막에 업데이트된다
    {
        this.transform.position = player.transform.position + offset;
    }


}
