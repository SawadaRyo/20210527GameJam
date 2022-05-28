using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : SingletonBehaviour<Ranking>
{
    [SerializeField] List<int> m_scores = new List<int>(10);
    [SerializeField] Text[] m_scoreText = new Text[10];
    [SerializeField] Text m_bestScore = default;
    //Dictionary<int, string> m_score = new Dictionary<int, string>();
    void Start()
    {
        //ScoreSort();
    }
    void Update()
    {
        if (m_bestScore != null && m_bestScore.enabled)
        {
            m_bestScore.text = String.Format($"{m_scores[0]}");
        }
        else return;
    }


    void ScoreSort()
    {
        var s = m_scoreText.ToArray();
        Array.Sort(m_scoreText);
        Array.Reverse(m_scoreText);
        for (int i = 0; i < m_scores.Count; i++)
        {
            m_scoreText[i].text = string.Format($"{m_scores[i]}");
        }
    }
    public void ScoreSort(int score)
    {
        var l = m_scores.ToList();
        l.Add(score);
        var m = l.OrderByDescending(x => x).Take(10).ToList();
        m_scores = m;
        for (int i = 0; i < m_scores.Count; i++)
        {
            m_scoreText[i].text = string.Format($"{m_scores[i]}");
        }

        //var l = m_score.ToDictionary(x => x.Key, x => x.Value);
        //l.Add(score, name);
        //var m = l .OrderByDescending(x => x.Key).Take(10).ToDictionary(x => x.Key, x => x.Value);
        //m_score = m;
        //for (int i = 0; i < m_scores.Count; i++)
        //{
        //    m_scoreText[i].text = string.Format($"{m_score.Keys}");
        //}
    }
}
