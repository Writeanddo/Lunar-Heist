using System.Collections;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public SpriteRenderer Button;
    public Sprite ButtonUp;
    public Sprite ButtonDown;
    public Transform BoxSpawnLocation;
    public Material NormalMaterial;


    public GameObject Box;

    private IEnumerator ButtonTimeout;

    public AudioSource spawnSound;

    public void SpawnBox()
    {
        Box.GetComponent<Rigidbody2D>().velocity = UnityEngine.Vector3.zero;
        Box.transform.position = BoxSpawnLocation.position;
        Box.GetComponent<SpriteRenderer>().material = NormalMaterial;
        Box.SetActive(true);
        Button.sprite = ButtonDown;
        spawnSound.Play();
        if (ButtonTimeout != null)
        {
            StartCoroutine(ButtonTimeout);
        }
        ButtonTimeout = SetButtonUp();
        StartCoroutine(ButtonTimeout);
    }

    IEnumerator SetButtonUp()
    {
        yield return new WaitForSeconds(0.5f);
        Button.sprite = ButtonUp;
    }
}
