using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    [SerializeField] int numBgCount = 5;
    [SerializeField] int obstacleCount = 0;
    [SerializeField] Vector3 obstacleLastPosition = Vector3.zero;

    private void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject+25;
            collision.transform.position = pos;
            return;
        }

        Obstacle obstacles = collision.gameObject.GetComponent<Obstacle>();
        if (obstacles)
        {
            obstacleLastPosition = obstacles.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }



}
