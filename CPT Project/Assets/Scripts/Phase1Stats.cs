using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Stats : MonoBehaviour {

    #region Variables
    private float trackSizeMin;
    private float trackSizeMax;

    private int amountOfCornersMin;
    private int amountOfCornersMax;

    private int amountOfCornerstonesMin;
    private int amountOfCornerstonesMax;

    private int amountOfAlcovesMin;
    private int amountOfAlcovesMax;

    private int amountOfDistortion1Min;
    private int amountOfDistortion1Max;

    private int amountOfDistortion2Min;
    private int amountOfDistortion2Max; 
    #endregion

    private void Awake()
    {
        trackSizeMin = 3100.0f;
        trackSizeMax = 5850.0f;

        amountOfCornersMin = 10;
        amountOfCornersMax = 23;

        amountOfCornerstonesMin = 3;
        amountOfCornerstonesMax = 5;

        amountOfAlcovesMin = 0;
        amountOfAlcovesMax = 4;

        amountOfDistortion1Min = 0;
        amountOfDistortion1Max = 5;

        amountOfDistortion2Min = 4;
        amountOfDistortion2Max = 15;
    }

    #region Getters
    public float getTrackSizeMin()
    {
        return trackSizeMin;
    }
    public  float getTrackSizeMax()
    {
        return trackSizeMax;
    }
    public int getAmountOfCornersMin()
    {
        return amountOfCornersMin;
    }
    public int getAmountOfCornersMax()
    {
        return amountOfCornersMax;
    }
    public int getAmountOfCornerstonesMin()
    {
        return amountOfCornerstonesMin;
    }
    public int getAmountOfCornerstonesMax()
    {
        return amountOfCornerstonesMax;
    }
    public int getAmountOfAlcovesMin()
    {
        return amountOfAlcovesMin;
    }
    public int getAmountOfAlcovesMax()
    {
        return amountOfAlcovesMax;
    }
    public int getAmountOfDistortion1Min()
    {
        return amountOfDistortion1Min;
    }
    public int getAmountOfDistortion1Max()
    {
        return amountOfDistortion1Max;
    }
    public int getAmountOfDistortion2Min()
    {
        return amountOfDistortion2Min;
    }
    public int getAmountOfDistortion2Max()
    {
        return amountOfDistortion2Max;
    } 
    #endregion

}
