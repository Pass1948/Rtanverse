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

        // 점수 내림차순 정렬
        topScores = topScores.OrderByDescending(score => score).ToList();       //OrderByDescending : 특정요소 기준으로 정렬하기 위해 사용

        // 5위 까지만 저장
        if (topScores.Count > 5)
        {
            topScores.RemoveAt(topScores.Count - 1);
        }
    }
}
