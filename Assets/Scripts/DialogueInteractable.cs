using UnityEngine;
using System.Collections;

public class DialogueInteractable : MonoBehaviour
{
    public string text;

    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (Input.GetButtonUp("Submit"))
            {
                Debug.Log("enter");
            }
            Debug.Log("dialogue interactable");

            col.gameObject.GetComponentInChildren<PlayerDialogue>().setText(text);
        }
    }
}
