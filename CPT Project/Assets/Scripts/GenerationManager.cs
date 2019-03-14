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
                break;
            case typeOfProcGenEnum.PURE_NORMAL_DISTRIBUTION_CO_2:
                normalDistributionStats(2);
                break;
            case typeOfProcGenEnum.PURE_NORMAL_DISTRIBUTION_CO_3:
                normalDistributionStats(100);
                break;
            case typeOfProcGenEnum.NORMAL_DISTRIBUTION_CUSTOM:
                normalDistributionStats(2);
                break;
            case typeOfProcGenEnum.ALGORITHM_1:
                break;
        }
    }

    private void normalDistributionStats(int coefficient)
    {
        LengthOfTrack = mathsFunctions.RandomNormalDistributionFLT(stats.getTrackSizeMin(), stats.getTrackSizeMax(), coefficient);
        AmountOfCorners = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfCornersMin(), stats.getAmountOfCornersMax(), coefficient);
        AmountOfCornerstones = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfCornerstonesMin(), stats.getAmountOfCornerstonesMax(), coefficient);
        AmountOfAlcoves = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfAlcovesMin(), stats.getAmountOfAlcovesMax(), coefficient);
        AmountOfDistortion1 = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfDistortion1Min(), stats.getAmountOfDistortion1Max(), coefficient);
        AmountOfDistortion2 = mathsFunctions.RandomNormalDistributionINT(stats.getAmountOfDistortion2Min(), stats.getAmountOfDistortion2Max(), coefficient);
    }

}

