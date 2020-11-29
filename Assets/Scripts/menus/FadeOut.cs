using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public bool fadingOut = false;
    public string NextScene;
    public Image Panel;
    public bool toOpaque = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fadingOut && !toOpaque)
        {
            Panel.color = new Color(Panel.color.r, Panel.color.g, Panel.color.b, Panel.color.a - 0.1f);

            if (Panel.color.a <= 0)
            {
                onDone();
            }
        }else if (fadingOut && toOpaque)
        {
            Panel.color = new Color(Panel.color.r, Panel.color.g, Panel.color.b, Panel.color.a + 0.1f);

            if (Panel.color.a >= 1)
            {
                onDone();
            }
        }
    }

    public void FadeToScene()
    {
        fadingOut = true;
    }

    private void onDone()
    {
        if (NextScene != "")
        {
            SceneManager.LoadScene(NextScene);
        }
    }

}

