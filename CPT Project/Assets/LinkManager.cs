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

    private GameData gameData;

    Vector2 midPoint;
    Vector2 testLineEq;
    Vector2[] intersectionPoints;
    Vector2 circlePosFin;

    Vector2[] circlePositions = new Vector2[50];

    // Use this for initialization
    void Start () {

        // link gameData
        gameData = GameObject.FindObjectOfType<GameData>();

        Debug.Log(calculateAngleFrom3PointsInDegrees(new Vector2(0.0f, 0.0f), new Vector2(3f, 2f), new Vector2(5f, 4f)));


        //calculateCirclePosition(new Vector2(0f,-30f), new Vector2(-17.62f, -21.07f), new Vector2(0f,0f), 1f);
        //checkIntersections(new Vector2(2,2), new Vector2(100,100), new Vector2(0,0));

        createCirclePositions();

    }

    // Update is called once per frame
    void Update () {

        runOutput();

    }

    /// <summary>
    /// Creates the circles
    /// </summary>
    void createCirclePositions()
    {
        
        // loop through all corners
        for (int i = 0; i < gameData.getAmountOfCorners(); i++)
        {
            //when at the start wont be able to acces NodeC as its the last in the array
            if (i == 0)
            {
                circlePositions[i] = calculateCirclePosition(gameData.getCornerCoordsAtPos(i),      //NodeA
                                                             gameData.getCornerCoordsAtPos(i + 1),  //NodeB
                                                             gameData.getCornerCoordsAtPos(gameData.getAmountOfCorners() - 1),   //NodeC
                                                             gameData.getCornerRadiusAtPos(i));     //Radius

            }

            //when at the end wont be able to access NodeB because its at the start
            else if (i == gameData.getAmountOfCorners() - 1)
            {
                circlePositions[i] = calculateCirclePosition(gameData.getCornerCoordsAtPos(i),      //NodeA
                                                             gameData.getCornerCoordsAtPos(0),      //NodeB
                                                             gameData.getCornerCoordsAtPos(i - 1),   //NodeC
                                                             gameData.getCornerRadiusAtPos(i));     //Radius
            }
            //if not at the start or end use adjacent nodes as B & C
            else
            {;
                circlePositions[i] = calculateCirclePosition(gameData.getCornerCoordsAtPos(i),      //NodeA
                                                             gameData.getCornerCoordsAtPos(i + 1),  //NodeB
                                                             gameData.getCornerCoordsAtPos(i - 1),   //NodeC
                                                             gameData.getCornerRadiusAtPos(i));     //Radius
            }

            //Debug.Log(circlePositions[i]);
        }


    }


    /// <summary>
    /// Calculates the circle positions and stores the positions in a Vector2 array in GameData
    /// </summary>
    /// <param name="nodeA">Current Node</param>
    /// <param name="nodeB">Next Node</param>
    /// <param name="nodeC">Previous Node</param>
    /// <param name="radius">Circle's Radius</param>
    /// <returns>Final position of the circle</returns>
    Vector2 calculateCirclePosition(Vector2 nodeA, Vector2 nodeB, Vector2 nodeC, float radius)
    {
        midPoint = calculateMidPoint(nodeB, nodeC);
        //testLineEq = calculateLineEquation(nodeA, midPoint);
        intersectionPoints = findLineCircleIntersections(nodeA, radius, nodeA, midPoint);
        circlePosFin = checkIntersections(intersectionPoints[0], intersectionPoints[1], midPoint);

        return circlePosFin;
    }

    /// <summary>
    /// calcualtes the mid-point of two coordinates. Possibly not needed anymore
    /// </summary>
    /// <param name="pos1"></param> first coordinates
    /// <param name="pos2"></param> second coordinates
    /// <returns>Coordinates of the Mid-Point of pos1 and pos2</returns>
    Vector2 calculateMidPoint(Vector2 pos1, Vector2 pos2)
    {

        Vector2 tempMidPoint;

        tempMidPoint.x = (pos1.x + pos2.x) / 2;
        tempMidPoint.y = (pos1.y + pos2.y) / 2;

        return tempMidPoint;
    }

    /// <summary>
    /// Calculates the point at which A intersects line BC at a right angle
    /// </summary>
    /// <returns></returns>
    Vector2 calculateCenterPoint()
    {
        return Vector2.zero;
    }

    /// <summary>
    /// Calculates the equation of a line. 
    /// </summary>
    /// <param name="pos1">First Coordinate</param>
    /// <param name="pos2">Second Coordinate</param>
    /// <returns> Line's Gradient & Y-Intercept</returns>
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
    
    /// <summary>
    /// Calculates intersection points between a line and a circle. 
    /// Point1 and 2 can be any coordinates along the line 
    /// </summary>
    /// <param name="circlePos">Coordinates of the center of the circle</param>
    /// <param name="radius">Circle's radius</param>
    /// <param name="point1">A coordinate on the line</param>
    /// <param name="point2">Another coordinate on the line</param>
    /// <returns>Two Vector2's of the two intersection points</returns>
    private Vector2[] findLineCircleIntersections(Vector2 circlePos, float radius, Vector2 point1, Vector2 point2)
    {
        Vector2[] intersections = new Vector2[2];

        float dx, dy, A, B, C, det, t;

        dx = point2.x - point1.x;
        dy = point2.y - point1.y;

        A = dx * dx + dy * dy;
        B = 2 * (dx * (point1.x - circlePos.x) + dy * (point1.y - circlePos.y));
        C = (point1.x - circlePos.x) * (point1.x - circlePos.x) + 
            (point1.y - circlePos.y) * (point1.y - circlePos.y) - 
            radius * radius;

        det = B * B - 4 * A * C;
        if ((A <= 0.0000001) || (det < 0))
        {
            // No real solutions.
            //do nothing
            intersections[0] = Vector2.zero;
            intersections[1] = Vector2.zero;

            //Debug.Log("0");

            return intersections;
        }
        else if (det == 0)
        {
            // One solution
            t = -B / (2 * A);

            intersections[0] = new Vector2(point1.x + t * dx, point1.y + t * dy);
            intersections[1] = Vector2.zero;

            //Debug.Log("1");

            return intersections;
        }
        else
        {
            // Two solutions.
            t = (float)((-B + Math.Sqrt(det)) / (2 * A));
            intersections[0] = new Vector2(point1.x + t * dx, point1.y + t * dy);
            t = (float)((-B - Math.Sqrt(det)) / (2 * A));
            intersections[1] = new Vector2(point1.x + t * dx, point1.y + t * dy);

            //Debug.Log("2");

            return intersections;
        }
    }

    /// <summary>
    /// Checks which intersection point is closest to the mid-point.
    /// This will effectively give the interior point in relation to the track
    /// </summary>
    /// <param name="inter1">First intersection point</param>
    /// <param name="inter2">Second intersection point</param>
    /// <param name="midPoint">Mid-Point</param>
    /// <returns>Returns a Vector2 of the closest Mid-Point</returns>
    Vector2 checkIntersections(Vector2 inter1, Vector2 inter2, Vector2 midPoint)
    {
        // check to see which point is closest to the Mid Point as this will give the interior position

        double D1 = calculateDistance(inter1, midPoint);
        double D2 = calculateDistance(inter2, midPoint);

        if (D1 < D2)
        {
            //Debug.Log(D1);
            return inter1;
        }
        else
        {
            //Debug.Log(D2);
            return inter2;
        }     
    }

    /// <summary>
    /// Calculate the distance between two positions
    /// </summary>
    /// <param name="pos1">Position 1</param>
    /// <param name="pos2">Position 2</param>
    /// <returns>Double</returns>
    double calculateDistance(Vector2 pos1, Vector2 pos2)
    {
        //                                    _______________________
        // Distance formula: Distance = Sqrt /(x2-x1)^2 + (y2 - y1)^2
        double answer = Math.Sqrt(
                                ((pos1.x - pos2.x) * (pos1.x - pos2.x))        //(x2 - x1)^2
                                                      +                             //          +
                                ((pos1.y - pos2.y) * (pos1.y - pos2.y)));     //(y2 - y1)^2

        return answer;

    }

    /// <summary>
    /// Calculates the angle of three coordinates. The Second position is the angle found
    /// </summary>
    /// <param name="pos1">First Coordinate</param>
    /// <param name="pos2">Second Coordinate</param>
    /// <param name="pos3">Third Coordinate</param>
    /// <returns></returns>
    private double calculateAngleFrom3PointsInDegrees(Vector2 pos1, Vector2 pos2, Vector2 pos3)
    {
        double a = pos2.x - pos1.x;
        double b = pos2.y - pos1.y;
        double c = pos3.x - pos2.x;
        double d = pos3.y - pos2.y;

        double atanA = Math.Atan2(a, b);
        double atanB = Math.Atan2(c, d);

        return (atanA - atanB) * (-180 / Math.PI);
        // if Second line is counterclockwise from 1st line angle is 
        // positive, else negative
    }

    /// <summary>
    /// Used to output data to Unity's debugger
    /// </summary>
    private void runOutput()
    {

        if (output)
        {

            if (midPointsB)
            {
                Debug.Log("Mid-Point :" + midPoint);
            }

            //if (lineEquationB)
            //{
            //    Debug.Log("Line Equation : Y = " + testLineEq.x + ".X + " + testLineEq.y);
            //}

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

