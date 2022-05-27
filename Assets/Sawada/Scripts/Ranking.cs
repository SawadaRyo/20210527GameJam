using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : SingletonBehaviour<Ranking>
{
    [SerializeField] int[] m_scores = new int[10];
    [SerializeField] Text[] m_scoreText = new Text[10];
    void Start()
    {
        ScoreSort();
    }

    
    void ScoreSort()
    {
        Array.Sort(m_scores);
        Array.Reverse(m_scores);
        for (int i = 0; i < m_scores.Length; i++)
        {
            m_scoreText[i].text = string.Format($"{m_scores[i]}");
        }
    }
    public void ScoreSort(int score)
    {
        var l = m_scores.ToList();
        l.Add(score);
        var m = l.OrderByDescending(x => x).Take(10).ToArray();
        for (int i = 0; i < m_scores.Length; i++)
        {
            m_scoreText[i].text = string.Format($"{m_scores[i]}");
        }
    }
}
