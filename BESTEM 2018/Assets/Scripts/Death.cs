using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {

    public Transform spawn;
    public GameObject[] elements;
    public GameObject[] souls;
    public Camera cam;
    public GameObject currentp;
    public int type = 0;
    public Transform enemies;
    public Transform enemies_end;
    private PlayerDamage pd;
    public int numOfLife;

    public void death(int tip, bool re)
    {
        numOfLife--;
        if (numOfLife <= 0)
        {
            SceneManager.LoadScene("lastmenu");
            print("TEST");
        }
        else
        {

            if (re)
            {
                elements[type].SetActive(false);
                elements[tip].GetComponent<NavMeshAgent>().Warp(elements[type].transform.position);
                souls[tip].SetActive(false);
                elements[tip].SetActive(true);

                cam.GetComponent<CameraController>().target = elements[tip].transform;
                pd = elements[tip].GetComponent<PlayerDamage>();
                PlayerDamage pd2 = elements[type].GetComponent<PlayerDamage>();
                pd.hp = pd2.hp;
                type = tip;
                currentp = elements[type];
            }
            else
            {
                elements[type].SetActive(false);
                souls[type].transform.position = elements[type].transform.position;
                souls[type].SetActive(true);
                elements[type].GetComponent<NavMeshAgent>().Warp(spawn.position);
                type = tip;
                elements[type].GetComponent<NavMeshAgent>().Warp(spawn.position);
                elements[type].SetActive(true);


                foreach (Transform enemie in enemies)
                {
                    enemie.position = enemie.gameObject.GetComponent<enemyController>().tr;
                    print("LUL");
                }

                foreach (Transform enemie in enemies_end)
                {
                    enemie.position = enemie.gameObject.GetComponent<enemyController>().tr;
                    print("LUL");
                }


                //NavMeshAgent nav = elements[type].GetComponent<NavMeshAgent>();
                cam.GetComponent<CameraController>().target = elements[type].transform;
                pd = elements[type].GetComponent<PlayerDamage>();
                pd.hp = pd.maxHp;
                currentp = elements[type];
            }
            //print("IN DEATH");
            //print(type);
        }
    }

}
