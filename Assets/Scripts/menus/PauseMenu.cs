using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public AudioMixer Mixer;

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
        Mixer.SetFloat("MasterCutoffFrequencyLowPass", 250f);
        Mixer.SetFloat("MasterResonanceLowPass", 0.1f);
    }

    public void Resume()
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
        Mixer.ClearFloat("MasterCutoffFrequencyLowPass");
        Mixer.ClearFloat("MasterResonanceLowPass");

    }


    public void Quit()
    {
        Application.Quit();
    }
}
