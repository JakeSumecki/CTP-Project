using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight : MonoBehaviour {


    // Does this really need an index??
    ///// <summary>
    ///// Order of the straights
    ///// </summary>
    //private int index;

    /// <summary>
    /// Coordinates of first position
    /// </summary>
    private Vector2 position1;

    /// <summary>
    /// Coordinates of second position
    /// </summary>
    private Vector2 position2;

    /// <summary>
    /// Angle of the straight
    /// </summary>
    private float angle;


    #region Getters
    //public int getIndex() { return index; }
    public Vector2 getPos1() { return position1; }
    public Vector2 getPos2() { return position2; }
    public float getAngle() { return angle; }
    #endregion

    #region Setters
    public void setPos1(Vector2 a) { position1 = a; }
    public void setPos2(Vector2 b) { position2 = b; }
    public void setAngle(float c) { angle = c; }
    #endregion
}
