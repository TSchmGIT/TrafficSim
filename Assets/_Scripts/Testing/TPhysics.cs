using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that provides extended physics methods
/// </summary>
public static class TPhysics
{
    public static List<Vector3> ConeCast(IEnumerable<Vector3> inPoints, Vector3 origin, Vector3 dir, float maxDistance, float coneAngle)
    {
        List<Vector3> result = new List<Vector3>();

        ConeCast(inPoints, result, origin, dir, maxDistance, coneAngle);

        return result;
    }
    public static void ConeCast(IEnumerable<Vector3> inPoints, List<Vector3> outList, Vector3 origin, Vector3 dir, float maxDistance, float coneAngle)
    {
        dir.Normalize();
        outList.Clear();

        foreach (Vector3 point in inPoints)
        {
            Vector3 dirToPoint = (point - origin).normalized;
            if (Vector3.Dot(dir, dirToPoint) < 0.0f)
                continue;

            if (Vector3.SqrMagnitude(point - origin) > maxDistance * maxDistance)
                continue;

            if (Vector3.Angle(dir, dirToPoint) > coneAngle)
                continue;

            outList.Add(point);
        }
    }

    public static float DistanceToLine(Ray ray, Vector3 point) => Vector3.Cross(ray.direction, point - ray.origin).magnitude;
    public static float SqrDistanceToLine(Ray ray, Vector3 point) => Vector3.Cross(ray.direction, point - ray.origin).sqrMagnitude;
}
