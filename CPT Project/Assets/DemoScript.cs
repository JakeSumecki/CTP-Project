using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour {

    //Camera Stuff
    public GameObject camera;
    public Transform[] views;
    float transitionSpeed;
    Transform currentView;
    

    public GameObject[] P1Stage1;
    public GameObject[] P1Stage2;
    public GameObject[] P1Stage3;
    public GameObject[] P1Stage4;


    bool pressed = true;
    int stepIterator = 0;

    // Use this for initialization
    void Start () {
        //instantiatePrefabs(P1Stage1);
        transitionSpeed = 2f;

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
                    instantiatePrefabs(P1Stage1);
                    break;
                case 2:
                    instantiatePrefabs(P1Stage2);
                    break;
                case 3:
                    instantiatePrefabs(P1Stage3);
                    break;
                case 4:
                    instantiatePrefabs(P1Stage4);
                    break;
                case 5:
                    currentView = views[0];
                    break;
            }
        }
        pressed = false;
	}

    void LateUpdate()
    {
        camera.transform.position = Vector3.Lerp(camera.transform.position, currentView.position, Time.deltaTime * transitionSpeed);
    }

    void instantiatePrefabs(GameObject[] array)
    {

        for (int i = 0; i < array.Length; i++)

        {
           Instantiate(array[i]);
        }

        return;
    }
}
