using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathsFunctions : MonoBehaviour {


    void Start()
    {
        testRNDNormalDis();
        Debug.Log("h");
    }

        public int RandomNormalDistribution(float maxNumber)
    {
        float answerFlt = Random.Range(0, maxNumber) + Random.Range(0, maxNumber);
        int answerInt = (int)answerFlt;

        return answerInt;
    }

    public void testRNDNormalDis()
    {
        int[] array = new int[10];
        int temp;

        for (int i = 0; i < array.Length; i++)
        {
            temp = RandomNormalDistribution(10);
            switch (temp)
            {
                case 0:
                    array[0] = array[0] + 1;
                    break;
                case 1:
                    array[1] = array[1] + 1;
                    break;
                case 2:
                    array[2] = array[2] + 1;
                    break;
                case 3:
                    array[3] = array[3] + 1;
                    break;
                case 4:
                    array[4] = array[4] + 1;
                    break;
                case 5:
                    array[5] = array[5] + 1;
                    break;
                case 6:
                    array[6] = array[6] + 1;
                    break;
                case 7:
                    array[7] = array[7] + 1;
                    break;
                case 8:
                    array[8] = array[8] + 1;
                    break;
                case 9:
                    array[9] = array[9] + 1;
                    break;
                case 10:
                    array[10] = array[9] + 1;
                    break;
            }
        }
        Debug.Log(array[0] + "|" + array[1] + "|" + array[2] + "|" + array[3] + "|" +
                  array[4] + "|" + array[5] + "|" + array[6] + "|" + array[7] + "|" +
                  array[8] + "|" + array[9] + "|" + array[10]);
    }
}
