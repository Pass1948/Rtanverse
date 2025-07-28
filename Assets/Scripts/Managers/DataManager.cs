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

    // ����ƺ� ġ��
    public void FencingAddScore(int score)
    {
        currentFencingScore += score;
        Debug.Log("Score: " + currentFencingScore);
    }

    public void FencingRoundScore(int roundScore)
    {
        fencingTopScores.Add(roundScore);

        // ���� �������� ����
        fencingTopScores = fencingTopScores.OrderByDescending(score => score).ToList();       //OrderByDescending : Ư����� �������� �����ϱ� ���� ���

        // 5�� ������ ����
        if (fencingTopScores.Count > 5)
        {
            fencingTopScores.RemoveAt(fencingTopScores.Count - 1);
        }
    }

    // �÷��� ����
    public void BirdAddScore(int score)
    {
        currentBirdScore += score;
        Debug.Log("Score: " + currentBirdScore);
    }

    public void BirdRoundScore(int roundScore)
    {
        birdTopScores.Add(roundScore);

        birdTopScores = birdTopScores.OrderByDescending(score => score).ToList();       //OrderByDescending : Ư����� �������� �����ϱ� ���� ���

        if (birdTopScores.Count > 5)
        {
            birdTopScores.RemoveAt(birdTopScores.Count - 1);
        }
    }
}
