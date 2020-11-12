using UnityEngine;

public class Map : MonoBehaviour
{
    public SpriteRenderer TowerMiniMap;
    public SpriteRenderer Marker;
    private Tower Tower;

    void Start()
    {
        Tower = FindObjectOfType<Tower>();
    }

    void FixedUpdate()
    {
        Marker.transform.localPosition = Tower.GetRatioOfPlayer() - new Vector2(0.5f, 2);
    }
}
