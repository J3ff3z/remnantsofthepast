using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    Vector3[] cameraZonePoints;
    Vector3[] gizmoPoints;
    Vector2[] nearestPointForEachLine;


    private void FixedUpdate()
    {
        Vector2 playerPosition = CharacterManager.Instance.transform.position;
        nearestPointForEachLine = new Vector2[cameraZonePoints.Length-1];
        for (int i = 0; i < cameraZonePoints.Length -1; i++)
        {
            nearestPointForEachLine[i] = NearestPointOnLine(cameraZonePoints[i], cameraZonePoints[i+1], playerPosition);
        }

        Vector2 wantedPosition = NearestPointInVector(nearestPointForEachLine,  playerPosition);
        transform.position = new Vector3(wantedPosition.x, wantedPosition.y, -10);
    }


    public Vector2 NearestPointInVector(Vector2[] vector, Vector2 point)
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
        if(IsCBetweenAB(linePoint1,nearestPoint, linePoint2)){
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
        CameraPointToGizmoPoint();

        Gizmos.color = Color.green;
        Gizmos.DrawLineList(gizmoPoints);
    }

    void CameraPointToGizmoPoint()
    {
        int length = cameraZonePoints.Length * 2;
        gizmoPoints = new Vector3[length];
        gizmoPoints[0] = cameraZonePoints[0];
        Vector2 lastGizmo = gizmoPoints[0];
        for(int i = 1; i < gizmoPoints.Length; i++)
        {
            if (i % 2 == 0)
            {
                gizmoPoints[i] = lastGizmo;
            }
            else
            {
                gizmoPoints[i] = cameraZonePoints[i/2];
                lastGizmo = gizmoPoints[i];
            }
        }
    }
}
