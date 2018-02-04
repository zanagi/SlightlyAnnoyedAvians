using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTest : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(LoadScene("Game"));
	}
	
	private IEnumerator LoadScene(string name)
    {
        yield return new WaitForEndOfFrame();

        LoadingScreen.Instance.LoadScene(name);
    }
}
