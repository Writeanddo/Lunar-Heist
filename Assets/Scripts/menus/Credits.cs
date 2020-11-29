using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            SceneManager.LoadScene("StartMenu");
        }
    }
}
