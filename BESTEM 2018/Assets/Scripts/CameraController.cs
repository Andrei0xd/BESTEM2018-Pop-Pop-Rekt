using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public Transform target;
    public Vector3 offset;
    public float pitch ;

    private float zoom = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = target.position - offset * zoom;
        transform.LookAt(target.position + Vector3.up * pitch);

		
    }
}
