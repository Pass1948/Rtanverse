using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpUI : MonoBehaviour
{
    [SerializeField] GameObject uiObj;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (uiObj == null)
        {
            Debug.Log("UI가 없어요");
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
            uiObj.SetActive(true);
        else
            uiObj.SetActive(false);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (uiObj == null)
        {
            Debug.Log("UI가 없어요");
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
            uiObj.SetActive(false);
    }



}
