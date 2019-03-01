using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour {

    // data generated to create corners
    #region Preliminary Corner Data
    private Vector2 placeholderCoordinates;    //coordinates of rough corner position
    #endregion
    #region Final Corner Data
    private int index;                  //corders order
    private double radius;              //radius of corner in degrees
    private bool direction;      //direction of corner (clockwise/counter clockwise)
    private Vector2 finalCoordinates;    // final coordinates of corners center     
    #endregion

    #region Getters
    public Vector2 getPlaceholderCoordinates() { return placeholderCoordinates; }
    public int getIndex() { return index; }
    public double getRadius() { return radius; }
    public bool getDiretcion() { return direction; }
    public Vector2 getFinalCoordinates() { return finalCoordinates; }
    #endregion

    #region Setters
    public void setPlaceholderCoordinates(Vector2 a) { placeholderCoordinates = a; }
    public void setIndex(int b) { index = b; }
    public void setRadius(double c) { radius = c; }
    public void setDirection(bool d) { direction = d; }
    public void setFinalCoordinates(Vector2 e) { finalCoordinates = e; } 
    #endregion


}
