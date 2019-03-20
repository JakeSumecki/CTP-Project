using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGenerator : MonoBehaviour {

    #region Variables
    private MathsFunctions mathsFunctions;
    private Phase1Stats stats;

    List<Corner> tempCorners = new List<Corner>();
    Corner tempCorner = new Corner();

    private enum typeOfProcGenEnum
    {
        COMPLETE_RANDOM,
        PURE_NORMAL_DISTRIBUTION_CO_2,
        PURE_NORMAL_DISTRIBUTION_CO_3,
        PURE_NORMAL_DISTRIBUTION_CO_5,
        NORMAL_DISTRIBUTION_CUSTOM,
        ALGORITHM_1,

    };

    private enum trackSizeEnum
    {
        CUSTOM,
        SMALL,
        MEDIUM,
        LARGE
    };

    private enum cornerstoneLayoutEnum
    {
        PERFECT_SQAURE,
        PERFECT_RECTANGLE_WIDE,
        PREFECT_RECTANGLE_NARROW,
        ROUGH_SQAURE,
        ROUGH_TRAPEZIUM,
        LEFT_SKEW_PARALLELOGRAM,
        RIGHT_SKEW_PARALLELOGRAM
    };
    #endregion

    #region Inspector
    [SerializeField]
    private bool RunProgram;

    [SerializeField]
    private bool VisualizeCoordinates;

    public GameObject nodeObject; 

    #region Procedural Generation
    [Header("Procedural generation")]
    [SerializeField]
    private bool GenerateStats;
    [SerializeField]
    private typeOfProcGenEnum typeOfProcGen;
    #endregion

    #region Base Stats
    [Header("Base Stats")]
    [SerializeField]
    private trackSizeEnum trackSize;

    [SerializeField]
    [Range(3100.0f, 5850.0f)]
    private float LengthOfTrack = 3100.0f;

    [SerializeField]
    [Range(10, 23)]
    private int AmountOfCorners = 10;

    #endregion

    #region Cornerstones
    [Header("Stage 1 Cornerstones")]
    [SerializeField]
    [Range(3, 5)]
    private int AmountOfCornerstones = 4;
    #endregion

    #region Alcoves
    [Header("Stage 2 Alcoves")]
    [SerializeField]
    [Range(0, 3)]
    private int AmountOfAlcoves = 0;
    #endregion

    #region Distortion 1
    [Header("Stage 3 Distortion 1")]
    [SerializeField]
    [Range(0, 5)]
    private int AmountOfDistortion1 = 2;
    #endregion

    #region Distortion 2
    [Header("Stage 4 Distortion 2")]
    [SerializeField]
    [Range(4, 15)]
    private int AmountOfDistortion2 = 4;
    #endregion

    #region Node Generation
    [Header("Node Generation")]
    [SerializeField]
    private bool GenerateNodes;

    [SerializeField]
    private cornerstoneLayoutEnum cornerstoneLayout;
    #endregion
    #endregion

    private void Start()
    {
        mathsFunctions = GameObject.FindObjectOfType<MathsFunctions>();
        stats = GameObject.FindObjectOfType<Phase1Stats>();
    }

    private void Update()
    {

        // generate Stats and then turn off button in inspector
        if (GenerateStats)
        {
            generateStats();
            GenerateStats = false;
        }

        if (GenerateNodes)
        {
            generateNodes();
            GenerateNodes = false;
        }

        // set track size
        switch (trackSize)
        {
            case trackSizeEnum.CUSTOM:
                break;
            case trackSizeEnum.SMALL:
                LengthOfTrack = 3100.0f;
                AmountOfCorners = 10;
                AmountOfCornerstones = 4;
                AmountOfAlcoves = 1;
                AmountOfDistortion1 = 1;
                AmountOfDistortion2 = 4;
                break;
            case trackSizeEnum.MEDIUM:
                LengthOfTrack = 4475.0f;
                AmountOfCorners = 17;
                AmountOfCornerstones = 4;
                AmountOfAlcoves = 2;
                AmountOfDistortion1 = 3;
                AmountOfDistortion2 = 8;
                break;
            case trackSizeEnum.LARGE:
                LengthOfTrack = 5850.0f;
                AmountOfCorners = 23;
                AmountOfCornerstones = 4;
                AmountOfAlcoves = 2;
                AmountOfDistortion1 = 5;
                AmountOfDistortion2 = 12;
                break;
            default:
                break;
        }
    }

    private void generateStats()
    {
        trackSize = trackSizeEnum.CUSTOM;

        switch (typeOfProcGen)
        {
            case typeOfProcGenEnum.COMPLETE_RANDOM:
                completeRandomStats();
                break;
            case typeOfProcGenEnum.PURE_NORMAL_DISTRIBUTION_CO_2:
                normalDistributionStats(2);
                break;
            case typeOfProcGenEnum.PURE_NORMAL_DISTRIBUTION_CO_3:
                normalDistributionStats(3);
                break;
            case typeOfProcGenEnum.PURE_NORMAL_DISTRIBUTION_CO_5:
                normalDistributionStats(5);
                break;
            case typeOfProcGenEnum.NORMAL_DISTRIBUTION_CUSTOM:
                normalDistributionStatsCustom();
                break;
            case typeOfProcGenEnum.ALGORITHM_1:
                algorithm1();
                break;
        }
    }

    private void generateNodes()
    {
        sizeAndPlaceCornerstones();
    }

    #region Node Quantity Generation Functions
    private void normalDistributionStats(int coefficient)
    {
        LengthOfTrack = mathsFunctions.RandomNormalDistributionFLT(stats.getTrackSizeMin(), stats.getTrackSizeMax(), coefficient);
        AmountOfCorners = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfCornersMin(), stats.getAmountOfCornersMax(), coefficient);
        AmountOfCornerstones = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfCornerstonesMin(), stats.getAmountOfCornerstonesMax(), coefficient);
        AmountOfAlcoves = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfAlcovesMin(), stats.getAmountOfAlcovesMax(), coefficient);
        AmountOfDistortion1 = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfDistortion1Min(), stats.getAmountOfDistortion1Max(), coefficient);
        AmountOfDistortion2 = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfDistortion2Min(), stats.getAmountOfDistortion2Max(), coefficient);
    }
    private void normalDistributionStatsCustom()
    {
        LengthOfTrack = mathsFunctions.RandomNormalDistributionFLT(stats.getTrackSizeMin(), stats.getTrackSizeMax(), 2);
        AmountOfCorners = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfCornersMin(), stats.getAmountOfCornersMax(), 2);
        AmountOfCornerstones = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfCornerstonesMin(), stats.getAmountOfCornerstonesMax(), 10);
        AmountOfAlcoves = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfAlcovesMin(), stats.getAmountOfAlcovesMax(), 5);
        AmountOfDistortion1 = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfDistortion1Min(), stats.getAmountOfDistortion1Max(), 2);
        AmountOfDistortion2 = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfDistortion2Min(), stats.getAmountOfDistortion2Max(), 2);
    }
    private void completeRandomStats()
    {
        LengthOfTrack = UnityEngine.Random.Range(stats.getTrackSizeMin(), stats.getTrackSizeMax());
        AmountOfCorners = UnityEngine.Random.Range(stats.getAmountOfCornersMin(), stats.getAmountOfCornersMax());
        AmountOfCornerstones = UnityEngine.Random.Range(stats.getAmountOfCornerstonesMin(), stats.getAmountOfCornerstonesMax());
        AmountOfAlcoves = UnityEngine.Random.Range(stats.getAmountOfAlcovesMin(), stats.getAmountOfAlcovesMax());
        AmountOfDistortion1 = UnityEngine.Random.Range(stats.getAmountOfDistortion1Min(), stats.getAmountOfDistortion1Max());
        AmountOfDistortion2 = UnityEngine.Random.Range(stats.getAmountOfDistortion2Min(), stats.getAmountOfDistortion2Max());
    }

    private void algorithm1()
    {
        // create amount of corners & track length
        createCornerAmountandTrackLengthCorrelated(2, 500);

        // create cornerstones
        AmountOfCornerstones = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfCornerstonesMin(), stats.getAmountOfCornerstonesMax(), 5);

        // create alcoves
        AmountOfAlcoves = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfAlcovesMin(), stats.getAmountOfAlcovesMax(), 5);

        // create distortion nodes
        createDistortion1andDistortion2withRatio(30, 5);


    }

    /// <summary>
    /// Creates amount of corners for the track whilst keeping them correlated to each other
    /// </summary>
    /// <param name="lengthVariation"> decides how much (max) the final length can be from the average</param>
    private void createCornerAmountandTrackLengthCorrelated(int cornerCoefficient, float lengthVariation)
    {
        float tempLength;
        float lengthInterval;

        float lengthRange = stats.getTrackSizeMax() - stats.getTrackSizeMin();
        int cornerRange = stats.getAmountOfCornersMax() - stats.getAmountOfCornersMin();

        // length interval = the average interval as it relates to the amount of corners
        lengthInterval = lengthRange / cornerRange;

        // figure out amount of corners
        AmountOfCorners = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfCornersMin(), stats.getAmountOfCornersMax(), cornerCoefficient);

        // get length of track as it DIRECTLY correlates to amount of corners
        tempLength = stats.getTrackSizeMin() + (lengthInterval * (AmountOfCorners - stats.getAmountOfCornersMin()));

        // vary new length a bit
        tempLength += mathsFunctions.RandomNormalDistributionFLT(-lengthVariation, lengthVariation, 5);

        // if track length goes over limit cap it
        if (tempLength > stats.getTrackSizeMax())
        {
            tempLength = stats.getTrackSizeMax();
        }
        if (tempLength < stats.getTrackSizeMin())
        {
            tempLength = stats.getTrackSizeMin();
        }

        // set the track length
        LengthOfTrack = tempLength;

    }

    /// <summary>
    /// Creates the amount of distortion nodes at a ratio set
    /// </summary>
    /// <param name="percent"> % of nodes to be Distortion1 nodes </param>
    /// <param name="variation"></param>
    private void createDistortion1andDistortion2withRatio(float percent, float variation)
    {
        // add variation to the percentage
        int tempPercent = (int)(percent + UnityEngine.Random.Range(-(variation / 2), (variation / 2)));

        // find out how many corners are left to place
        int cornersLeft = AmountOfCorners - AmountOfCornerstones - AmountOfAlcoves;

        AmountOfDistortion1 = (int)(cornersLeft * (tempPercent / 100.0f));
        AmountOfDistortion2 = cornersLeft - AmountOfDistortion1;

    }
    #endregion

    #region Size and Placement Functions

    private void sizeAndPlaceCornerstones()
    {
        //set up inital corner at (0,0)
        //float tempRadius = 5.0f;
        //tempCorner.setUpCorner(Vector2.zero, -1, tempRadius, true, Vector2.negativeInfinity, 0);
        //tempCorners.Add(tempCorner);

        cornerstonePlacementMethods();

    }

    private void sizeAndPlaceAlcoves()
    {

    }
    private void sizeAndPlaceDistortion1()
    {

    }
    private void sizeAndPlaceDistortion2()
    {

    }
    private void storeCornerQuantityData()
    {


    } 
    #endregion

    /// <summary>
    /// Holds different method of creating cornerstones (the outline of the whole track) 
    /// </summary>
    /// <param name="methodSelector"></param>
    private void cornerstonePlacementMethods()
    {
        //Variables used in calculations
        float trackLeft = LengthOfTrack;
        float sideA, sideB, sideC, sideD;
        float angleAB, angleBC;

        if (AmountOfCornerstones == 4)
        {
            switch (cornerstoneLayout)
            {
                //==============CREATES A PERFECT SQAURE=======================//
                case cornerstoneLayoutEnum.PERFECT_SQAURE:
                    sideA = LengthOfTrack / 4;
                    setUpCornerstones(sideA, sideA, sideA, 90.0f, 90.0f);            
                    break;

                //==============CREATES A SHORT PERFECT RECTANGLE==============//
                case cornerstoneLayoutEnum.PERFECT_RECTANGLE_WIDE:
                    // get a quarter of the track. Use side A as a temp
                    sideA = (LengthOfTrack / 8);

                    // get a value between 1/8 && 1/4 to use as width of sideA
                    sideB = sideA + UnityEngine.Random.Range(0, sideA);

                    // how much track is left
                    trackLeft = LengthOfTrack - (sideB * 2);

                    sideA = trackLeft / 2;

                    //side A is used for side C because theyre the same (rectangle)
                    setUpCornerstones(sideA, sideB, sideA, 90.0f, 90.0f);
                    break;

                //==============CREATES A WIDE PERFECT RECTANGLE==============//
                case cornerstoneLayoutEnum.PREFECT_RECTANGLE_NARROW:
                    // get a quarter of the track. Use side A as a temp
                    sideA = (LengthOfTrack / 8);

                    // get a value between 1/8 && 1/4 to use as width of sideA. Added 5.0f to make sure theres room for the corner
                    sideB = sideA + 5.0f - UnityEngine.Random.Range(0, sideA);

                    // how much track is left
                    trackLeft = LengthOfTrack - (sideB * 2);

                    sideA = trackLeft / 2;

                    //side A is used for side C because theyre the same (rectangle)
                    setUpCornerstones(sideA, sideB, sideA, 90.0f, 90.0f);
                    break;

                //=================CREATES A ROUGH SQAURE=====================//
                case cornerstoneLayoutEnum.ROUGH_SQAURE:
                    // Gives sideA a length of  1/4 of the length of track +/- 10% 
                    sideA = (UnityEngine.Random.Range(LengthOfTrack - (LengthOfTrack / 10), LengthOfTrack + (LengthOfTrack / 10))) / 4;
                    trackLeft -= sideA;

                    // Gives sideB a length of  1/4 of the length of track +/- 10% 
                    sideC = (UnityEngine.Random.Range(LengthOfTrack - (LengthOfTrack / 10), LengthOfTrack + (LengthOfTrack / 10))) / 4;
                    trackLeft -= sideC;

                    // takes half of whats left to use of track length
                    sideB = (LengthOfTrack - trackLeft) / 2;

                    //create angle roughly 90 degrees
                    angleAB = mathsFunctions.RandomNormalDistributionFLT(80.0f, 100.0f, 15);
                    angleBC = mathsFunctions.RandomNormalDistributionFLT(80.0f, 100.0f, 15);

                    setUpCornerstones(sideA, sideB, sideC, angleAB, angleBC);
                    break;
                case cornerstoneLayoutEnum.ROUGH_TRAPEZIUM:


                    break;
            }
        }
    }

    /// <summary>                                                                     B
    ///                                                                         -------------
    /// </summary>                                                              |AB/     \BC|
    /// <param name="sideA">Size of sideA</param>                               |           |
    /// <param name="sideB">Size of sideB</param>                             A |           | C
    /// <param name="sideC">Size of sideC</param>                               |           | 
    /// <param name="angleAB">Size of angle AB</param>                          |___________|
    /// <param name="angleBC">Size of angle BC</param>                                D
    private void setUpCornerstones(float sideA, float sideB, float sideC, float angleAB, float angleBC)
    {
        tempCorners.Clear();

        Vector2 node1 = Vector2.zero;
        Vector2 node2 = new Vector2(0.0f, sideA);
        Vector2 node3 = new Vector2(0.0f, sideB);
        Vector2 node4 = new Vector2(0.0f, sideC);

        float interimCornerSize = 5.0f;                 // REPLACE LATER

        //Node 1
        Corner cornerstone1 = new Corner();
        cornerstone1.setUpCorner(node1, -1, interimCornerSize, true, Vector2.negativeInfinity, 0);
        tempCorners.Add(cornerstone1);
        //Debug.Log(cornerstone11.getPlaceholderCoordinates());

        //Node 2 Node 1 (0,0) + sideA
        Corner cornerstone2 = new Corner();
        cornerstone2.setUpCorner(node2, -1, interimCornerSize, true, Vector2.negativeInfinity, 0);
        tempCorners.Add(cornerstone2);
        //Debug.Log(cornerstone2.getPlaceholderCoordinates());

        //Node 3 : Node 2 + Side A rotated by angleAB
        Corner cornerstone3 = new Corner();
        node3 = mathsFunctions.transformAndRotate(node3, node2, angleAB);
        cornerstone3.setUpCorner(node3, -1, interimCornerSize, true, Vector2.negativeInfinity, 0);
        tempCorners.Add(cornerstone3);
        //Debug.Log(cornerstone3.getPlaceholderCoordinates());

        //Node 4 : Node 3 + Side A rotated 90 rotated by angleBC + angleAB //Might need to change the angle part dependant on implimentation elsewhere
        Corner cornerstone4 = new Corner();
        node4 = mathsFunctions.transformAndRotate(node4, node3, angleBC + angleAB);
        cornerstone4.setUpCorner(node4, -1, interimCornerSize, true, Vector2.negativeInfinity, 0);
        tempCorners.Add(cornerstone4);
        //Debug.Log(cornerstone4.getPlaceholderCoordinates());

        //Debug.Log(node1 + " | " + node2 + " | " + node3 + " | " + node4);


        if (VisualizeCoordinates)
        {
            List<GameObject> go = new List<GameObject>();

            // destroy previous nodes
            for (int i = 0; i < go.Count; i++)
            {
                Destroy(go[i]);
            }

            //clear list
            go.Clear();

            //for loop is working correctly, data seems to be fucked
            for (int i = 0; i < tempCorners.Count; i++)
            {
                tempCorner = tempCorners[i];
                Debug.Log(tempCorner.getPlaceholderCoordinates());
                GameObject newGO = Instantiate(nodeObject, new Vector3(tempCorner.getPlaceholderCoordinates().x, 0.0f, tempCorner.getPlaceholderCoordinates().y), Quaternion.identity);
                go.Add(newGO);
            }
        }

    }

}




