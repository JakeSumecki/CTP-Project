using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager : MonoBehaviour {

    [SerializeField]
    private int specificCorner = 0;
    [SerializeField]
    bool cornerPos = true;
    [SerializeField]
    bool cornerOrder = true;
    [SerializeField]
    bool cornerDirection = true;
    [SerializeField]
    bool cornerRadius = true;
    [SerializeField]
    private bool output = false;

    private GameData gameData;

    int numberOfCorners;
    Vector2[] hardCodeCoords;
    int[] hardCodeCornerOrder;
    bool[] hardCodeTurningDirection;
    float[] hardCodeCornerRadius;

    // Use this for initialization
    void Awake () {
        // link gameData
        gameData = GameObject.FindObjectOfType<GameData>();

        numberOfCorners = 14;
        gameData.setAmountOfCorners(numberOfCorners);

        previousData();


    }

    // Update is called once per frame
    void Update () {

        runDebugOutput();

    }
    
    private void runDebugOutput()
    {
        if (output && specificCorner == 0)
        {
            Debug.Log("Total Corners" + numberOfCorners);

            for (int i = 0; i < numberOfCorners; i++)
            {

                int j = i + 1;
                Debug.Log("Corner Number :" + j);

                if (cornerPos)
                {
                    Debug.Log("Corner Coordinates : " + hardCodeCoords[i]);
                }
                if (cornerOrder)
                {
                    Debug.Log("Corner Direction : " + hardCodeTurningDirection[i]);
                }
                if (cornerDirection)
                {
                    Debug.Log("Corner Order : " + hardCodeCornerOrder[i]);
                }
                if (cornerRadius) 
                {
                    Debug.Log("Corner Radius : " + hardCodeCornerRadius[i]);
                }
            }
        }
        else if(output && specificCorner != 0)
        {
            Debug.Log("Corner Number :" + specificCorner);
            if (cornerPos)
            {
                Debug.Log("Corner Coordinates : " + hardCodeCoords[specificCorner - 1]);
            }
            if (cornerOrder)
            {
                Debug.Log("Corner Direction : " + hardCodeTurningDirection[specificCorner - 1]);
            }
            if (cornerDirection)
            {
                Debug.Log("Corner Order : " + hardCodeCornerOrder[specificCorner - 1]);
            }
            if (cornerRadius)
            {
                Debug.Log("Corner Radius : " + hardCodeCornerRadius[specificCorner - 1]);
            }
        }
        output = false;
        return;
    }

    void previousData()
    {
        hardCodeCoords = new Vector2[]
        {
        new Vector2(0f,0f),
        new Vector2(0.84f,-8.95f),
        new Vector2(-0.72f,-19.82f),
        new Vector2(0f,-30f),
        new Vector2(-4.3f,-29.32f),
        new Vector2(-9.04f,-25.41f),
        new Vector2(-17.62f,-21.07f),
        new Vector2(-14.27f,-16.14f),
        new Vector2(-4.85f,-15.99f),
        new Vector2(-5.82f,-7.37f),
        new Vector2(-10.75f,-8.68f),
        new Vector2(-23.98f,-4.49f),
        new Vector2(-24.5f,2.41f),
        new Vector2(-19.44f,3.93f)
        };

        hardCodeCornerOrder = new int[] {  1,
                                               4,
                                               7,
                                               13,
                                               9,
                                               10,
                                               8,
                                               11,
                                               12,
                                               2,
                                               3,
                                               5,
                                               6,
                                               14
                                             };

        hardCodeTurningDirection = new bool[] {  true,
                                                        true,
                                                        false,
                                                        true,
                                                        true,
                                                        false,
                                                        true,
                                                        true,
                                                        false,
                                                        false,
                                                        true,
                                                        true,
                                                        true,
                                                        true
                                                            };

        hardCodeCornerRadius = new float[] {  0.75f,
                                               0.625f,
                                               0.625f,
                                               0.75f,
                                               0.75f,
                                               0.75f,
                                               1f,
                                               5f,
                                               2.5f,
                                               2.5f,
                                               2f,
                                               2f,
                                               1.5f,
                                               0.625f
                                             };

        // link these to gameData
        gameData.setCornerCoords(hardCodeCoords);
        gameData.setCornerOrder(hardCodeCornerOrder);
        gameData.setCornerTurningDirection(hardCodeTurningDirection);
        gameData.setCornerRadius(hardCodeCornerRadius);
    }
}
