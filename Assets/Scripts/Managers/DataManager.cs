using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public int currentFencingScore = 0;
    public int currentBirdScore = 0;

    public List<int> fencingTopScores = new List<int>();
    public List<int> birdTopScores = new List<int>();

    // 허수아비 치기
    public void FencingAddScore(int score)
    {
        currentFencingScore += score;
        Debug.Log("Score: " + currentFencingScore);
    }

    public void FencingRoundScore(int roundScore)
    {
        fencingTopScores.Add(roundScore);

        // 점수 내림차순 정렬
        fencingTopScores = fencingTopScores.OrderByDescending(score => score).ToList();       //OrderByDescending : 특정요소 기준으로 정렬하기 위해 사용

        // 5위 까지만 저장
        if (fencingTopScores.Count > 5)
        {
            fencingTopScores.RemoveAt(fencingTopScores.Count - 1);
        }
    }

    // 플래피 버드
    public void BirdAddScore(int score)
    {
        currentBirdScore += score;
        Debug.Log("Score: " + currentBirdScore);
    }

    public void BirdRoundScore(int roundScore)
    {
        birdTopScores.Add(roundScore);

        birdTopScores = birdTopScores.OrderByDescending(score => score).ToList();       //OrderByDescending : 특정요소 기준으로 정렬하기 위해 사용

        if (birdTopScores.Count > 5)
        {
            birdTopScores.RemoveAt(birdTopScores.Count - 1);
        }
    }
}
