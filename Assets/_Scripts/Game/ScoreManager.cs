using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score;
    private int displayedScore;
    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (displayedScore < score)
            displayedScore++;

        text.text = "Score: " + displayedScore + "\nAvians left: " + GameManager.Instance.avianCount;
	}
}
