using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMove : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float followSpeed;

    Vector3 offset;
    Vector3 targetPos;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()       // late는 다른 작업이 전부 완료된뒤 마지막에 업데이트된다
    {
        targetPos = player.transform.position + offset;
        this.transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime); // Larp 두 물체사이의 거리를 주어진 수만큼 보간
    }


}
