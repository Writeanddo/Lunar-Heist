using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerDialogue : MonoBehaviour
{

    public TextMeshPro Text;
    


    void Start()
    {
        Text.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setText(string text)
    {
        Text.text = text;

    }
}
