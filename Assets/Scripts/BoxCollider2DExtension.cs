using UnityEngine;
public static class BoxCollider2DExtension
{
    public static Rigidbody2D GetPlayerRidingHits(this BoxCollider2D source, float distance = 0.5f)
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(source.transform.position, source.size, 0, Vector2.up, distance);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.tag == "Player")
            {
                return hits[i].transform.gameObject.GetComponent<Rigidbody2D>();
            }
        }
        return null;
    }

}
