using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlinkerScript : MonoBehaviour
{

    [SerializeField]private float _speed = 0;
    private Text _text;
    private float _time;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.color = GetAlphaColor(_text.color);
    }

    private Color GetAlphaColor(Color color) 
    {
        _time += Time.deltaTime * 5f * _speed;

        color.a = Mathf.Sin(_time);
                
        return color;
    }
}
