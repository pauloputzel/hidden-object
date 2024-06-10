using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public TextMeshProUGUI percentText;

    private float timeMinLoadCutoff = 0f;

    private bool gameManagerPresente = false;

    private void Start()
    {
        if (GameManager.instance)
        {
            StartCoroutine(LoadYourAsyncScene());
            gameManagerPresente = true;
        }
        else
        {
            gameManagerPresente = false;
        }
    }

    private void Update()
    {
        if (!gameManagerPresente && GameManager.instance)
        {
            gameManagerPresente = true;
            StartCoroutine(LoadYourAsyncScene());
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        timeMinLoadCutoff = Time.time + GameManager.instance.tempoMinimoLoadSegundos;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GameManager.instance.nextSceneToLoad);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f || timeMinLoadCutoff > Time.time)
        {
            percentText.text = asyncLoad.progress < 0.9f ? $"{asyncLoad.progress * 100}%" : "100%";
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
    }
}
