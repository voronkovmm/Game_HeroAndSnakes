using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    static int _totalScore;
    public static int TotalScore
    {
        get {return _totalScore;}
        set
        {
            _totalScore++;
            _text.text = TotalScore.ToString();
        }
    }

    static Text _text;

    void Awake() {
        _text = GameObject.FindGameObjectWithTag("score").transform.Find("text").GetComponent<Text>();
        gameObject.SetActive(true);
    }

    void Start() {
        gameObject.SetActive(false);
    }





}
