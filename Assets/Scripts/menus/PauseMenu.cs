using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public AudioMixerSnapshot pausedMusic;
    public AudioMixerSnapshot resumeMusic;

    public GameObject Menu;
    void Update()
    {
        if (Input.GetButtonUp("Pause")){
            if (Menu.activeSelf)
            {
                Resume();
            }else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Menu.SetActive(true);
        Time.timeScale = 0;
        pausedMusic.TransitionTo(0.1f);
    }

    public void Resume()
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
        resumeMusic.TransitionTo(0.1f);
    }


    public void Quit()
    {
        Application.Quit();
    }
}
