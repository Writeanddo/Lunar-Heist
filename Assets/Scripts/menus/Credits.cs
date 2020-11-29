using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public AudioSource creditsMusic;

    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            creditsMusic.volume = 0;
            SceneManager.LoadScene("StartMenu");
        }
    }
}
