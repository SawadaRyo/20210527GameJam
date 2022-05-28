using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeContllor : MonoBehaviour
{
    [SerializeField] Text m_countDownText = default;
    [SerializeField] Text m_timerText = default;
    [SerializeField] GameObject m_load = default;
    [SerializeField]float m_time = 60;
    [SerializeField]float m_countDown = 3;

    

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsGame)
        {
            if (m_countDown >= 0)
            {
                m_countDownText.enabled = true;
                m_countDown -= Time.deltaTime;
                m_countDownText.text = string.Format($"{(int)m_countDown + 1}");
            }
            else if (m_countDown < 0)
            {
                GameManager.Instance.SwichGame();
            }
        }

        else 
        {
            m_countDownText.enabled = false;
            m_timerText.enabled = true;
            // countTime‚ÉAƒQ[ƒ€‚ªŠJŽn‚µ‚Ä‚©‚ç‚Ì•b”‚ðŠi”[
            m_time -= Time.deltaTime;

            // ¬”0Œ…‚É‚µ‚Ä•\Ž¦
            //m_timerText.text = "Time:" + "  " + m_countTime.ToString("F0");
            m_timerText.text = string.Format($"Time:{(int)m_time}");

            if (m_time < 0)
            {
                GameManager.Instance.SwichGame();
                m_load.GetComponent<LoadScene>().ScenesLoad(3);
            }
        }
    }
}
