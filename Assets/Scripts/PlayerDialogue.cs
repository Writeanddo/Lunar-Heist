using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Events;

public class PlayerDialogue : MonoBehaviour
{

    public TextMeshPro Text;
    private IEnumerator textRemovalWait;

    public AudioSource speakSFX;

    void OnEnable()
    {
        Text.text = "";
    }

    public void setText(string text, UnityAction onEnd = null)
    {
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
        textRemovalWait = RemoveText(wordCount * 0.4f, onEnd);
        StartCoroutine(textRemovalWait);
    }

    private IEnumerator RemoveText(float lengthToWait, UnityAction onEnd)
    {
        yield return new WaitForSeconds(lengthToWait);
        Text.text = "";
        if (onEnd != null)
        {
            onEnd.Invoke();
        }
    }
}
