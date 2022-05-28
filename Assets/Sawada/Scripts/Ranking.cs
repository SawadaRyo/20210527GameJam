using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : SingletonBehaviour<Ranking>
{
    [SerializeField] GameObject m_rankingPanel = default;
    [SerializeField] GameObject m_rezultPanel = default;
    [SerializeField] Text[] m_scoreText = new Text[10];
    [SerializeField] Text m_isScoreText = default;
    [SerializeField] InputField m_inputFields = default;
    bool m_panelActive = false;
    Dictionary<int, string> m_score = new Dictionary<int, string>();
    void Start()
    {
        m_rankingPanel.SetActive(m_panelActive);
        m_isScoreText.text = string.Format($"{GameManager.Instance.Score}");
    }
    
    public void ScoreSort(int score,string name)
    {
        m_score.Add(score, name);
        m_score.OrderByDescending(x => x.Key).Take(10);
        var arrayScore = m_score.Keys.ToArray();
        for (int i = 0; i < m_score.Count; i++)
        {
            m_scoreText[i].text = string.Format($"{i+1}:{m_score[arrayScore[i]]} {arrayScore[i]}");
        }
    }
    public void PanelAvtive()
    {
        m_panelActive = !m_panelActive;
        m_rankingPanel.SetActive(m_panelActive);
        m_rezultPanel.SetActive(!m_panelActive);
    }
    public void InputRank()
    {
        var name = m_inputFields.text;
        var score = GameManager.Instance.Score;
        ScoreSort(score, name);
        m_inputFields.enabled = false;
    }
}
