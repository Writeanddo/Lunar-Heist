using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public AudioSource startMenuMusic;

    public void StartGame()
    {
        startMenuMusic.volume = 0;
        SceneManager.LoadSceneAsync("OpeningCutscene", LoadSceneMode.Single);
    }

    public void Credits()
    {
        startMenuMusic.volume = 0;
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }


    public void Quit()
    {
        Application.Quit();
    }
}
