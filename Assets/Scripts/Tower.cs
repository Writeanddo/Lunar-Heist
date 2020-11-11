using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject Player;
    public BoxCollider2D TowerBounds;

    void LateUpdate()
    {
    }

    public Vector2 GetRatioOfPlayer()
    {
        if (Player == null)
        {
            return new Vector2(0, 0);
        }
        Vector2 playerPosition = Player.transform.position;

        float boxStartX = TowerBounds.transform.position.x - (TowerBounds.size.x /2);
        float ratioX = (playerPosition.x - boxStartX) / TowerBounds.size.x;

        float boxStartY = TowerBounds.transform.position.y - (TowerBounds.size.y / 2);
        float ratioY = (playerPosition.y - boxStartY) / TowerBounds.size.y;


        return new Vector2(ratioX, ratioY);
    }
}
