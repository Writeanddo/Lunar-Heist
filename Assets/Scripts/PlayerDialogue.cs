using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerDialogue : MonoBehaviour
{

    public TextMeshPro Text;
    private IEnumerator textRemovalWait;

    public AudioSource speakSFX;

    void Start()
    {
        Text.text = "";
    }

    public void setText(string text)
    {
        Debug.Log("Say " + text);
        Text.text = text;
        if (textRemovalWait != null)
        {
            StopCoroutine(textRemovalWait);
        }
        if (speakSFX != null)
        {
            speakSFX.Play();
        }
        int wordCount = text.Split(' ').Length;
        textRemovalWait = RemoveText(wordCount * 0.3f);
        StartCoroutine(textRemovalWait);
    }

    private IEnumerator RemoveText(float lengthToWait)
    {
        yield return new WaitForSeconds(lengthToWait);
        Text.text = "";
    }
}
