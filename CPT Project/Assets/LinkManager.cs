using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkManager : MonoBehaviour {

    [SerializeField]
    private int specificCorner;
    [SerializeField]
    bool intersectionPoints = false;
    [SerializeField]
    bool midPoints = false;
    [SerializeField]
    bool lineEquation = false;
    [SerializeField]
    private bool output = false;

    Vector2 testMidPoints;
    Vector2 testLineEq;
    Vector2[] testIntersectionPoints;

    // Use this for initialization
    void Start () {

        calculateCirclePosition();
        checkIntersections(new Vector2(2,2), new Vector2(100,100), new Vector2(0,0));
    }
	
	// Update is called once per frame
	void Update () {

        runOutput();

    }

    void calculateCirclePosition()
    {
        testMidPoints = calculateMidPoint(1, 1, 3, 5);
        testLineEq = calculateLineEquation(-5, -7, 2, -9);
        testIntersectionPoints = FindLineCircleIntersections(3, 3, 4, new Vector2(-3, -3), new Vector2(6,6));
   
    }

    Vector2 calculateMidPoint(float x1, float z1, float x2, float z2)
    {
        float x = (x1 + x2) / 2;
        float z = (z1 + z2) / 2;

        Vector2 midPoint;
        midPoint.x = x;
        midPoint.y = z;

        return midPoint;
    }

    Vector2 calculateLineEquation(float x1, float z1, float x2, float z2)
    {
        // works, tested
        float gradient = (z2 - z1) / (x2 - x1);
        // works, tested
        float yIntercept = z1 - (gradient * x1);

        Vector2 lineEquation;

        lineEquation.x = gradient;
        lineEquation.y = yIntercept;

        return lineEquation;
    }

    // needs to be fully tested
    private Vector2[] FindLineCircleIntersections(float cx, float cy, float radius, Vector2 point1, Vector2 point2)
    {
        float dx, dy, A, B, C, det, t;

        Vector2[] intersections = new Vector2[2];

        dx = point2.x - point1.x;
        dy = point2.y - point1.y;

        A = dx * dx + dy * dy;
        B = 2 * (dx * (point1.x - cx) + dy * (point1.x - cy));
        C = (point1.x - cx) * (point1.x - cx) + (point1.y - cy) * (point1.y - cy) - radius * radius;

        det = B * B - 4 * A * C;
        if ((A <= 0.0000001) || (det < 0))
        {
            // No real solutions.
            //do nothing
            intersections[0] = Vector2.zero;
            intersections[1] = Vector2.zero;

            return intersections;
        }
        else if (det == 0)
        {
            // One solution.
            t = -B / (2 * A);

            intersections[0] = new Vector2(point1.x + t * dx, point1.y + t * dy);
            intersections[1] = Vector2.zero;

            return intersections;
        }
        else
        {
            // Two solutions.
            t = (float)((-B + Math.Sqrt(det)) / (2 * A));
            intersections[0] = new Vector2(point1.x + t * dx, point1.y + t * dy);
            t = (float)((-B - Math.Sqrt(det)) / (2 * A));
            intersections[1] = new Vector2(point1.x + t * dx, point1.y + t * dy);

            return intersections;
        }
    }

    Vector2 checkIntersections(Vector2 inter1, Vector2 inter2, Vector2 midPoint)
    {
        // check to see which point is closest to the Mid Point as this will give the interior position
        //                                   _______________________
        // distance formula Distance = Sqrt /(x2-x1)^2 + (y2 - y1)^2

       // float D1, D2;

        double D1 = Math.Sqrt(
                                ((inter1.x - midPoint.x)* (inter1.x - midPoint.x))        //(x2 - x1)^2
                                                      +                             //          +
                                ((inter1.y - midPoint.y) * (inter1.y - midPoint.y)));     //(y2 - y1)^2

        double D2 = Math.Sqrt(
                                ((inter2.x - midPoint.x) * (inter2.x - midPoint.x))       //(x2 - x1)^2
                                                      +                             //          +
                                ((inter2.y - midPoint.y) * (inter2.y - midPoint.y)));     //(y2 - y1)^2


        if (D1 > D2)
        {
            return inter1;
        }
        else
        {
            return inter2;
        }


        
    }

    private void runOutput()
    {

        if (output)
        {
            if (intersectionPoints)
            {
                Debug.Log(testIntersectionPoints[0]);
                Debug.Log(testIntersectionPoints[1]);
            }

            if (midPoints)
            {
                Debug.Log(testMidPoints);
            }

            if (lineEquation)
            {
                Debug.Log(testLineEq);
            }
            output = false;
        }
        return;
    }
}

