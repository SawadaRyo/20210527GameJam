using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeContllor : MonoBehaviour
{
    [SerializeField] Text m_countDownText = default;
    [SerializeField] Text m_timerText = default;
    float m_countTime = 60;
    float m_countDown = 3;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.IsGame)
        {
            if(m_countDown >= 0)
            {
                m_countDownText.enabled = true;
                m_countDown -= Time.deltaTime;
                m_countDownText.text = string.Format($"{m_countDown}");
            }
            else if(m_countDown <= -1)
            {
                m_countDownText.enabled = false;
                m_timerText.enabled = true;
                // countTime�ɁA�Q�[�����J�n���Ă���̕b�����i�[
                m_countTime -= Time.deltaTime;

                // ����0���ɂ��ĕ\��
                //m_timerText.text = "Time:" + "  " + m_countTime.ToString("F0");
                m_timerText.text = string.Format($"Time:{m_countTime}");

                if (m_countTime < 0)
                {
                    GameManager.Instance.SwichGame();
                }
            }
        }
    }
}
