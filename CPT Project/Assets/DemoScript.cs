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

    public GameObject line1;
    public GameObject line2;
    public GameObject line3;
    public GameObject line4;

    public GameObject[] P2Stage1;
    public GameObject[] P2Stage2;

    public GameObject tanPoints1;
    public GameObject tanPoints2;
    public GameObject tanPointsFin;

    public GameObject Circles;

    bool pressed = true;
    int stepIterator = 0;

    GameObject prevGameObject1;
    GameObject prevGameObject2;
    GameObject prevGameObject3;
    GameObject prevGameObject4;
    GameObject dots1;
    GameObject dots2;
    GameObject dots3;
    GameObject dots4;
    GameObject lines1;
    GameObject lines2;
    GameObject lines3;
    GameObject lines4;
    GameObject circles;

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
                    dots1 = Instantiate(P1Stage1);
                    lines1 = Instantiate(line1);
                    break;
                case 2:
                    Destroy(lines1);
                    dots2 = Instantiate(P1Stage2);
                    lines2 = Instantiate(line2);
                    break;
                case 3:
                    Destroy(lines2);
                    dots3 = Instantiate(P1Stage3);
                    lines3 = Instantiate(line3);
                    break;
                case 4:
                    Destroy(lines3);
                    dots4 = Instantiate(P1Stage4);
                    lines4 = Instantiate(line4);
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
                    Destroy(prevGameObject4);
                    Destroy(prevGameObject1);
                    prevGameObject1 = Instantiate(P2Stage1[5]);
                    break;
                case 12:
                    //Create Circle
                    prevGameObject2 = Instantiate(P2Stage1[6]);
                    break;
                case 13:
                    // create all circles
                    Destroy(prevGameObject1);
                    Destroy(dots1);
                    Destroy(dots2);
                    Destroy(dots3);
                    Destroy(dots4);
                    Destroy(lines4);
                    circles = Instantiate(Circles);
                    break;
                case 14:
                    currentView = views[1];
                    break;
                case 15:
                    prevGameObject1 = Instantiate(tanPoints1);
                    break;
                case 16:
                    prevGameObject2 = Instantiate(tanPoints2);
                    break;
                case 17:
                    Destroy(prevGameObject1);
                    Destroy(prevGameObject2);
                    prevGameObject1 = Instantiate(tanPointsFin);
                    break;
            }
        }
        pressed = false;
	}

    void LateUpdate()
    {
        camera.transform.position = Vector3.Lerp(camera.transform.position, currentView.position, Time.deltaTime * transitionSpeed);

        Vector3 currentAngle = new Vector3(Mathf.LerpAngle(camera.transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
                                            Mathf.LerpAngle(camera.transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
                                            Mathf.LerpAngle(camera.transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

        camera.transform.eulerAngles = currentAngle;
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
