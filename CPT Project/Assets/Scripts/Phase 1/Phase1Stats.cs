using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Stats : MonoBehaviour {

    #region Variables
    //----------------------TRACK-STATS-------------------------//
    private float trackSizeMin;
    private float trackSizeMax;

    private int amountOfCornersMin;
    private int amountOfCornersMax;


    private int amountOfCornerstonesMin;
    private int amountOfCornerstonesMax;
    private float radiusOfCornerstonesMin;
    private float radiusOfCornerstonesMax;

    private int amountOfAlcovesMin;
    private int amountOfAlcovesMax;
    private float radiusOfAlcovesMin;
    private float radiusOfAlcovesMax;

    private int amountOfDistortion1Min;
    private int amountOfDistortion1Max;
    private float radiusOfDistortion1Min;
    private float radiusOfDistortion1Max;

    private int amountOfDistortion2Min;
    private int amountOfDistortion2Max;
    private float radiusOfDistortion2Min;
    private float radiusOfDistortion2Max;

    #endregion

    private void Awake()
    {
        trackSizeMin = 3100.0f;
        trackSizeMax = 5850.0f;

        amountOfCornersMin = 10;
        amountOfCornersMax = 23;

        amountOfCornerstonesMin = 3;
        amountOfCornerstonesMax = 5;
        radiusOfCornerstonesMin = 5.0f;
        radiusOfCornerstonesMax = 20.0f;

        amountOfAlcovesMin = 0;
        amountOfAlcovesMax = 4;
        radiusOfAlcovesMin = 0.0f;
        radiusOfAlcovesMax = 0.0f;

        amountOfDistortion1Min = 0;
        amountOfDistortion1Max = 5;
        radiusOfDistortion1Min = 0.0f;
        radiusOfDistortion1Max = 0.0f;

        amountOfDistortion2Min = 4;
        amountOfDistortion2Max = 15;
        radiusOfDistortion2Min = 0.0f;
        radiusOfDistortion2Max = 0.0f; 
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
