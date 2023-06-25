using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Transform target;
    private Vector3 targetPos;
    private Vector3 thisPos;
    private float angle;
    private float offset;
   // private EnemyScript enemy;

    public static EnemyProjectile instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
      //  enemy = FindObjectOfType<EnemyScript>();
        ShootProjectile();
        FacePlayer();
    }
    

    protected virtual void FacePlayer()
    {
        offset =170;
        targetPos = target.position;
        thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
    }

   

    private void ShootProjectile()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity =
            (target.transform.position - transform.position).normalized * EnemyScript.instance.enemyprojectileSpeedInLevel;
        StartCoroutine(ProjectileRoutine());
    }

    public virtual IEnumerator ProjectileRoutine()
    {
        yield return new WaitForSeconds(EnemyScript.instance.fireFrequencyEnemyProjectileInLevel);
        if (EnemyScript.instance!=null)
        {
            Destroy(EnemyScript.instance.projectile);
        }
       
        //EnemyScript.instance.SpawnPlaneProjectile();
        
    }

    private void OnDestroy()
    {
        if (EnemyScript.instance!=null)
        {
            EnemyScript.instance.SpawnPlaneProjectile();
        }
            
        
       
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag==("Player"))
        {
            Debug.Log("CollisionWithPlayer");
            
        }
    }

   
}