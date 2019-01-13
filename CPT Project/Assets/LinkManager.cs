using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkManager : MonoBehaviour {

    [SerializeField]
    private int specificCorner;
    [SerializeField]
    bool intersectionPointsB = false;
    [SerializeField]
    bool midPointsB = false;
    [SerializeField]
    bool lineEquationB = false;
    [SerializeField]
    bool closestPointB = false;
    [SerializeField]
    private bool output = false;

    Vector2 midPoint;
    Vector2 testLineEq;
    Vector2[] intersectionPoints;
    Vector2 circlePosFin;

    // Use this for initialization
    void Start () {

        calculateCirclePosition(new Vector2(0,6), new Vector2(6,6), new Vector2(0,0), 3f);
        //checkIntersections(new Vector2(2,2), new Vector2(100,100), new Vector2(0,0));
    }
	
	// Update is called once per frame
	void Update () {

        runOutput();

    }

    // A = current node
    // B = next node
    // C = previous node
    void calculateCirclePosition(Vector2 nodeA, Vector2 nodeB, Vector2 nodeC, float radius)
    {

        midPoint = calculateMidPoint(nodeB, nodeC);
        testLineEq = calculateLineEquation(nodeA, midPoint);
        intersectionPoints = FindLineCircleIntersections(nodeA, 4, new Vector2(-3, -3), new Vector2(6,6));
        circlePosFin = checkIntersections(intersectionPoints[0], intersectionPoints[1], midPoint);   

    }

    Vector2 calculateMidPoint(Vector2 pos1, Vector2 pos2)
    {

        Vector2 tempMidPoint;

        tempMidPoint.x = (pos1.x + pos2.x) / 2;
        tempMidPoint.y = (pos1.y + pos2.y) / 2;

        return tempMidPoint;
    }

    Vector2 calculateLineEquation(Vector2 pos1, Vector2 pos2)
    {
        // works, tested
        float gradient = (pos2.y - pos1.y) / (pos2.x - pos1.x);
        // works, tested
        float yIntercept = pos1.y - (gradient * pos1.x);

        Vector2 lineEquation;

        lineEquation.x = gradient;
        lineEquation.y = yIntercept;

        return lineEquation;
    }

    // needs to be fully tested
    // need to either change the Point1 & two to use an equation or extend them out
    private Vector2[] FindLineCircleIntersections(Vector2 circlePos, float radius, Vector2 point1, Vector2 point2)
    {
        float dx, dy, A, B, C, det, t;

        Vector2[] intersections = new Vector2[2];

        dx = point2.x - point1.x;
        dy = point2.y - point1.y;

        A = dx * dx + dy * dy;
        B = 2 * (dx * (point1.x - circlePos.x) + dy * (point1.x - circlePos.y));
        C = (point1.x - circlePos.x) * (point1.x - circlePos.x) + (point1.y - circlePos.y) * (point1.y - circlePos.y) - radius * radius;

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

    // inter1 = intersection point 1 
    // inter2 = intersection point 2  
    // midPoint = mid-point between Node B & node C

    Vector2 checkIntersections(Vector2 inter1, Vector2 inter2, Vector2 midPoint)
    {
        // check to see which point is closest to the Mid Point as this will give the interior position
        //                                   _______________________
        // Distance formula: Distance = Sqrt /(x2-x1)^2 + (y2 - y1)^2

        double D1 = Math.Sqrt(
                                ((inter1.x - midPoint.x)* (inter1.x - midPoint.x))        //(x2 - x1)^2
                                                      +                             //          +
                                ((inter1.y - midPoint.y) * (inter1.y - midPoint.y)));     //(y2 - y1)^2

        double D2 = Math.Sqrt(
                                ((inter2.x - midPoint.x) * (inter2.x - midPoint.x))       //(x2 - x1)^2
                                                      +                             //          +
                                ((inter2.y - midPoint.y) * (inter2.y - midPoint.y)));     //(y2 - y1)^2
        // Check which point is furtest from node A
        if (D1 < D2)
        {
            Debug.Log(D1);
            return inter1;
        }
        else
        {
            Debug.Log(D2);
            return inter2;
        }     
    }

    private void runOutput()
    {

        if (output)
        {

            if (midPointsB)
            {
                Debug.Log("Mid-Point :" + midPoint);
            }

            if (lineEquationB)
            {
                Debug.Log("Line Equation : Y = " + testLineEq.x + ".X + " + testLineEq.y);
            }

            if (intersectionPointsB)
            {
                Debug.Log("Intersecion Point 1 :" + intersectionPoints[0]);
                Debug.Log("Intersecion Point 2 :" + intersectionPoints[1]);
            }

            if (closestPointB)
            {
                Debug.Log("Final circle coordinates :" + circlePosFin);
            }

            output = false;
        }
        return;
    }
}

