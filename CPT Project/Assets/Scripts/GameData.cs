﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    #region Private Variables
    private int amountOfCorners;
    private List<Corner> corners = new List<Corner>();

    #region Generated Corner Variables
    private Vector2[] cornerCoords; // initial coordinates

    
    private int[] cornerOrder;
    private bool[] cornerTurningDirection;
    private float[] cornerRadius;
    //rivate Vector2 fin
    #endregion

    #endregion

    #region Getters
    public int getAmountOfCorners() { return amountOfCorners;}
    public Corner getCornerAtIndex(int a) { return corners[a]; }
    public List<Corner> getCornersAll() { return corners; }
    public Vector2 getPlaceholderCoordsAtIndex( int b) { return corners[b].getPlaceholderCoordinates(); }
    public int getIndexAtIndex(int c) { return corners[c].getIndex(); }
    public double getRadiusAtIndex(int d) { return corners[d].getRadius(); }
    public bool getDirectionAtIndex(int e) { return corners[e].getDiretcion(); }
    public Vector2 getFinalCoordinatesAtIndex(int f) { return corners[f].getFinalCoordinates(); }

    #region Antiquated delete later
    public Vector2 getCornerCoordsAtPos(int pos)
    {
        return cornerCoords[pos];
    }

    public int getCornerOrderAtPos(int pos)
    {
        return cornerOrder[pos];
    }

    public bool getCornerTurningDirectionAtPos(int pos)
    {
        return cornerTurningDirection[pos];
    }

    public float getCornerRadiusAtPos(int pos)
    {
        return cornerRadius[pos];
    } 
    #endregion
    #endregion

    #region Setters
    public void setCorners(List<Corner> temp) { corners = temp; }
    public void setAmountOfCorners(int a)
    {
        amountOfCorners = a;
    }
    public void addCorner(Corner a) { corners.Add(a); }
    public void setCornerAtindex(int index, Corner b) { corners[index] = b; }
    public void setPlaceholderCoordsAtIndex(int index, Vector2 c) { corners[index].setPlaceholderCoordinates(c); }
    public void setIndexAtIndex(int index, int d) { corners[index].setIndex(d); }
    public void setRadiusAtIndex (int index, double e) { corners[index].setRadius(e); }
    public void setDirectionAtIndex (int index, bool f) { corners[index].setDirection(f); }
    public void setFinalCoordinatesAtIndex(int index, Vector2 g) { corners[index].setFinalCoordinates(g); }

    #region Antiquated delete later
    public void setCornerCoords(Vector2[] b)
    {
        cornerCoords = b;
    }

    public void setCornerOrder(int[] c)
    {
        cornerOrder = c;
    }

    public void setCornerTurningDirection(bool[] d)
    {
        cornerTurningDirection = d;
    }

    public void setCornerRadius(float[] e)
    {
        cornerRadius = e;
    }  
    #endregion
    #endregion

}
