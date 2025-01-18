using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] Slider _progressBar;
    [SerializeField] TMP_Text _progressText;

    [SerializeField] GameObject _enableWhileLoading;

    [SerializeField] List<GameObject> _disableWhileLoading;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    public void LoadSync(string targetScene)
    {
        foreach (GameObject panel in _disableWhileLoading)
        {
            panel.SetActive(false);
        }

        SceneManager.LoadScene(targetScene);
    }

    public void LoadAsync(string targetScene)
    {
        foreach (GameObject panel in _disableWhileLoading)
        {
            panel.SetActive(false);
        }

        StartCoroutine(LoadSceneAsync(targetScene));
    }

    IEnumerator LoadSceneAsync(string targetScene)
    {
        _enableWhileLoading?.SetActive(true);
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);
        asyncLoad.allowSceneActivation = false;

        float fakeProgress = 0;
        while (!asyncLoad.isDone)
        {
            fakeProgress += Time.deltaTime * 0.3f;
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            _progressText.text = fakeProgress.ToString("p2");

            _progressBar.value = fakeProgress;
            if (fakeProgress >= 1f && progress >= 1f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}