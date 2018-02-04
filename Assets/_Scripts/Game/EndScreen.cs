using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {

    public static EndScreen Instance { get; private set; }

    [SerializeField]
    private GameObject winScreen, loseScreen;

    [SerializeField]
    private string nextScene;

    // Use this for initialization
    void Start () {
        Instance = this;
	}
	
	public void Show(bool win)
    {
        (win ? winScreen : loseScreen).SetActive(true);
    }

    public void ToNextScene()
    {
        LoadingScreen.Instance.LoadScene(nextScene);
    }

    public void Retry()
    {
        ScoreManager.Reset();
        LoadingScreen.Instance.LoadScene(SceneManager.GetActiveScene().name);
    }
}
