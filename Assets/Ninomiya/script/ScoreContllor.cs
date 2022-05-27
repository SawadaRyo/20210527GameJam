using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreContllor : MonoBehaviour
{
    public int score;
    public Text scoretext;
    // Start is called before the first frame update
    void Start()
    {
        score = default;
    }
   

    // Update is called once per frame
    void Update()
    {
        scoretext.text = "score:" + "  " + score.ToString();
    }
}
