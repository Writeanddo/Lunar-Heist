using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public GameObject Menu;
    void Update()
    {
        if (Input.GetButtonUp("Pause")){
            Debug.Log("Pause");
            Menu.SetActive(!Menu.activeSelf);
        }
    }

    public void Resume()
    {
        Menu.SetActive(false);
    }


    public void Quit()
    {
        Application.Quit();
    }
}
