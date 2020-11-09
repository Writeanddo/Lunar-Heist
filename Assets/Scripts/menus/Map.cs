using UnityEngine;

public class Map : MonoBehaviour
{

    private ScreenBounds bounds = new ScreenBounds();
    public SpriteRenderer TowerMiniMap;
    public SpriteRenderer Marker;
    private float Padding = 0.5f;
    private Vector2 Offset;
    private Tower Tower;

    void Start()
    {
        Offset = (TowerMiniMap.size / 2) + new Vector2(Padding, Padding);
        Tower = FindObjectOfType<Tower>();
    }

    void Update()
    {
        TowerMiniMap.transform.position = bounds.TopRightScreen() - Offset;
        Marker.transform.localPosition = Tower.GetRelativePlayerPosition();
    }
}
