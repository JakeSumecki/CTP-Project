using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public Transform[] views;
    float transitionSpeed;
    Transform currentView;


    // Use this for initialization
    void Start () {
        transitionSpeed = 3f;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentView = views[0];
        }
	}

    //void LateUpdate()
    //{
    //    transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);
    //}
}
