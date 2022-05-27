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
            // countTimeに、ゲームが開始してからの秒数を格納
            countTime -= Time.deltaTime;

            // 小数0桁にして表示
            GetComponent<Text>().text = "Time:" + "  " + countTime.ToString("F0");

            if (countTime < 0)
            {
                GameManager.Instance.SwichGame();
            }
        }
        
    }
}
