using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject Player;
    public BoxCollider2D TowerBounds;

    void LateUpdate()
    {
    }

    public Vector2 GetRelativePlayerPosition()
    {
        Vector2 playerPosition = Player.transform.position;

        float boxStartX = TowerBounds.transform.position.x - (TowerBounds.size.x /2);
        float x = playerPosition.x - boxStartX;

        float boxStartY = TowerBounds.transform.position.y - (TowerBounds.size.y / 2);
        float y = playerPosition.y - boxStartY;

        return new Vector2(x, y) / TowerBounds.size;
    }
}
