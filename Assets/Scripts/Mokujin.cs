using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mokujin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mokujin"))
        {
            GameManager.Data.AddScore(1);
        }
    }
}
