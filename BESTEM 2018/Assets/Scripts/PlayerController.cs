using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization


    Rigidbody rb;
    public float movementSpeed;
    public float rotationSpeed;
    Vector3 lookAtVector;
    private float g = 9.81f;
    private float mass;
    public bool canMove;


    Animator anim;

    public Transform cam;
    void Start() {
        lookAtVector = transform.position;
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        anim = GetComponent<Animator>();

        mass = rb.mass;
    }

    // Update is called once per frame
    void FixedUpdate() {

        if(canMove)
            Move();


    }

    

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("terrain"))
            canMove = true;
          

    }

    void OnTriggerExit(Collider other)
    {
        canMove = false;
        //anim.SetFloat("Blend", 0);


    }*/

    public void Move()
    {

        float z = 0;
        float x = 0;
        z = Input.GetAxis("Horizontal");
        x = Input.GetAxis("Vertical");


        rb.AddForce(new Vector3(0, -1, 0)*g*mass);


        if (z != 0 || x != 0)
        {


            Vector3 direction = new Vector3(x * cam.rotation.x, 0, z * cam.rotation.z);


            Vector3 destination = direction + transform.position;



            lookAtVector = transform.position + direction;

            transform.LookAt(lookAtVector);

            rb.AddForce(transform.forward * movementSpeed);

            //rb.MovePosition(transform.position + transform.forward * Time.deltaTime);



           // rb.AddForce(direction * movementSpeed);



            //anim.SetFloat("Blend", 1f);




        }
        else
        {
            lookAtVector = transform.position;
            //anim.SetFloat("Blend", 0);

        }

        if(lookAtVector != null)
            transform.LookAt(lookAtVector);
    }


}
