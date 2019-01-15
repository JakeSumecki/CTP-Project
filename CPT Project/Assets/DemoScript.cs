using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour {

    //Camera Stuff
    public GameObject camera;
    public Transform[] views;
    float transitionSpeed;
    Transform currentView;
    

    public GameObject P1Stage1;
    public GameObject P1Stage2;
    public GameObject P1Stage3;
    public GameObject P1Stage4;

    public GameObject[] P2Stage1;
    public GameObject[] P2Stage2;

    bool pressed = true;
    int stepIterator = 0;

    GameObject prevGameObject1;
    GameObject prevGameObject2;
    GameObject prevGameObject3;
    GameObject prevGameObject4;

    // Use this for initialization
    void Start () {
        //instantiatePrefabs(P1Stage1);
        transitionSpeed = 3f;

        currentView = camera.transform;
	}
	
	// Update is called once per frame
	void Update () {
		

        if(Input.GetKeyDown(KeyCode.Space))
        {
            stepIterator++;
            pressed = true;
        }

        //Debug.Log(stepIterator);

        if(pressed)
        {
            switch (stepIterator)
            {
                case 0:
                    break;
                case 1:
                    prevGameObject1 = Instantiate(P1Stage1);
                    break;
                case 2:
                    prevGameObject2 = Instantiate(P1Stage2);
                    break;
                case 3:
                    prevGameObject3 = Instantiate(P1Stage3);
                    break;
                case 4:
                    prevGameObject4 = Instantiate(P1Stage4);
                    break;
                case 5:
                    currentView = views[0];
                    break;
                case 6:
                    // midpoint
                    prevGameObject1 = Instantiate(P2Stage1[0]);
                    break;
                case 7:
                    // line of symmetry
                    Destroy(prevGameObject1);
                    prevGameObject2 = Instantiate(P2Stage1[1]);
                    break;
                case 8:
                    //circle
                    prevGameObject3 = Instantiate(P2Stage1[2]);
                    break;
                case 9:
                    //Intersection points
                    prevGameObject4 = Instantiate(P2Stage1[3]);
                    break;
                case 10:
                    // closest to mid-point
                    Destroy(prevGameObject1);
                    Destroy(prevGameObject2);
                    Destroy(prevGameObject3);
                    prevGameObject1 = Instantiate(P2Stage1[4]);
                    break;
                case 11:
                    // highlight dot
                    Destroy(prevGameObject1);
                    prevGameObject1 = Instantiate(P2Stage1[5]);
                    break;
                case 12:
                    //Create Circle
                    prevGameObject2 = Instantiate(P2Stage1[6]);
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;
                case 16:
                    break;
                case 17:
                    break;
            }
        }
        pressed = false;
	}

    void LateUpdate()
    {
        camera.transform.position = Vector3.Lerp(camera.transform.position, currentView.position, Time.deltaTime * transitionSpeed);
    }

    //void instantiatePrefabs(GameObject[] array)
    //{

    //    for (int i = 0; i < array.Length; i++)

    //    {
    //       Instantiate(array[i]);
    //    }

    //    return;
    //}
}
