using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility 
{
    //2022-07-09: copied from Stonicorn.Utility
    public static Plane raycastPlane = new Plane(Vector3.forward, Vector3.zero);
    public static Vector2 ScreenToWorldPoint(Vector3 screenPoint)
    {
        //2019-01-28: copied from an answer by Tomer-Barkan: https://answers.unity.com/questions/566519/camerascreentoworldpoint-in-perspective.html
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        float distance;
        raycastPlane.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}