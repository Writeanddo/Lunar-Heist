using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public List<string> Dialogue;
    public List<Color> WhoIsSpeaking;
    public int textIndex = -1;
    public GameObject TextContainer;
    public TextMeshProUGUI Text;
    public string NextScene;
    public Image Panel;
    public GameObject ClickMe;

    private bool fadingOut;

    void Start()
    {
        Camera.main.backgroundColor = Color.black;
    }

    void Update()
    {
        if (!fadingOut && Input.GetButtonUp("Fire1"))
        {
            Next();
        }
    }

    void FixedUpdate()
    {
        if (fadingOut)
        {
            Panel.color = new Color(Panel.color.r, Panel.color.g, Panel.color.b, Panel.color.a - 0.1f);

            if (Panel.color.a <= 0)
            {
                SceneManager.LoadScene(NextScene);
            }
        }
    }

    private void Next()
    {
        if (!TextContainer.activeSelf)
        {
            textIndex = 0;
            TextContainer.SetActive(true);
            setText();
        }
        else if (textIndex == Dialogue.Count - 1)
        {
            fadingOut = true;
            TextContainer.SetActive(false);
            ClickMe.SetActive(false);
        }
        else
        {
            textIndex += 1;
            setText();
        }
    }

    private void setText()
    {
        Text.text = "\n" + Dialogue[textIndex];
        Text.color = WhoIsSpeaking[textIndex];
    }
}
