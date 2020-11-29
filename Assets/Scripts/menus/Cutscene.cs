using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class Cutscene : MonoBehaviour
{
    public List<string> Dialogue;
    public List<Color> WhoIsSpeaking;
    public int textIndex = -1;
    public GameObject TextContainer;
    public TextMeshProUGUI Text;
    public Image Panel;
    public GameObject ClickMe;

    private bool done;

    public AudioSource voices;
    public List<AudioClip> voiceList;
    public UnityEvent OnComplete;

    void Start()
    {
        UnityEngine.Camera.main.backgroundColor = Color.black;
    }

    void Update()
    {
        if (!done && Input.GetButtonUp("Fire1"))
        {
            Next();
        }
    }

    private void Next()
    {
        if (!TextContainer.activeSelf)
        {
            textIndex = 0;
            TextContainer.SetActive(true);
            setTextAndVoices();
        }
        else if (textIndex == Dialogue.Count - 1)
        {
            OnDoneTalk();
        }
        else
        {
            textIndex += 1;
            setTextAndVoices();
        }
    }

    private void setTextAndVoices()
    {
        Text.text = "\n" + Dialogue[textIndex];
        Text.color = WhoIsSpeaking[textIndex];
        if (voiceList.Count > 0)
        {
            voices.PlayOneShot(voiceList[textIndex]);
        }
    }

    private void OnDoneTalk()
    {
        TextContainer.SetActive(false);
        ClickMe.SetActive(false);
        OnComplete.Invoke();
        done = true;
    }
}
