using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    Vector3[] cameraZonePoints;
    Vector2[] nearestPointForEachLine;


    private void FixedUpdate()
    {
        Vector2 playerPosition = CharacterManager.Instance.transform.position;
        nearestPointForEachLine = new Vector2[cameraZonePoints.Length-1];
        for (int i = 0; i < cameraZonePoints.Length -2; i++)
        {
            nearestPointForEachLine[i] = NearestPointInPlane(cameraZonePoints[i], cameraZonePoints[i + 1], cameraZonePoints[i + 2], playerPosition);
        }

        Vector2 wantedPosition = NearestPointInArray(nearestPointForEachLine,  playerPosition);
        transform.position = new Vector3(wantedPosition.x, wantedPosition.y, -10);
    }


    public Vector2 NearestPointInArray(Vector2[] vector, Vector2 point)
    {
        double distance = Double.MaxValue;
        Vector2 nearestPoint = vector[0];
        foreach (Vector2 pnt in vector)
        {
            double dist = DistanceBetweenPoints(pnt, point);
            if (dist < distance)
            {
                distance = dist;
                nearestPoint = pnt;
            }
        }

        return nearestPoint;
    }

    double DistanceBetweenPoints(Vector2 p1, Vector2 p2)
    {
        return Math.Sqrt(Math.Pow((p2.x - p1.x), 2) + Math.Pow(p2.y - p1.y, 2));
    }



    Vector2 NearestPointInPlane(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 pObj)
    {
        Plane p = new Plane(p1,p2,p3);
        Vector2 point = p.ClosestPointOnPlane(pObj);

        if(PointInTriangle(point, p1, p2, p3))
        {
            return point;
        }

        Vector2[] points = new Vector2[3];
        points[0] = NearestPointOnLine(p1, p2, point);
        points[1] = NearestPointOnLine(p2, p3, point);
        points[2] = NearestPointOnLine(p3, p1, point);

        return NearestPointInArray(points, point);
    }
    float sign(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
    }

    bool PointInTriangle(Vector2 pt, Vector2 v1, Vector2 v2, Vector2 v3)
    {
        float d1, d2, d3;
        bool has_neg, has_pos;

        d1 = sign(pt, v1, v2);
        d2 = sign(pt, v2, v3);
        d3 = sign(pt, v3, v1);

        has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

        return !(has_neg && has_pos);
    }

    public Vector2 NearestPointOnLine(Vector2 linePoint1, Vector2 linePoint2, Vector2 point)
    {
        Vector2 vectorLine = linePoint1 - linePoint2;
        vectorLine.Normalize();

        Vector2 vectorPoint = point - linePoint1;
        float dot = Vector2.Dot(vectorPoint, vectorLine);
        Vector2 nearestPoint = linePoint1 + vectorLine * dot;

        if (IsCBetweenAB(linePoint1, linePoint2, nearestPoint))
        {
            return nearestPoint;
        }
        if (IsCBetweenAB(linePoint1, nearestPoint, linePoint2))
        {
            return linePoint2;
        }
        return linePoint1;

    }
    bool IsCBetweenAB(Vector3 A, Vector3 B, Vector3 C)
    {
        return Vector3.Dot((B - A).normalized, (C - B).normalized) < 0f && Vector3.Dot((A - B).normalized, (C - A).normalized) < 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < cameraZonePoints.Length - 2; i++)
        {
            DrawTriangle(cameraZonePoints[i], cameraZonePoints[i+1], cameraZonePoints[i+2]);
        }
    }

    void DrawTriangle(Vector3 a, Vector3 b, Vector3 c)
    {
        Mesh m = new Mesh();

        m.vertices = new Vector3[3]
        {
            a,
            b,
            c
        };

        m.triangles = new int[]
        {
            0, 1, 2
        };

        m.normals = new Vector3[]
        {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward
        };

        Graphics.DrawMeshNow(m, Vector3.zero, Quaternion.identity);

        DestroyImmediate(m);
    }

}
