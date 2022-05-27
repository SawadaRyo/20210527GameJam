using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    int m_score = 0;
    bool m_isCombo = false;
    void Start()
    {

    }

    void Update()
    {

    }
    public void PlusScore()
    {
        m_score += plusScore;
    }
    IEnumerator Combo()
    {
        while(m_isCombo)
        {

        }
    }
}
