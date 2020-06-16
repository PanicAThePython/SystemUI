using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string firstlevel;
    public GameObject optionScreen;

    public GameObject loadingScreen, loadingIcon;
    public Text loadingText;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        //SceneManager.LoadScene(firstlevel);

        StartCoroutine(LoadStart());
    }

    public void OpenOptions()
    {
        optionScreen.SetActive(true);
    }
    public void CloseOptions()
    {
        optionScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator LoadStart()
    {
        loadingScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(firstlevel);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= .9f)
            {
                loadingText.text = "Press any key to continue...";
                loadingIcon.SetActive(false);

                if (Input.anyKeyDown)
                {
                    asyncLoad.allowSceneActivation = true;
                    Time.timeScale = 1f;
                }
            }
            yield return null;
        }
    }
}
