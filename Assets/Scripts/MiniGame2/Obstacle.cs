using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float highPosY = 1f;
    [SerializeField] float lowPosY = -1f;

    [SerializeField] float holeSizeMin = 1f;
    [SerializeField] float holeSizeMax = 3f;

    [SerializeField] Transform topOdj;
    [SerializeField] Transform bottomOdj;

    [SerializeField] float widthPadding = 4f;

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstaclCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2;

        topOdj.localPosition = new Vector3(0, halfHoleSize);
        bottomOdj.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);

        transform.position = placePosition;

        return placePosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Data.BirdAddScore(1);
        }
    }
}
