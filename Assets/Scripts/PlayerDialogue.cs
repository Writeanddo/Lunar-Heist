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
        Text.text = text;
        if (textRemovalWait != null)
        {
            StopCoroutine(textRemovalWait);
        }

        speakSFX.Play();
        textRemovalWait = RemoveText();
        StartCoroutine(textRemovalWait);
    }

    private IEnumerator RemoveText()
    {
        yield return new WaitForSeconds(3f);
        Text.text = "";
    }
}
