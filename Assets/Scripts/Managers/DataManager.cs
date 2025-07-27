using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public int currentScore = 0;

    public List<int> topScores = new List<int>();

    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Score: " + currentScore);
    }
    public void SaveRoundScore(int roundScore)
    {
        topScores.Add(roundScore);

        // ���� �������� ����
        topScores = topScores.OrderByDescending(score => score).ToList();       //OrderByDescending : Ư����� �������� �����ϱ� ���� ���

        // 5�� ������ ����
        if (topScores.Count > 5)
        {
            topScores.RemoveAt(topScores.Count - 1);
        }
    }
}
