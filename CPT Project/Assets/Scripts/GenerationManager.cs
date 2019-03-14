using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager : MonoBehaviour
{
    #region Variables
    private MathsFunctions mathsFunctions;
    private Phase1Stats stats;

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
    #endregion

    #region Inspector
    [SerializeField]
    public bool RunProgram;

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
        if(tempLength > stats.getTrackSizeMax())
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
        int tempPercent = (int)(percent + UnityEngine.Random.Range(-(variation/2), (variation / 2)));
        Debug.Log(tempPercent);

        // find out how many corners are left to place
        int cornersLeft = AmountOfCorners - AmountOfCornerstones - AmountOfAlcoves;
        Debug.Log(cornersLeft);


        AmountOfDistortion1 = (int)(cornersLeft * (tempPercent / 100.0f));
        AmountOfDistortion2 = cornersLeft - AmountOfDistortion1;

        Debug.Log(AmountOfDistortion1);
        Debug.Log(AmountOfDistortion2);

    }
    #endregion

}

