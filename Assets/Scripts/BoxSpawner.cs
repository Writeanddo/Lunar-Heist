using System.Collections;
using System.Numerics;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public SpriteRenderer Button;
    public Sprite ButtonUp;
    public Sprite ButtonDown;
    public Transform BoxSpawnLocation;

    public GameObject Box;
    public BoxCollider2D BoxCollider;

    private bool canSpawn;
    private IEnumerator ButtonTimeout;
    public BoxCollider2D BoxBounds;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn && Input.GetButtonDown("Fire1"))
        {
            SpawnBox();
        }

       
        if (!BoxBounds.IsTouching(BoxCollider))
        {
            SpawnBox();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canSpawn = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canSpawn = false;
        }
    }

    private void SpawnBox()
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
