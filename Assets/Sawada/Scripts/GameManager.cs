using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField] Text m_scoreText = default;
    int m_enemyDeathCounter = 0;
    static float m_score = 0;
    float m_multiplyScore = 1f;
    bool m_isCombo = false;
    bool m_isGame = false;
    public bool IsGame => m_isGame;
    public int Score => (int)m_score;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlusScore(float plusScore)
    {
        if (!m_isCombo)
        {
            m_isCombo = true;
            StartCoroutine(Combo());
        }

        plusScore *= m_multiplyScore;
        m_score += plusScore;
        m_scoreText.text = string.Format($"Score:{(int)m_score}");
        m_enemyDeathCounter++;
    }
    IEnumerator Combo()
    {
        var decreaseCount = 0;
        var interval = 1f;
        //ToDo while‚ÌðŒ‚ðƒvƒŒƒCƒ„[‚ª¶‚«‚Ä‚¢‚éŠÔ‚É‚·‚é
        while (m_isCombo)
        {
            if (m_enemyDeathCounter > 0 && m_enemyDeathCounter <= 5)
            {
                m_multiplyScore = 1.1f;
                decreaseCount = 1;
            }
            else if (m_enemyDeathCounter > 5 && m_enemyDeathCounter <= 10)
            {
                m_multiplyScore = 1.2f;
                interval = 0.8f;
            }
            else if (m_enemyDeathCounter > 10 && m_enemyDeathCounter <= 15)
            {
                m_multiplyScore = 1.5f;
                decreaseCount = 2;
            }
            else if (m_enemyDeathCounter > 15)
            {
                m_multiplyScore = 2f;
                interval = 0.5f;
            }

            m_enemyDeathCounter -= decreaseCount;
            if (m_enemyDeathCounter < 0)
            {
                m_enemyDeathCounter = 0;
                m_multiplyScore = 1f;
                m_isCombo = false;
            }
            yield return new WaitForSeconds(interval);
        }
        yield break;
    }
    public void SwichGame()
    {
        m_isGame = !m_isGame;
    }
    public void Reset()
    {
        m_score = 0;
    }
}
