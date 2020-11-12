using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public AudioSource playGameSound;
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("MenuOverlay", LoadSceneMode.Single);
        playGameSound.Play();
    }
}
