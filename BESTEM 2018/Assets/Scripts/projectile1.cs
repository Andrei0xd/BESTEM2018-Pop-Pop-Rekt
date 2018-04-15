using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile1 : MonoBehaviour {

    // Use this for initialization
    public float dmg;
    public int type;



	void Start () {
		
	}

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<enemyController>() != null)
        {
            print("GG");
            enemyController enemy = other.gameObject.GetComponent<enemyController>();
            enemy.TakeDmg(this.dmg, this.type);
        }

        //Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
