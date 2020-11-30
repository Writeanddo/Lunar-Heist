using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public AudioMixer Mixer;

    public GameObject Menu;
    public GameObject ControlsMenu;
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
        ControlsMenu.SetActive(false);
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

    public void Controls()
    {
        ControlsMenu.SetActive(true);
        Menu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
