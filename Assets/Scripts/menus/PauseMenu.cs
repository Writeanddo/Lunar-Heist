using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
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
    }

    public void Resume()
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
    }


    public void Quit()
    {
        Application.Quit();
    }
}
