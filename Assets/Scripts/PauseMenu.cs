using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject optionsScreen, pauseScreen;

    public string mainMenu;

    private bool isPaused;

    public GameObject loadingScreen, loadingIcon;
    public Text loadingText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if (!isPaused)
        {
            pauseScreen.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
        
    }

    public void Closeoptions()
    {
        optionsScreen.SetActive(false);
    }

    public void QuitMainMenu()
    {
        //SceneManager.LoadScene(mainMenu);
        //Time.timeScale = 1f;

        StartCoroutine(LoadMain());
    }

    public IEnumerator LoadMain()
    {
        loadingScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenu);
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
