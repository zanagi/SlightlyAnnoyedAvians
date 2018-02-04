using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// A simple loading screen script
public class LoadingScreen : MonoBehaviour {

    private enum LoadingState
    {
        None, Start, Load, End
    }

    public static LoadingScreen Instance { get; private set; }

    [SerializeField]
    private Image background;
    private string targetSceneName;
    
    [SerializeField]
    private float loadTime = 1.0f;
    private float currentLoadTime;
    private AsyncOperation loadingOperation;
    private LoadingState loadingState;
    // private LoadSceneMode loadingMode = LoadSceneMode.Single;

    private void Start()
    {
        if(Instance)
        {
            return;
        }
        Instance = this;

        SetBackgroundAlpha(0.0f);
        background.gameObject.SetActive(false);

        if (loadTime <= 0.0f)
            loadTime = 1.0f;
    }

    private void FixedUpdate () {
		if(loadingState == LoadingState.Start)
            HandleStart();
        else if(loadingState == LoadingState.Load)
            HandleLoad();
        else if(loadingState == LoadingState.End)
            HandleEnd();
	}

    public bool skipFade = false;
    private void HandleStart()
    {
        currentLoadTime += Time.fixedDeltaTime;
        SetBackgroundAlpha(currentLoadTime / loadTime);

        if (skipFade || currentLoadTime >= loadTime)
        {
            loadingOperation = SceneManager.LoadSceneAsync(targetSceneName);
            loadingState = LoadingState.Load;
            currentLoadTime = 0.0f;
        }

    }

    private void HandleLoad()
    {
        if (loadingOperation == null)
        {
            loadingOperation = SceneManager.LoadSceneAsync(targetSceneName);
            return;
        }

        if (loadingOperation.isDone)
        {
            loadingState = LoadingState.End;
        }
    }

    private void HandleEnd()
    {
        currentLoadTime += Time.fixedDeltaTime;
        SetBackgroundAlpha(1.0f - currentLoadTime / loadTime);

        if (skipFade || currentLoadTime >= loadTime)
        {
            loadingState = LoadingState.None;
            currentLoadTime = 0.0f;
            background.gameObject.SetActive(false);
        }
    }

    private void SetBackgroundAlpha(float a)
    {
        Color color = background.color;
        color.a = a;
        background.color = color;
    }

    public void LoadScene(string sceneName)
    {
        if (IsLoading)
            return;

        loadingState = LoadingState.Start;
        currentLoadTime = 0.0f;
        targetSceneName = sceneName;

        // Activate "black" background
        background.gameObject.SetActive(true);
    }

    public bool IsLoading
    {
        get { return loadingState != LoadingState.None; }
    }
}
