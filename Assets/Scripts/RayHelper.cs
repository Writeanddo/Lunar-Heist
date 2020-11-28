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


    public static RaycastHit2D DrayRay(Vector2 origin, Vector2 direction, float distance, Transform transform, int[] layers)
    {
        LayerMask mask = GetLayerMask(layers);

        Vector3 normalisedDirection = transform.TransformDirection(direction).normalized;

        Debug.DrawRay(origin, normalisedDirection * distance, Color.green);

        return Physics2D.Raycast(origin, normalisedDirection, distance, mask);
    }

    public static LayerMask GetLayerMask(int[] layers)
    {
        LayerMask mask = new LayerMask();
        foreach (int m in layers)
        {
            mask |= 1 << m;
        }
        return mask;
    }


    public static LayerMask GetLayerMask(int layer)
    {
        return 1 << layer;
    }
}