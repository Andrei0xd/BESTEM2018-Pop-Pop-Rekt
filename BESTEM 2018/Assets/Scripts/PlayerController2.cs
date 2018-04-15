using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class PlayerController2 : MonoBehaviour {



    NavMeshAgent agent;
    public LayerMask rayMask;
    public Transform cam;
    public Transform spawn;
    public GameObject all;
    public GameObject pass_br;
    public GameObject left;
    public GameObject right;
    private Death death;
    public bool leftpass = true;
    public GameObject end;
    public Transform enemies;
    public Transform enemies_end;

    public float bulletTime = 0.3f;
    private float bT=0f;
    Animator anim;
    //Rigidbody rb;


    void Start () {

        
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        death = all.GetComponent<Death>();
        //rb = GetComponent<Rigidbody>();
        //death.death(0);


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("deadly_zone")) {
            //print("DEAD");
            //Time.timeScale = 0;
            agent.Warp(spawn.position);
            death.death(death.type, false);

            enemies.gameObject.SetActive(true);
            foreach (Transform enemie in enemies)
            {
                enemie.position = enemie.gameObject.GetComponent<enemyController>().tr;
                print("LUL");
            }

            enemies_end.gameObject.SetActive(false);
            foreach (Transform enemie in enemies_end)
            {
                enemie.position = enemie.gameObject.GetComponent<enemyController>().tr;
                print("LUL");
            }

        } else if (other.gameObject.CompareTag("soul")) {
            if (other.gameObject.name == "Fire")
                death.death(0, true);
            else if (other.gameObject.name == "Water")
                death.death(1, true);
            else if (other.gameObject.name == "Air")
                death.death(2, true);
            else if (other.gameObject.name == "Earth")
                death.death(3, true);
            else if (other.gameObject.name == "Ice")
                death.death(4, true);
            else
                death.death(5, true);
        } else if (other.gameObject.CompareTag("fire")) {
            if (death.type != 0 && death.type != 1) {
                agent.Warp(spawn.position);
                death.death(death.type, false);
            } else if (death.type == 1) {
                pass_br.GetComponent<ShutFire>().Shut();
                pass_br.SetActive(false);
                print("Calling shut");
            }
        } else if (other.gameObject.CompareTag("pass")) {
            if(leftpass == true)
            {
                left.SetActive(false);
                right.SetActive(true);
                leftpass = false;
            } else
            {
                left.SetActive(true);
                right.SetActive(false);
                leftpass = true;
            }
        } else if (other.gameObject.CompareTag("end")) {
            if(death.type == 4)
            {
                end.SetActive(true);
                SceneManager.LoadScene("lastmenu 1");
            } else
            {
                agent.Warp(spawn.position);
                death.death(death.type, false);
            }
        }

    }


    void Update () {

        bT -= Time.deltaTime;

        float z = Input.GetAxis("Horizontal");
        float x = Input.GetAxis("Vertical");

        if ( x!=0 || z !=0)
        {
            agent.isStopped = false;
            Vector3 p = new Vector3(x * cam.rotation.x, 0, z * cam.rotation.z);
            p += transform.position;
            agent.SetDestination(p);

            transform.LookAt(p);
            anim.SetFloat("Blend", 1f);


        } else {
            anim.SetFloat("Blend", 0);
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
        }



    }




}
