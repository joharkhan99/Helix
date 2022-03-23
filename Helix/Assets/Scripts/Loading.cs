using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField]
    Image progressBar;
    void Start()
    {
        StartCoroutine(LoadGame());
    }
    IEnumerator LoadGame()
    {
        AsyncOperation gameScene = SceneManager.LoadSceneAsync("Game");
        while (!gameScene.isDone)
        {
            progressBar.fillAmount = Mathf.Clamp01(gameScene.progress / .9f);
            yield return null;
        }
    }
}
