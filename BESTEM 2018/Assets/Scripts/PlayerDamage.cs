using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerDamage : MonoBehaviour
{

    public int numOfL;
    public float maxHp;
    public float hp;
    public float dmg;
    private int type;


   // public TextAsset test;
    public Texture tex;
    private float resp = 0f;
    private float timeUntillRespawn;
    public Camera cam;
    public GameObject projectilePrefabs;
    private GameObject projectile;
    public GameObject all;
    private Death death;
    public float bulletTime = 0.3f;
    private float bT = 0f;


    private float texWidth;
    private float texHeight;

    public int lasttype;


    void Start()
    {
        timeUntillRespawn = 2f;
        death = all.GetComponent<Death>();
        type = death.type;
        projectile = projectilePrefabs;
        print(projectile);
        hp = maxHp;
        resp = timeUntillRespawn;

        //exture2D tex = new Texture2D(4, 4);
       // tex.LoadImage(test.bytes);
       


        texWidth = tex.width;
        texHeight = tex.height;
       

    }


    void FixedUpdate()
    {


        type = death.type;
        Attack();

        if (Mathf.FloorToInt(hp) <= 0)
        {

            resp -= Time.deltaTime;


            if (resp <= 0)
            {

                death.death(lasttype, false);
                resp = timeUntillRespawn;
            }

        }
    }

 


    void OnGUI()
    {
        Vector2 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position);

        targetPos.x = targetPos.x - 27f;
        targetPos.y = targetPos.y + 15f;


        GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, 60, 20), hp + "/" + maxHp);


        if (death.numOfLife > 0)
        {
            var posRect =new Rect(50, 60, texWidth * death.numOfLife, texHeight);
            
            GUI.Box(posRect, "" );
           

            var texRect = new Rect(0, 0, 1.0f * death.numOfLife, 1.0f);
            //for(int i=0;i<death.numOfLife; i++)
                GUI.DrawTextureWithTexCoords(posRect, tex, texRect);
        }

    }

    


    private float dmgMultiplier(float dmg,int eType)
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
        //print("Took" + dmg + "damage");
        //print(this.type + " " + type);
        hp -= dmg;
        lasttype = type;
        
    }

    void Attack()
    {

        bT -= Time.deltaTime;
        //projectile: type
        //            baseDmg

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (bT > 0)
                return;

            bT = bulletTime;

            Vector3 newPos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
            Vector3 pos;


            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                pos = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);


                Vector3 projectileDirection = pos - transform.position;


                var bullet = (GameObject)Instantiate(
                    projectile,
                    newPos,
                    Quaternion.identity
                    );

                print("type : " + type);
                if (type == 0)
                {
                    bullet.AddComponent<projectile1>();
                    projectile1 proj = bullet.GetComponent<projectile1>();
                    proj.type = type;
                    proj.dmg = dmg;
                }
                else if (type == 1)
                {
                    bullet.AddComponent<projectile1>();
                    projectile1 proj = bullet.GetComponent<projectile1>();
                    print("HERE");
                    proj.type = this.type;
                    proj.dmg = this.dmg;
                }
                else if (type == 2)
                {
                    bullet.AddComponent<projectile1>();
                    projectile1 proj = bullet.GetComponent<projectile1>();
                    proj.type = type;
                    proj.dmg = dmg;
                }
                else if (type == 3)
                {
                    bullet.AddComponent<projectile1>();
                    projectile1 proj = bullet.GetComponent<projectile1>();
                    proj.type = type;
                    proj.dmg = dmg;
                }
                else if (type == 4)
                {
                    bullet.AddComponent<projectile1>();
                    projectile1 proj = bullet.GetComponent<projectile1>();
                    proj.type = type;
                    proj.dmg = dmg;
                }
                else if (type == 5)
                {
                    bullet.AddComponent<projectile1>();
                    projectile1 proj = bullet.GetComponent<projectile1>();
                    proj.type = type;
                    proj.dmg = dmg;
                }





                bullet.AddComponent<Rigidbody>();
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.velocity = projectileDirection * 9;

                Destroy(bullet, 2.0f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            //spell animation 2
            //spawn sphere
            //spell animatin AOE sphere
            //send sphere towards ground
        }


    }
}
