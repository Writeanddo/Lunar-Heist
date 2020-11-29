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

        Vector2 towerPosition = bounds.TopRightScreen() - Offset;
        TowerMiniMap.transform.position = new Vector3(towerPosition.x, towerPosition.y, transform.position.z);
        Tower = FindObjectOfType<Tower>();
    }

    public void RelocateTower()
    {
        Tower = FindObjectOfType<Tower>();
    }

    void FixedUpdate()
    {
        Vector2 ratio = Tower.GetRatioOfPlayer();
        float y = (ratio.y * 3) - 1.5f;
        Marker.transform.localPosition = new Vector2(ratio.x - 0.5f, y);
    }
}
