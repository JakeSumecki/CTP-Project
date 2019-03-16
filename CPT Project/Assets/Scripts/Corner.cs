using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner {

    private enum typeOfCornerEnum
    {
        CORNERSTONE,
        ALCOVE,
        DISTORTION1,
        DISTORTION2
    };

    // data generated to create corners
    #region Preliminary Corner Data
    private Vector2 placeholderCoordinates;    //coordinates of rough corner position
    #endregion
    #region Final Corner Data
    private int index;                  //corders order
    private float radius;              //radius of corner in degrees
    private bool direction;      //direction of corner (clockwise/counter clockwise)
    private Vector2 finalCoordinates;    // final coordinates of corners center    
    private typeOfCornerEnum typeOfCorner;
    #endregion

    #region Getters
    public Vector2 getPlaceholderCoordinates() { return placeholderCoordinates; }
    public int getIndex() { return index; }
    public float getRadius() { return radius; }
    public bool getDiretcion() { return direction; }
    public Vector2 getFinalCoordinates() { return finalCoordinates; }
    public int getCornerType() { return (int)typeOfCorner; }
    #endregion

    #region Setters
    public void setPlaceholderCoordinates(Vector2 a) { placeholderCoordinates = a; }
    public void setIndex(int b) { index = b; }
    public void setRadius(float c) { radius = c; }
    public void setDirection(bool d) { direction = d; }
    public void setFinalCoordinates(Vector2 e) { finalCoordinates = e; } 
    public void setCornerType(int f) { typeOfCorner = (typeOfCornerEnum)f; } 

    public void setUpCorner(Vector2 placeholderCoords, int index, float radius, bool dir, Vector2 finalCoordinates, int cornerType)
    {
        setPlaceholderCoordinates(placeholderCoords);
        setIndex(index);
        setRadius(radius);
        setDirection(dir);
        setFinalCoordinates(finalCoordinates);
        setCornerType(cornerType);
    }
    #endregion


}
