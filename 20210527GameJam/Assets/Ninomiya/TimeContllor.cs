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
        // countTimeに、ゲームが開始してからの秒数を格納
        countTime -= Time.deltaTime;

        // 小数2桁にして表示
        GetComponent<Text>().text = "Time:" + "  "+ countTime.ToString("F0");
    }
}
