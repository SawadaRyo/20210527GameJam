using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeContllor : MonoBehaviour
{
    float countTime = 60;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.IsGame)
        {
            // countTime�ɁA�Q�[�����J�n���Ă���̕b�����i�[
            countTime -= Time.deltaTime;

            // ����0���ɂ��ĕ\��
            GetComponent<Text>().text = "Time:" + "  " + countTime.ToString("F0");

            if (countTime < 0)
            {
                GameManager.Instance.SwichGame();
            }
        }
        
    }
}
