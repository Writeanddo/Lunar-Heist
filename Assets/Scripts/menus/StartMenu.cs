using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("MenuOverlay", LoadSceneMode.Single);
    }
}
