using System;
using System.Collections;
using System.Collections.Generic;
//using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private GameObject[] enemyprojectiles;
    [SerializeField] private GameObject[] otherprojectiles;
    private Transform target;
    private Vector3 targetPos;
    private Vector3 thisPos;
    private float angle;
    [SerializeField] private Transform EnemyprojectileSpawnPoint;
    [SerializeField] private Transform OtherprojectileSpawnPoint;
    float maxMoveDistance = 2;
  

    Vector3 origin;

//    float speed = 1;
    public float fireFrequencyEnemyProjectileInLevel;
    public float fireFrequencyOtherProjectileInLevel;
    public float enemyprojectileSpeedInLevel;
    public float otherprojectileSpeedInLevel;
    public float projectileDestroyTime;
    public float otherProjectileDestroyTime;

  [HideInInspector]  public GameObject projectile;
   [HideInInspector] public GameObject otherprojectile;
    public static EnemyScript instance;


    public float offset;

    public int LevelNum;


    public int speed;
    public float updownspeed;
    private bool shouldmoveupdown;
    private bool ismobile = false;
    public GameObject movetotarget;
  //  public CinemachineVirtualCamera myvcam;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            instance = null;
            instance = this;
        }

      

        
    }

    private void setphonesettings()
    {
        Debug.Log("phoonneee");
        ismobile = true;
    //    myvcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = new Vector3(2, 0, 0);
        
    }

    private void OnDisable()
    {
        instance = null;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void Start()
    {
        target = player.transform;


        SpawnPlaneProjectile();
        SpawnOtherProjectile();
        PlayerPrefs.SetInt("Level", LevelNum);
        Debug.Log(LevelNum);
        if (Screen.width < Screen.height)
        {
            setphonesettings();
        }
    }


    private void LateUpdate()
    {
        FacePlayer();
        EnemyDistance();
    }

    private void FixedUpdate()
    {
        upanddown();
    }

    public void upanddown()
    {
        float y = Mathf.PingPong(Time.time * updownspeed, 1.2f) * 5 - 3;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    private void EnemyDistance()
    {
        if (!ismobile)
        {
            if (transform.position.x - player.transform.position.x < 11.9 && movetotarget.transform.position.x - transform.position.x > .4)
            {
                transform.position = Vector2.MoveTowards(transform.position, movetotarget.transform.position,
                    1 * speed * Time.deltaTime);
            }
            else if (transform.position.x - player.transform.position.x > 13)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
                    1 * speed * Time.deltaTime);
            }
        }

        if (ismobile)
        {
            if (transform.position.x - player.transform.position.x < 4 && movetotarget.transform.position.x - transform.position.x > .2)
            {
                transform.position = Vector2.MoveTowards(transform.position, movetotarget.transform.position,
                    1 * speed * Time.deltaTime);
            }
            else if (transform.position.x - player.transform.position.x > 4.1)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
                    1 * speed * Time.deltaTime);
            }
        }
    }

    IEnumerator spawnplaneprojectile()
    {
        yield return new WaitForSeconds(3);
        projectile = Instantiate(enemyprojectiles[Random.Range(0, enemyprojectiles.Length)],
            EnemyprojectileSpawnPoint.transform.position,
            EnemyprojectileSpawnPoint.transform.rotation);
    }


    public void SpawnPlaneProjectile()
    {
        StartCoroutine(spawnplaneprojectile());
    }

    IEnumerator spawnotherprojectile()
    {
        yield return new WaitForSeconds(3);
        otherprojectile = Instantiate(otherprojectiles[Random.Range(0, otherprojectiles.Length)],
            OtherprojectileSpawnPoint.transform.position,
            OtherprojectileSpawnPoint.transform.rotation);
    }

    public void SpawnOtherProjectile()
    {
        StartCoroutine(spawnotherprojectile());
    }

    protected virtual void FacePlayer()
    {
        targetPos = target.position;
        thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
    }
}