using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkManager : MonoBehaviour {

    #region bools for inspector
    /*[SerializeField]
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
    private bool output = false;*/ 
    #endregion

    // local version of gameData
    private GameData gameData;
    private MathsFunctions mathsFunctions;

    // might want to move this, bit hacky
    List<Straight> tempStraights = new List<Straight>();

    #region Variables
    Vector2 midPoint;
    Vector2 testLineEq;
    Vector2[] intersectionPoints;
    Vector2 circlePosFin;

    Vector2[] circlePositions = new Vector2[50]; 
    #endregion

    // Use this for initialization
    void Start () {

        // init gameData
        gameData = GameObject.FindObjectOfType<GameData>();
        mathsFunctions = GameObject.FindObjectOfType<MathsFunctions>();

        #region TestArea
        Vector2 o11 = new Vector2(-1.0f, -1.0f);
        Vector2 o12 = new Vector2(-1.0f, -1.0f);
        Vector2 o21 = new Vector2(-1.0f, -1.0f);
        Vector2 o22 = new Vector2(-1.0f, -1.0f);
        Vector2 i11 = new Vector2(-1.0f, -1.0f);
        Vector2 i12 = new Vector2(-1.0f, -1.0f);
        Vector2 i21 = new Vector2(-1.0f, -1.0f);
        Vector2 i22 = new Vector2(-1.0f, -1.0f);

       mathsFunctions.FindCircleCircleTangents(new Vector2(0.0f, 0.0f), 1.0f, new Vector2(3.0f, 3.0f), 1.0f,
                      out o11, out o12, out o21, out o22, out i11, out i12, out i21, out i22);
        //Debug.Log(o11 + "|" + o12);
        //Debug.Log(o21 + "|" + o22);
        //Debug.Log(i11 + "|" + i12);
        //Debug.Log(i21 + "|" + i22);


        //private 
        #endregion

        createCirclesFromPlaceholderCoordinates();
        createStraightsFromCircleTangents();

    }

    ////--------------------------HIGH-LEVEL-FUNCTIONS------------------------------////

    /// <summary>
    /// Creates the circles and stores the coordinates in gameData
    /// Tested. Only thing that needs addressing is the point used to find the intersection points is wroing-ish (works for now).
    /// </summary>
    void createCirclesFromPlaceholderCoordinates()
    {

        // loop through all corners
        for (int i = 0; i < gameData.getAmountOfCorners(); i++)
        {
            //when at the start wont be able to access NodeC as its the last in the array
            if (i == 0)
            {
                circlePositions[i] = calculateCirclePosition(gameData.getPlaceholderCoordsAtIndex(i),                                   //NodeA
                                                             gameData.getPlaceholderCoordsAtIndex(i + 1),                               //NodeB
                                                             gameData.getPlaceholderCoordsAtIndex(gameData.getAmountOfCorners() - 1),   //NodeC
                                                             gameData.getRadiusAtIndex(i));                                             //Radius

            }

            //when at the end wont be able to access NodeB because its at the start
            else if (i == gameData.getAmountOfCorners() - 1)
            {
                circlePositions[i] = calculateCirclePosition(gameData.getPlaceholderCoordsAtIndex(i),       //NodeA
                                                             gameData.getPlaceholderCoordsAtIndex(0),       //NodeB
                                                             gameData.getPlaceholderCoordsAtIndex(i - 1),   //NodeC
                                                             gameData.getRadiusAtIndex(i));                 //Radius
            }
            //if not at the start or end use adjacent nodes as B & C
            else
            {
                
                circlePositions[i] = calculateCirclePosition(gameData.getPlaceholderCoordsAtIndex(i),       //NodeA
                                                             gameData.getPlaceholderCoordsAtIndex(i + 1),   //NodeB
                                                             gameData.getPlaceholderCoordsAtIndex(i - 1),   //NodeC
                                                             gameData.getRadiusAtIndex(i));                 //Radius
            }

            // put final positions into gameData
            gameData.setFinalCoordinatesAtIndex(i, circlePositions[i]);
            //Debug.Log(gameData.getFinalCoordinatesAtIndex(i));
        }
    }

    /// <summary>
    /// Creates the tracks straights from the tangents of the circles
    /// </summary>
    void createStraightsFromCircleTangents()
    {
        // loop through straight (amount of corners == amount of straights) 
        for (int i = 0; i < gameData.getAmountOfCorners(); i++)
        {
            //when at the end it wont be able to access the first corner
            if (i == gameData.getAmountOfCorners() - 1)
            {
                //calculate the tangents between the last and first corner
                calculateCorrectTangent(gameData.getCornerAtIndex(i), gameData.getCornerAtIndex(0));
            }
            else
            {
                // calculate the tangents between the current corner and the next
                calculateCorrectTangent(gameData.getCornerAtIndex(i), gameData.getCornerAtIndex(i+1));
            }
        }
    }

    ////--------------------------MEDIUM-LEVEL-FUNCTIONS----------------------------////

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
        midPoint = mathsFunctions.calculateMidPoint(nodeB, nodeC);
        intersectionPoints = mathsFunctions.findLineCircleIntersections(nodeA, radius, nodeA, midPoint); // midpoint is the wrong point to use. Need a point on line of reflection
        circlePosFin = checkIntersections(intersectionPoints[0], intersectionPoints[1], midPoint);
        //Instantiate(original, new Vector3(circlePosFin.x, 0.0f, circlePosFin.y), Quaternion rotation);

        return circlePosFin;
    }

    ////---------------------------LOW-LEVEL-FUNCTIONS------------------------------////

    /// <summary>
    /// Calculates the correct tangent to use
    /// </summary>
    /// <param name="corner1"></param>
    /// <param name="corner2"></param>
    private void calculateCorrectTangent(Corner corner1, Corner corner2)
    {

        Straight tempStraight = new Straight();

        int tangentSelector = -1;
        tangentSelector = directionLogic(corner1, corner2);


        // this is the problem!!
        //FindCircleCircleTangents(corner1.getFinalCoordinates(), 1.0f, corner2.getFinalCoordinates(), 1.0f,
        //              out o11, out o12, out o21, out o22, out i11, out i12, out i21, out i22);
        Vector2 o11, o12, o21, o22, i11, i12, i21, i22;
        mathsFunctions.FindCircleCircleTangents(new Vector2(0.0f, 0.0f), 1.0f, new Vector2(3.0f, 3.0f), 1.0f,
                      out o11, out o12, out o21, out o22, out i11, out i12, out i21, out i22);

        // select which tangents to use and then NOT WORKING CURRENTLY
        switch (tangentSelector)
        {
            // both circle's directions are positive - outer tangent 1
            case 1:
                tempStraight.setPos1(o11);
                tempStraight.setPos2(o12);
                tempStraight.setAngle(1f);  // needs actually calculating. currently using for debugging
                //Debug.Log(tempStraight.getAngle());
                //Debug.Log(tempStraight.getPos1());
                //Debug.Log(tempStraight.getPos2());
                //Debug.Log(o11);
                //Debug.Log(o12);
                break;

            // both circle's directions are negative - outer tangent 2
            case 2:
                tempStraight.setPos1(Vector2.zero);
                tempStraight.setPos2(Vector2.zero);
                tempStraight.setAngle(2f);
                break;

            // circle 1 positive, circle 2 negative
            case 3:
                tempStraight.setPos1(Vector2.zero);
                tempStraight.setPos2(Vector2.zero);
                tempStraight.setAngle(3f);
                break;

            // circle 1 negative, circle 2 positive
            case 4:
                tempStraight.setPos1(Vector2.zero);
                tempStraight.setPos2(Vector2.zero);
                tempStraight.setAngle(4f);
                break;
        }
        // add straight to a list of straights
        tempStraights.Add(tempStraight);

    }

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

        double D1 = mathsFunctions.calculateDistance(inter1, midPoint);
        double D2 = mathsFunctions.calculateDistance(inter2, midPoint);

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

    private int directionLogic(Corner corner1, Corner corner2)
    {
        //Tangent when both directions are positive (outer tangent 1) pretty sure
        if (corner1.getDiretcion() == true && corner2.getDiretcion() == true)
        {
            return 1;
        }
        //Tangent when both directions are negative (outer tangent 2)
        else if (corner1.getDiretcion() == false && corner2.getDiretcion() == false)
        {
            return  2;
        }
        // Tangent when 1 positive and 2 is negative (not figured out)
        else if (corner1.getDiretcion() == true && corner2.getDiretcion() == false)
        {
            return  3;
        }
        // Tangent when 1 negative and 2 is positive (not figured out)
        else if (corner1.getDiretcion() == false && corner2.getDiretcion() == true)
        {
            return 4;
        }
        return -1;
    }

    #region Debug Stuff
    /// <summary>
    /// Used to output data to Unity's debugger
    /// </summary>
    /*private void runOutput()
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
    }*/
    #endregion

    void Update()
    {
        //runOutput();
    }
}


