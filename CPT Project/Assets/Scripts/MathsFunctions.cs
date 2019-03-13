using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathsFunctions : MonoBehaviour {
    #region Base Functions
    /// <summary>
    /// calcualtes the mid-point of two coordinates. Possibly not needed anymore
    /// </summary>
    /// <param name="pos1"></param> first coordinates
    /// <param name="pos2"></param> second coordinates
    /// <returns>Coordinates of the Mid-Point of pos1 and pos2</returns>
    public Vector2 calculateMidPoint(Vector2 pos1, Vector2 pos2)
    {

        Vector2 tempMidPoint;

        tempMidPoint.x = (pos1.x + pos2.x) / 2;
        tempMidPoint.y = (pos1.y + pos2.y) / 2;

        return tempMidPoint;
    }

    /// <summary>
    /// Calculate the distance between two positions
    /// </summary>
    /// <param name="pos1">Position 1</param>
    /// <param name="pos2">Position 2</param>
    /// <returns>Double</returns>
    public double calculateDistance(Vector2 pos1, Vector2 pos2)
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
    /// NOT USED IN CURRENT BUILD
    /// Calculates the angle of three coordinates. The Second position is the angle found
    /// </summary>
    /// <param name="pos1">First Coordinate</param>
    /// <param name="pos2">Second Coordinate</param>
    /// <param name="pos3">Third Coordinate</param>
    /// <returns></returns>
    public double calculateAngle(Vector2 pos1, Vector2 pos2, Vector2 pos3)
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
    #endregion

    #region Probability Functions
    /// <summary>
    /// /// Returns a number between minNumber and maxNumber with a normal distribution
    /// strength corresponds to hot strong the pull is towards the center
    /// </summary>
    /// <param name="minNumber">minimum number</param>
    /// <param name="maxNumber">maximum number</param>
    /// <param name="strength">How strong the "pull" is towards the centre</param>
    /// <returns></returns>
    public int RandomNormalDistributionINT(float minNumber, float maxNumber, int strength)
    {
        float answerFlt = 0f;
        for (int i = 0; i < strength; i++)
        {
            answerFlt += UnityEngine.Random.Range(minNumber, maxNumber);
        }

        // average the number
        answerFlt = answerFlt / strength;

        int answerInt = (int)answerFlt;

        return answerInt;
    }

    /// <summary>
    /// /// Returns a number between minNumber and maxNumber with a normal distribution
    /// strength corresponds to hot strong the pull is towards the center
    /// </summary>
    /// <param name="minNumber">minimum number</param>
    /// <param name="maxNumber">maximum number</param>
    /// <param name="strength">How strong the "pull" is towards the centre</param>
    /// <returns></returns>
    public float RandomNormalDistributionFLT(float minNumber, float maxNumber, int strength)
    {
        float answerFlt = 0f;
        for (int i = 0; i < strength; i++)
        {
            answerFlt += UnityEngine.Random.Range(minNumber, maxNumber);
        }

        // average the number
        answerFlt = answerFlt / strength;

        return answerFlt;
    } 
    #endregion

    #region Transformation Functions
    /// <summary>
    /// Takes two positions and adds them together to give the position of Node 1 on Node 2
    /// For use in placing nodes after calculated
    /// </summary>
    /// <param name="node">The node to be moved. Origin should be (0,0)</param>
    /// <param name="transformation">The new origin of the node</param>
    /// <returns></returns>
    public Vector2 transformPosition(Vector2 node, Vector2 transformation)
    {
        Vector2 result = node + transformation;
        return result;
    }

    /// <summary>
    /// Rotates one point around another
    /// </summary>
    /// <param name="pointToRotate">The point to rotate.</param>
    /// <param name="centerPoint">The center point of rotation.</param>
    /// <param name="angleInDegrees">The rotation angle in degrees.</param>
    /// <returns>Rotated point</returns>
    public Vector2 rotateNodeAroundNode(Vector2 pointToRotate, Vector2 centerPoint, double angleInDegrees)
    {
        double angleInRadians = angleInDegrees * (Math.PI / 180);
        double cosTheta = Math.Cos(angleInRadians);
        double sinTheta = Math.Sin(angleInRadians);
        return new Vector2(
            (float)(cosTheta * (pointToRotate.x - centerPoint.x) - sinTheta * (pointToRotate.y - centerPoint.y) + centerPoint.x)
            ,
            (float)((sinTheta * (pointToRotate.x - centerPoint.x) + cosTheta * (pointToRotate.y - centerPoint.y) + centerPoint.y)));
    } 
    #endregion

    #region Tangent and Intersection Functions
    /// <summary>
    /// Find the tangent points for these two circles.
    /// </summary>
    /// <param name="c1">Circle 1 position</param>
    /// <param name="radius1">Circle 1 radius</param>
    /// <param name="c2">circle 2 position</param>
    /// <param name="radius2">Circle 2 Radius </param>
    /// <param name="outer1_p1"></param>
    /// <param name="outer1_p2"></param>
    /// <param name="outer2_p1"></param>
    /// <param name="outer2_p2"></param>
    /// <param name="inner1_p1"></param>
    /// <param name="inner1_p2"></param>
    /// <param name="inner2_p1"></param>
    /// <param name="inner2_p2"></param>
    /// <returns>Return the number of tangents: 4, 2, or 0.</returns>
    public int FindCircleCircleTangents(Vector2 c1, float radius1, Vector2 c2, float radius2,
        out Vector2 outer1_p1, out Vector2 outer1_p2, out Vector2 outer2_p1, out Vector2 outer2_p2,
        out Vector2 inner1_p1, out Vector2 inner1_p2, out Vector2 inner2_p1, out Vector2 inner2_p2)
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

    /// <summary>
    /// Find the tangent points for this circle and external point. Return true if we find the tangents, 
    /// false if the point is inside the circle.
    /// </summary>
    /// <param name="center"></param>
    /// <param name="radius"></param>
    /// <param name="external_point"></param>
    /// <param name="pt1"></param>
    /// <param name="pt2"></param>
    /// <returns></returns>
    public bool FindTangents(Vector2 center, float radius,
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
    public int FindCircleCircleIntersections(float cx0, float cy0, float radius0, float cx1, float cy1, float radius1,
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
    /// Calculates intersection points between a line and a circle. 
    /// Point1 and 2 can be any coordinates along the line 
    /// </summary>
    /// <param name="circlePos">Coordinates of the center of the circle</param>
    /// <param name="radius">Circle's radius</param>
    /// <param name="point1">A coordinate on the line</param>
    /// <param name="point2">Another coordinate on the line</param>
    /// <returns>Two Vector2's of the two intersection points</returns>
    public Vector2[] findLineCircleIntersections(Vector2 circlePos, float radius, Vector2 point1, Vector2 point2)
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
    #endregion

    #region Antiquated Functions


    /// <summary>
    /// NOT USED IN CURRENT BUILD
    /// Calculates the equation of a line. 
    /// </summary>
    /// <param name="pos1">First Coordinate</param>
    /// <param name="pos2">Second Coordinate</param>
    /// <returns> Line's Gradient & Y-Intercept</returns>
    public Vector2 calculateLineEquation(Vector2 pos1, Vector2 pos2)
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
    /// NOT USED IN CURRENT BUILD
    /// Calculate point D where point D is where point A intersects line BC at a right angle
    /// </summary>
    /// <param name="nodeA">Current Node</param>
    /// <param name="nodeB">Next Node</param>
    /// <param name="nodeC">Previous Node</param>
    /// <returns>Vector2 of the coordinates of point D</returns>
    //Vector2 calculatePointDFromABC(Vector2 nodeA, Vector2 nodeB, Vector2 nodeC)
    //{
    //    double answerX;
    //    double answerY;

    //    Vector2 answer;
    //    Vector2 XCoords;

    //    // angle ADC = 90
    //    double angleDCA = calculateAngle(nodeB, nodeC, nodeA);
    //    double angleCAD = findLastAngleInTriangle(angleDCA, 90f);

    //    double sideAC = calculateDistance(nodeA, nodeC);
    //    double sideAD = calculateSideAAS(angleDCA, 90f, sideAC);
    //    double sideCD = calculateSideAAS(angleCAD, angleDCA, sideAD);
    //    Debug.Log(sideAD);
    //    Debug.Log(sideCD);
    //    Debug.Log(sideAC);

    //    //       AB^2 + AC^2 - BC^2     |              ___________
    //    //  Cy = -------------------    |   Cx = (+-) /AC^2 - Cy^2
    //    //             2AB              |              

    //    //answerY = ((sideAD * sideAD) + (sideAC * sideAC) - (sideCD * sideCD)) / (2 * sideAD);
    //    answerY = ((sideAC * sideAC) + (sideCD * sideCD) - (sideAD * sideAD)) / (2 * sideAC);
    //    answerX = Math.Sqrt((sideAC * sideAC) - (answerY * answerY));

    //    //Debug.Log(answerX + " : " + answerY);
    //    Debug.Log(answerY);

    //    //answer.y = ((sideAD * sideAD) + (sideAC * sideAC) - (sideCD * sideCD)) / 2 * sideAD;
    //    //XCoords = Math.Sqrt((sideAC * sideAC) - (answer.y * answer.y));


    //    return Vector2.zero;
    //}

    /// <summary>
    /// NOT USED IN CURRENT BUILD
    /// Calculates the size of a side with two angles and a side (AAS)
    /// </summary>
    /// <param name="angleA">Angle opposite side wanted</param>
    /// <param name="angleB">Angle opposite another side</param>
    /// <param name="sideB">Side of the other angle</param>
    /// <returns></returns>
    public double calculateSideAAS(double angleA, double angleB, double sideB)
    {
        double answer = (sideB * Math.Sin(convertToRadians(angleA))) / Math.Sin(convertToRadians(angleB));
        answer = makePositive(answer);
        return answer;
    }

    /// <summary>
    /// NOT USED IN CURRENT BUILD
    /// Takes two andles in a triangle and returns the last one in degrees
    /// </summary>
    /// <param name="angle1">First angle</param>
    /// <param name="angle2">Second angle</param>
    /// <returns></returns>
    public double findLastAngleInTriangle(double angle1, double angle2)
    {

        double answer = 180 - angle1 - angle2;

        return answer;
    }

    /// <summary>
    /// NOT USED IN CURRENT BUILD
    /// Takes a number and makes it positive
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public double makePositive(double number)
    {
        double answer = Math.Sqrt(number * number);
        return answer;
    }

    /// <summary>
    /// NOT USED IN CURRENT BUILD
    /// Converts degrees to radians for use in Math.Sin()
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public double convertToRadians(double angle)
    {
        return (Math.PI / 180) * angle;
    } 
    #endregion

    #region Test Area
    public void Start()
    {
        //Debug.Log(transformPosition(new Vector2(5, 5), new Vector2(0, 0)));
        Debug.Log(rotateNodeAroundNode(new Vector2(8f, 5f), new Vector2(5.0f, 5.0f), 60));
    }

    public void testRNDNormalDis()
    {
        int[] array = new int[100];
        int temp;

        for (int i = 0; i < array.Length; i++)
        {
            temp = RandomNormalDistributionINT(0, 10, 3);
            switch (temp)
            {
                case 0:
                    array[0] += 1;
                    break;
                case 1:
                    array[1] += 1;
                    break;
                case 2:
                    array[2] += 1;
                    break;
                case 3:
                    array[3] += 1;
                    break;
                case 4:
                    array[4] += 1;
                    break;
                case 5:
                    array[5] += 1;
                    break;
                case 6:
                    array[6] += 1;
                    break;
                case 7:
                    array[7] += 1;
                    break;
                case 8:
                    array[8] += 1;
                    break;
                case 9:
                    array[9] += 1;
                    break;
            }
        }
        Debug.Log(array[0] + "|" + array[1] + "|" + array[2] + "|" + array[3] + "|" +
                  array[4] + "|" + array[5] + "|" + array[6] + "|" + array[7] + "|" +
                  array[8] + "|" + array[9]);
    } 
    #endregion
}
