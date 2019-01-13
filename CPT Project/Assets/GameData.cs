using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    private int amountOfCorners;
    private Vector2[] cornerCoords;
    private int[] cornerOrder;
    private bool[] cornerTurningDirection;
    private float[] cornerRadius;


// -----------------GETTERS-----------------------//

    public int getAmountOfCorners()
    {
        return amountOfCorners;
    }

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

// -----------------SETTERS-----------------------//

    public void setAmountOfCorners(int a)
    {
        amountOfCorners = a;
    }

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

    // -----------------HARDCODED-STUFF-----------------------//

}
