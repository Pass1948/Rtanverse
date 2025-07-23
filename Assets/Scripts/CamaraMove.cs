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

    void LateUpdate()       // late�� �ٸ� �۾��� ���� �Ϸ�ȵ� �������� ������Ʈ�ȴ�
    {
        targetPos = player.transform.position + offset;
        this.transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime); // Larp �� ��ü������ �Ÿ��� �־��� ����ŭ ����
    }


}
