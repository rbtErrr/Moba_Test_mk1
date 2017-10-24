using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {

    

    private Transform targetedEnemy;
    private bool enemyClicked;
    private bool walking;
    private Animator anim;
    private NavMeshAgent navAgent;
    private float nextFire;
    private float timeBetweenShots = 2f;

    private Ray ray;

    
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {

        Debug.Log(Input.touchCount);
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                ray = Camera.main.ScreenPointToRay(touch.position);
            }
        }
        else
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        RaycastHit hit;

        if (Input.GetButton("Fire2") || Input.touchCount > 0) { 

                if (Physics.Raycast(ray, out hit, 100))
                    {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        targetedEnemy = hit.transform;
                        enemyClicked = true;
                    }
                    else
                    {
                        walking = true;
                        navAgent.isStopped = false;
                        enemyClicked = false;
                        navAgent.destination = hit.point;
                    }
                }
        
                if (enemyClicked)
                {
                    MoveAndShoot();
                }

       
                anim.SetBool("IsWalking", walking);
        }
    }


    void MoveAndShoot()
    {
        if (targetedEnemy == null)
        {
            return;
        }
        navAgent.destination = targetedEnemy.position;
        transform.LookAt(targetedEnemy);
        if (Time.time > nextFire)
        {
            nextFire = Time.time + timeBetweenShots;
            Fire();
        }
    }

    void Fire()
    {
            walking = false;
            anim.SetTrigger("Attack");
            GameObject fireball = Instantiate(GameManager.instance.BulletPrefab, GameManager.instance.BulletSpawnPoint.position, GameManager.instance.BulletSpawnPoint.rotation) as GameObject;
            fireball.GetComponent<ParticleSystem>().Play();
            fireball.GetComponent<NavMeshAgent>().SetDestination(targetedEnemy.position);
            Destroy(fireball, 3.5f);
    }
}
