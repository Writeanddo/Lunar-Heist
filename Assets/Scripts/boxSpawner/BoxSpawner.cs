using System.Collections;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public SpriteRenderer Button;
    public Sprite ButtonUp;
    public Sprite ButtonDown;
    public Transform BoxSpawnLocation;

    public GameObject Box;
    public BoxCollider2D BoxCollider;

    private IEnumerator ButtonTimeout;
    public BoxCollider2D BoxBounds;


    void Update()
    {
        if (!BoxBounds.IsTouching(BoxCollider))
        {
            SpawnBox();
        }
    }

    public void SpawnBox()
    {
        Box.GetComponent<Rigidbody2D>().velocity = UnityEngine.Vector3.zero;
        Box.transform.position = BoxSpawnLocation.position;
        Box.SetActive(true);
        Button.sprite = ButtonDown;
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
