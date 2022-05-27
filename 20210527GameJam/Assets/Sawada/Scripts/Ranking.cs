using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : SingletonBehaviour<Ranking>
{
    int[] m_scores = new int[10];
    Text[] m_scoreText = new Text[10];
    void Start()
    {
        ScoreSort(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ScoreSort(int score)
    {
        var n = m_scores.Length;

        int h = n / 2;

        while (h > 0)
        {
            for (int i = h; i < n; i++)
            {
                var k = i;

                while (k >= h && score.CompareTo(m_scores[k - h]) > 0)
                {
                    m_scores[k] = m_scores[k - h];
                    k -= h;
                }

                m_scores[k] = score;
            }

            h = h / 2;
        }

        for (int i = 0; i < m_scores.Length; i++)
        {
            //m_scoreText[i].text = string
        }
    }
}
