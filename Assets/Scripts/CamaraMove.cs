using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMove : MonoBehaviour
{
    [SerializeField] float followSpeed;

   public GameObject player { get; set; }

    Vector3 offset;
    Vector3 targetPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);  // �ɸ��� ������Ʈ ����ɽ� ��ġ ��ȯ
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()       // late�� �ٸ� �۾��� ���� �Ϸ�ȵ� �������� ������Ʈ�ȴ�
    {
        targetPos = player.transform.position + offset;
        this.transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime); // Larp �� ��ü������ �Ÿ��� �־��� ����ŭ ����
    }
}
