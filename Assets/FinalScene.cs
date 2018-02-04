using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScene : MonoBehaviour {

    private Text text;
    private bool waitDone;

    public float waitTime = 2.0f;

	// Use this for initialization
	void Start () {
        text = GetComponentInChildren<Text>();
        text.text = "Congratulations!\nYou've beaten the game with a score of: " + ScoreManager.score;

        StartCoroutine(CheckWait(waitTime));
	}

    private IEnumerator CheckWait(float time)
    {
        yield return new WaitForSeconds(time);

        waitDone = true;
        text.text += "\n\nPress any key to restart the game.";
    }

    private void Update()
    {
        if(waitDone)
        {
            if(Input.anyKeyDown)
            {
                ScoreManager.score = ScoreManager.oldScore = 0;
                LoadingScreen.Instance.LoadScene("Game");
            }
        }
    }
}
