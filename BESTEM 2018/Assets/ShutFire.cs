using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutFire : MonoBehaviour {

    public GameObject[] fires;

	// Use this for initialization
	void Start () {
        		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Shut()
    {

        print("in shut");
        foreach (GameObject fire in fires)
        {
            fire.GetComponent<ParticleSystem>().Stop();

        }
    }
}
