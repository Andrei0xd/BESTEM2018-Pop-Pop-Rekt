using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour {


    public int type;
    public float maxHp;
    private float hp;
    public Transform targetPlayer;
    private NavMeshAgent agent;
    public int rangeRadius;
    public float aggroRadius;


    public float baseDmg; //Damage de baza, calculat fara type ( element) 
    public GameObject all;
    public Vector3 tr;


    private PlayerDamage pDamage;
    private Death death;

    public float cooldown;
    private float timer;
    private Animator anim;


    void Start()
    {
        hp = maxHp;
        agent = GetComponent<NavMeshAgent>();
        death = all.gameObject.GetComponent<Death>();
        targetPlayer = death.currentp.transform;
        pDamage = targetPlayer.GetComponent<PlayerDamage>();
        tr = transform.position;
        timer = 0f;
        anim = GetComponent<Animator>();

    }


    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            targetPlayer = death.currentp.transform;
            pDamage = targetPlayer.GetComponent<PlayerDamage>();
            Vector3 destination = transform.position + targetPlayer.position;
            float dist = Vector3.Distance(transform.position, targetPlayer.position);

            if (dist > rangeRadius && dist < aggroRadius)
            {
                agent.SetDestination(targetPlayer.position);
                anim.SetFloat("Blend", 1f);
            }
               
            else if (dist <= rangeRadius)
            {

                agent.SetDestination(transform.position);
                anim.SetFloat("Blend", 1f);
                Attack();

            }
            else
            {
                anim.SetFloat("Blend", 0f);
            }
            timer = cooldown;
        }
    }



    void OnGUI()
    {
        Vector2 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position);

        targetPos.x = targetPos.x - 27f;
        targetPos.y = targetPos.y + 15f;


        GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, 60, 20), hp + "/" + maxHp);


    }

    private float dmgMultiplier(float dmg, int eType)
    {

        float k = dmg;
        //if enemy type is 1 - Fire
        if (eType == 1)
        {
            if (this.type == 2)
                k *= 0.5f;
            else if (this.type == 2)
                k *= 0;
            else if (this.type == 5)
                k *= 2;

        }

        else if (eType == 2)
        {
            if (this.type == 1)
                k *= 2;
            else if (this.type == 6)
                k *= 1.75f;

        }
        else if (eType == 5)
        {
            if (this.type == 1)
                k *= 0.8f;
            else if (this.type == 6)
                k *= 0.5f;

        }
        else if (eType == 6)
        {

            if (this.type == 2)
                k *= 0.75f;
            else if (this.type == 5)
                k *= 2.5f;

        }

        return k;



    }

    public void TakeDmg(float dmg, int type)
    {
        dmg = dmgMultiplier(dmg, type);

        hp -= dmg;
        if (hp <= 0)
            Destroy(this.gameObject);
    }

    void Attack()
    {
        if(Mathf.FloorToInt(pDamage.hp) >= 0)
            pDamage.TakeDmg(baseDmg, type);
    }




}
