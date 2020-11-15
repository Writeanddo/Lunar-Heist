using UnityEngine;
using System.Collections;

public static class RayHelper
{
    public static RaycastHit2D DrayRay(Vector2 origin, Vector2 direction, float distance, Transform transform, int layer)
    {
        LayerMask mask = 1 << layer;
        Vector3 normalisedDirection = transform.TransformDirection(direction).normalized;

        Debug.DrawRay(origin, normalisedDirection * distance, Color.green);

        return Physics2D.Raycast(origin, normalisedDirection, distance, mask);
    }
}