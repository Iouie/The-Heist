using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour {
    TextMeshProUGUI score;
    public int scoreValue;

    void Start()
    {
        score = GetComponent<TextMeshProUGUI>();
        scoreValue = 0;
    }

    void Update()
    {
        score.text = "" + scoreValue;
    }
}
