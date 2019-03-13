using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathsFunctions : MonoBehaviour {

    private void Start()
    {
        //Debug.Log(transformPosition(new Vector2(5, 5), new Vector2(0, 0)));
        Debug.Log(rotateNodeAroundNode(new Vector2(8f,5f), new Vector2(5.0f ,5.0f), 60));
    }

    /// <summary>
    /// /// Returns a number between minNumber and maxNumber with a normal distribution
    /// strength corresponds to hot strong the pull is towards the center
    /// </summary>
    /// <param name="minNumber">minimum number</param>
    /// <param name="maxNumber">maximum number</param>
    /// <param name="strength">How strong the "pull" is towards the centre</param>
    /// <returns></returns>
    public int RandomNormalDistributionINT(float minNumber, float maxNumber, int strength)
    {
        float answerFlt = 0f;
        for (int i = 0; i < strength; i++)
        {
            answerFlt += UnityEngine.Random.Range(minNumber, maxNumber);
        }

        // average the number
        answerFlt = answerFlt / strength;

        int answerInt = (int)answerFlt;

        return answerInt;
    }

    /// <summary>
    /// /// Returns a number between minNumber and maxNumber with a normal distribution
    /// strength corresponds to hot strong the pull is towards the center
    /// </summary>
    /// <param name="minNumber">minimum number</param>
    /// <param name="maxNumber">maximum number</param>
    /// <param name="strength">How strong the "pull" is towards the centre</param>
    /// <returns></returns>
    public float RandomNormalDistributionFLT(float minNumber, float maxNumber, int strength)
    {
        float answerFlt = 0f;
        for (int i = 0; i < strength; i++)
        {
            answerFlt += UnityEngine.Random.Range(minNumber, maxNumber);
        }

        // average the number
        answerFlt = answerFlt / strength;

        return answerFlt;
    }

    /// <summary>
    /// Takes two positions and adds them together to give the position of Node 1 on Node 2
    /// For use in placing nodes after calculated
    /// </summary>
    /// <param name="node">The node to be moved. Origin should be (0,0)</param>
    /// <param name="transformation">The new origin of the node</param>
    /// <returns></returns>
    public Vector2 transformPosition(Vector2 node, Vector2 transformation)
    {
        Vector2 result = node + transformation;
        return result;
    }

    /// <summary>
    /// Rotates one point around another
    /// </summary>
    /// <param name="pointToRotate">The point to rotate.</param>
    /// <param name="centerPoint">The center point of rotation.</param>
    /// <param name="angleInDegrees">The rotation angle in degrees.</param>
    /// <returns>Rotated point</returns>
    public Vector2 rotateNodeAroundNode(Vector2 pointToRotate, Vector2 centerPoint, double angleInDegrees)
    {
        double angleInRadians = angleInDegrees * (Math.PI / 180);
        double cosTheta = Math.Cos(angleInRadians);
        double sinTheta = Math.Sin(angleInRadians);
        return new Vector2(
            (float)(cosTheta * (pointToRotate.x - centerPoint.x) - sinTheta * (pointToRotate.y - centerPoint.y) + centerPoint.x)
            ,
            (float)((sinTheta * (pointToRotate.x - centerPoint.x) + cosTheta * (pointToRotate.y - centerPoint.y) + centerPoint.y)));
    }

    #region Test Area
    public void testRNDNormalDis()
    {
        int[] array = new int[100];
        int temp;

        for (int i = 0; i < array.Length; i++)
        {
            temp = RandomNormalDistributionINT(0, 10, 3);
            switch (temp)
            {
                case 0:
                    array[0] += 1;
                    break;
                case 1:
                    array[1] += 1;
                    break;
                case 2:
                    array[2] += 1;
                    break;
                case 3:
                    array[3] += 1;
                    break;
                case 4:
                    array[4] += 1;
                    break;
                case 5:
                    array[5] += 1;
                    break;
                case 6:
                    array[6] += 1;
                    break;
                case 7:
                    array[7] += 1;
                    break;
                case 8:
                    array[8] += 1;
                    break;
                case 9:
                    array[9] += 1;
                    break;
            }
        }
        Debug.Log(array[0] + "|" + array[1] + "|" + array[2] + "|" + array[3] + "|" +
                  array[4] + "|" + array[5] + "|" + array[6] + "|" + array[7] + "|" +
                  array[8] + "|" + array[9]);
    } 
    #endregion
}
