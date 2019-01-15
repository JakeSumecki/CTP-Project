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

   // gameObject circle;

    Vector2 midPoint;
    Vector2 testLineEq;
    Vector2[] intersectionPoints;
    Vector2 circlePosFin;

    Vector2[] circlePositions = new Vector2[50];

    // Use this for initialization
    void Start () {

        // link gameData
        gameData = GameObject.FindObjectOfType<GameData>();

        Vector2 o11, o12, o21, o22, i11, i12, i21, i22;

        FindCircleCircleTangents(new Vector2(0.0f,0.0f), 5.0f, new Vector2(11.0f, 0.0f), 
             5.0f, out o11, out o12, out o21, out o22, out i11, out i12, out i21, out i22);


        //calculatePointDFromABC(new Vector2(6f,6f), new Vector2(12f,0f), new Vector2(0f,0f));

        //createCircles();

    }

    // Update is called once per frame
    void Update () {

        runOutput();

    }

    /// <summary>
    /// Creates the circles
    /// </summary>
    void createCircles()
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
    private Vector2 calculateCirclePosition(Vector2 nodeA, Vector2 nodeB, Vector2 nodeC, float radius)
    {
        midPoint = calculateMidPoint(nodeB, nodeC);
        //testLineEq = calculateLineEquation(nodeA, midPoint);
        intersectionPoints = findLineCircleIntersections(nodeA, radius, nodeA, midPoint);
        circlePosFin = checkIntersections(intersectionPoints[0], intersectionPoints[1], midPoint);
        //Instantiate(original, new Vector3(circlePosFin.x, 0.0f, circlePosFin.y), Quaternion rotation);

        return circlePosFin;
    }

    //--------------------------Maths-Functions-------------------------//

    /// <summary>
    /// Checks which intersection point is closest to the mid-point.
    /// This will effectively give the interior point in relation to the track
    /// </summary>
    /// <param name="inter1">First intersection point</param>
    /// <param name="inter2">Second intersection point</param>
    /// <param name="midPoint">Mid-Point</param>
    /// <returns>Returns a Vector2 of the closest Mid-Point</returns>
    private Vector2 checkIntersections(Vector2 inter1, Vector2 inter2, Vector2 midPoint)
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
    /// Calculate point D where point D is where point A intersects line BC at a right angle
    /// </summary>
    /// <param name="nodeA">Current Node</param>
    /// <param name="nodeB">Next Node</param>
    /// <param name="nodeC">Previous Node</param>
    /// <returns>Vector2 of the coordinates of point D</returns>
    Vector2 calculatePointDFromABC(Vector2 nodeA, Vector2 nodeB, Vector2 nodeC)
    {
        double answerX;
        double answerY;

        Vector2 answer;
        Vector2 XCoords;

        // angle ADC = 90
        double angleDCA = calculateAngle(nodeB, nodeC, nodeA);
        double angleCAD = findLastAngleInTriangle(angleDCA, 90f);

        double sideAC = calculateDistance(nodeA, nodeC);
        double sideAD = calculateSideAAS(angleDCA, 90f, sideAC);
        double sideCD = calculateSideAAS(angleCAD, angleDCA, sideAD);
        Debug.Log(sideAD);
        Debug.Log(sideCD);
        Debug.Log(sideAC);

        //       AB^2 + AC^2 - BC^2     |              ___________
        //  Cy = -------------------    |   Cx = (+-) /AC^2 - Cy^2
        //             2AB              |              

        //answerY = ((sideAD * sideAD) + (sideAC * sideAC) - (sideCD * sideCD)) / (2 * sideAD);
        answerY = ((sideAC * sideAC) + (sideCD * sideCD) - (sideAD * sideAD)) / (2 * sideAC);
        answerX = Math.Sqrt((sideAC * sideAC) - (answerY * answerY));

        //Debug.Log(answerX + " : " + answerY);
        Debug.Log(answerY);

        //answer.y = ((sideAD * sideAD) + (sideAC * sideAC) - (sideCD * sideCD)) / 2 * sideAD;
        //XCoords = Math.Sqrt((sideAC * sideAC) - (answer.y * answer.y));


        return Vector2.zero;
    }

    /// <summary>
    /// Calculates the size of a side with two angles and a side (AAS)
    /// </summary>
    /// <param name="angleA">Angle opposite side wanted</param>
    /// <param name="angleB">Angle opposite another side</param>
    /// <param name="sideB">Side of the other angle</param>
    /// <returns></returns>
    private double calculateSideAAS(double angleA, double angleB, double sideB)
    {
        double answer = (sideB * Math.Sin(convertToRadians(angleA))) / Math.Sin(convertToRadians(angleB));
        answer = makePositive(answer);
        return answer;
    }

    /// <summary>
    /// Calculates the angle of three coordinates. The Second position is the angle found
    /// </summary>
    /// <param name="pos1">First Coordinate</param>
    /// <param name="pos2">Second Coordinate</param>
    /// <param name="pos3">Third Coordinate</param>
    /// <returns></returns>
    private double calculateAngle(Vector2 pos1, Vector2 pos2, Vector2 pos3)
    {
        double a = pos2.x - pos1.x;
        double b = pos2.y - pos1.y;
        double c = pos3.x - pos2.x;
        double d = pos3.y - pos2.y;

        double atanA = Math.Atan2(a, b);
        double atanB = Math.Atan2(c, d);

        double answer = (atanA - atanB) * (-180 / Math.PI);

        // make it positive
        answer = makePositive(answer);

        return answer;
    }

    /// <summary>
    /// Calculates the equation of a line. 
    /// </summary>
    /// <param name="pos1">First Coordinate</param>
    /// <param name="pos2">Second Coordinate</param>
    /// <returns> Line's Gradient & Y-Intercept</returns>
    private Vector2 calculateLineEquation(Vector2 pos1, Vector2 pos2)
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
    /// calcualtes the mid-point of two coordinates. Possibly not needed anymore
    /// </summary>
    /// <param name="pos1"></param> first coordinates
    /// <param name="pos2"></param> second coordinates
    /// <returns>Coordinates of the Mid-Point of pos1 and pos2</returns>
    private Vector2 calculateMidPoint(Vector2 pos1, Vector2 pos2)
    {

        Vector2 tempMidPoint;

        tempMidPoint.x = (pos1.x + pos2.x) / 2;
        tempMidPoint.y = (pos1.y + pos2.y) / 2;

        return tempMidPoint;
    }

    /// <summary>
    /// Converts degrees to radians for use in Math.Sin()
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public double convertToRadians(double angle)
    {
        return (Math.PI / 180) * angle;
    }

    /// <summary>
    /// Takes two andles in a triangle and returns the last one in degrees
    /// </summary>
    /// <param name="angle1">First angle</param>
    /// <param name="angle2">Second angle</param>
    /// <returns></returns>
    private double findLastAngleInTriangle(double angle1, double angle2)
    {

        double answer = 180 - angle1 - angle2;

        return answer;
    }

    /// <summary>
    /// Takes a number and makes it positive
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    private double makePositive(double number)
    {
        double answer = Math.Sqrt(number * number);
        return answer;
    }

    //----------------TEST

    //      IMPORTANT! MIGHT NEED TO ADD ANOTHER FUNCTION FOR THIS TO WORK (FindTangents)

    // Find the tangent points for these two circles.
    // Return the number of tangents: 4, 2, or 0.
    private int FindCircleCircleTangents(
        Vector2 c1, float radius1, Vector2 c2, float radius2,
        out Vector2 outer1_p1, out Vector2 outer1_p2,
        out Vector2 outer2_p1, out Vector2 outer2_p2,
        out Vector2 inner1_p1, out Vector2 inner1_p2,
        out Vector2 inner2_p1, out Vector2 inner2_p2)
    {
        // Make sure radius1 <= radius2.
        if (radius1 > radius2)
        {
            // Call this method switching the circles.
            return FindCircleCircleTangents(
                c2, radius2, c1, radius1,
                out outer1_p2, out outer1_p1,
                out outer2_p2, out outer2_p1,
                out inner1_p2, out inner1_p1,
                out inner2_p2, out inner2_p1);
        }

        // Initialize the return values in case
        // some tangents are missing.
        outer1_p1 = new Vector2(-1, -1);
        outer1_p2 = new Vector2(-1, -1);
        outer2_p1 = new Vector2(-1, -1);
        outer2_p2 = new Vector2(-1, -1);
        inner1_p1 = new Vector2(-1, -1);
        inner1_p2 = new Vector2(-1, -1);
        inner2_p1 = new Vector2(-1, -1);
        inner2_p2 = new Vector2(-1, -1);

        // ***************************
        // * Find the outer tangents *
        // ***************************
        {
            float radius2a = radius2 - radius1;
            if (!FindTangents(c2, radius2a, c1,
                out outer1_p2, out outer2_p2))
            {
                // There are no tangents.
                return 0;
            }

            // Get the vector perpendicular to the
            // first tangent with length radius1.
            float v1x = -(outer1_p2.y - c1.y);
            float v1y = outer1_p2.x - c1.x;
            float v1_length = (float)Math.Sqrt(v1x * v1x + v1y * v1y);
            v1x *= radius1 / v1_length;
            v1y *= radius1 / v1_length;
            // Offset the tangent vector's points.
            outer1_p1 = new Vector2(c1.x + v1x, c1.y + v1y);
            outer1_p2 = new Vector2(
                outer1_p2.x + v1x,
                outer1_p2.y + v1y);

            // Get the vector perpendicular to the
            // second tangent with length radius1.
            float v2x = outer2_p2.y - c1.y;
            float v2y = -(outer2_p2.x - c1.x);
            float v2_length = (float)Math.Sqrt(v2x * v2x + v2y * v2y);
            v2x *= radius1 / v2_length;
            v2y *= radius1 / v2_length;
            // Offset the tangent vector's points.
            outer2_p1 = new Vector2(c1.x + v2x, c1.y + v2y);
            outer2_p2 = new Vector2(
                outer2_p2.x + v2x,
                outer2_p2.y + v2y);
        }

        // If the circles intersect, then there are no inner tangents.
        float dx = c2.x - c1.x;
        float dy = c2.y - c1.y;
        double dist = Math.Sqrt(dx * dx + dy * dy);
        if (dist <= radius1 + radius2) return 2;

        // ***************************
        // * Find the inner tangents *
        // ***************************
        {
            float radius1a = radius1 + radius2;
            FindTangents(c1, radius1a, c2,
                out inner1_p2, out inner2_p2);

            // Get the vector perpendicular to the
            // first tangent with length radius2.
            float v1x = inner1_p2.y - c2.y;
            float v1y = -(inner1_p2.x - c2.x);
            float v1_length = (float)Math.Sqrt(v1x * v1x + v1y * v1y);
            v1x *= radius2 / v1_length;
            v1y *= radius2 / v1_length;
            // Offset the tangent vector's points.
            inner1_p1 = new Vector2(c2.x + v1x, c2.y + v1y);
            inner1_p2 = new Vector2(
                inner1_p2.x + v1x,
                inner1_p2.y + v1y);

            // Get the vector perpendicular to the
            // second tangent with length radius2.
            float v2x = -(inner2_p2.y - c2.y);
            float v2y = inner2_p2.x - c2.x;
            float v2_length = (float)Math.Sqrt(v2x * v2x + v2y * v2y);
            v2x *= radius2 / v2_length;
            v2y *= radius2 / v2_length;
            // Offset the tangent vector's points.
            inner2_p1 = new Vector2(c2.x + v2x, c2.y + v2y);
            inner2_p2 = new Vector2(
                inner2_p2.x + v2x,
                inner2_p2.y + v2y);
        }

        return 4;
    }

    // Find the tangent points for this circle and external point.
    // Return true if we find the tangents, false if the point is
    // inside the circle.
    private bool FindTangents(Vector2 center, float radius,
        Vector2 external_point, out Vector2 pt1, out Vector2 pt2)
    {
        // Find the distance squared from the
        // external point to the circle's center.
        double dx = center.x - external_point.x;
        double dy = center.y - external_point.y;
        double D_squared = dx * dx + dy * dy;
        if (D_squared < radius * radius)
        {
            pt1 = new Vector2(-1, -1);
            pt2 = new Vector2(-1, -1);
            return false;
        }

        // Find the distance from the external point
        // to the tangent points.
        double L = Math.Sqrt(D_squared - radius * radius);

        // Find the points of intersection between
        // the original circle and the circle with
        // center external_point and radius dist.
        FindCircleCircleIntersections(
            center.x, center.y, radius,
            external_point.x, external_point.y, (float)L,
            out pt1, out pt2);

        return true;
    }

    // Find the points where the two circles intersect.
    private int FindCircleCircleIntersections(
        float cx0, float cy0, float radius0,
        float cx1, float cy1, float radius1,
        out Vector2 intersection1, out Vector2 intersection2)
    {
        // Find the distance between the centers.
        float dx = cx0 - cx1;
        float dy = cy0 - cy1;
        double dist = Math.Sqrt(dx * dx + dy * dy);

        // See how many solutions there are.
        if (dist > radius0 + radius1)
        {
            // No solutions, the circles are too far apart.
            intersection1 = new Vector2(float.NaN, float.NaN);
            intersection2 = new Vector2(float.NaN, float.NaN);
            return 0;
        }
        else if (dist < Math.Abs(radius0 - radius1))
        {
            // No solutions, one circle contains the other.
            intersection1 = new Vector2(float.NaN, float.NaN);
            intersection2 = new Vector2(float.NaN, float.NaN);
            return 0;
        }
        else if ((dist == 0) && (radius0 == radius1))
        {
            // No solutions, the circles coincide.
            intersection1 = new Vector2(float.NaN, float.NaN);
            intersection2 = new Vector2(float.NaN, float.NaN);
            return 0;
        }
        else
        {
            // Find a and h.
            double a = (radius0 * radius0 -
                radius1 * radius1 + dist * dist) / (2 * dist);
            double h = Math.Sqrt(radius0 * radius0 - a * a);

            // Find P2.
            double cx2 = cx0 + a * (cx1 - cx0) / dist;
            double cy2 = cy0 + a * (cy1 - cy0) / dist;

            // Get the points P3.
            intersection1 = new Vector2(
                (float)(cx2 + h * (cy1 - cy0) / dist),
                (float)(cy2 - h * (cx1 - cx0) / dist));
            intersection2 = new Vector2(
                (float)(cx2 - h * (cy1 - cy0) / dist),
                (float)(cy2 + h * (cx1 - cx0) / dist));

            // See if we have 1 or 2 solutions.
            if (dist == radius0 + radius1) return 1;
            return 2;
        }
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

