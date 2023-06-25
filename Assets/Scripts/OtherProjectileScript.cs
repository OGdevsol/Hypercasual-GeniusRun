using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherProjectileScript : MonoBehaviour
{
    private Transform target;
    private Vector3 targetPos;
    private Vector3 thisPos;
    private float angle;
    private float offset;
  //  private EnemyScript enemy;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
      //  enemy = FindObjectOfType<EnemyScript>();
        ShootProjectile();
        FacePlayer();
       // StartCoroutine(OtherProjectileDestroyTime());
    }

    protected virtual void FacePlayer()
    {
        offset = 170;
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
            (target.transform.position - transform.position).normalized * EnemyScript.instance.otherprojectileSpeedInLevel;
        StartCoroutine(ProjectileRoutine());
       // EnemyScript.instance.SpawnOtherProjectile();
    }

     protected virtual IEnumerator ProjectileRoutine()
     {
         yield return new WaitForSeconds(EnemyScript.instance.fireFrequencyOtherProjectileInLevel);
         if (EnemyScript.instance!=null)
         {
             Destroy(EnemyScript.instance.otherprojectile);
         }
        
         //enemy.SpawnOtherProjectile();
      //   StartCoroutine(OtherProjectileDestroyTime());
          }

    /*private IEnumerator OtherProjectileDestroyTime()
    {
        yield return new WaitForSeconds(enemy.otherProjectileDestroyTime);
    
    
        Destroy(this.gameObject);
    }*/

    private void OnDestroy()
    {
        if (EnemyScript.instance != null)
        {
            EnemyScript.instance.SpawnOtherProjectile();
        }
        
       
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            Debug.Log("CollisionWithPlayer");
        }
    }
}