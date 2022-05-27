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
            // countTime‚ÉAƒQ[ƒ€‚ªŠJn‚µ‚Ä‚©‚ç‚Ì•b”‚ğŠi”[
            countTime -= Time.deltaTime;

            // ¬”0Œ…‚É‚µ‚Ä•\¦
            GetComponent<Text>().text = "Time:" + "  " + countTime.ToString("F0");

            if (countTime < 0)
            {
                GameManager.Instance.SwichGame();
            }
        }
        
    }
}
