using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public static playercontroller instance;
    private InGameUIController UIController;
    private Rigidbody2D rb;
    public Transform ship;

    public GameObject[] lightsgroup1;
    public GameObject[] lightsgroup2;

    private float movehor;
    private float movever;

    [HideInInspector] public int netWorth;
    [HideInInspector] public int Assets;

    [HideInInspector] public int Liabilities;
    public TMP_Text minusText;


    public AudioSource coins;
    public AudioSource gems;
    public AudioSource lose;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
    }

    IEnumerator MinusScoreRoutine(int minusBy)
    {
        //    minusText.transform.position = EnemyScript.instance.player.transform.position;
        minusText.transform.gameObject.SetActive(true);
        minusText.text = minusBy.ToString();
        yield return new WaitForSeconds(0.4f);
        minusText.transform.gameObject.SetActive(false);
    }

    public GameObject coinEffect;


    void Start()
    {
        UIController = FindObjectOfType<InGameUIController>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(lights());
        PlayerPrefs.SetInt("Assets", 0);
        PlayerPrefs.SetInt("Liabilities", 0);
        // PlayerPrefs.SetInt("Networth", 0);
    }

    IEnumerator lights()
    {
        foreach (var VARIABLE in lightsgroup1)
        {
            VARIABLE.SetActive(true);
        }

        foreach (var VARIABLE in lightsgroup2)
        {
            VARIABLE.SetActive(false);
        }

        yield return new WaitForSeconds(1f);

        foreach (var VARIABLE in lightsgroup2)
        {
            VARIABLE.SetActive(true);
        }

        foreach (var VARIABLE in lightsgroup1)
        {
            VARIABLE.SetActive(false);
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(lights());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movehor = ControlFreak2.CF2Input.GetAxis("Horizontal");
        movever = ControlFreak2.CF2Input.GetAxis("Vertical");

        rb.velocity = new Vector2(5 * movehor, 5 * movever);

        if (movehor > 0)
        {
            if (transform.rotation.z > -.13)
            {
                transform.Rotate(-Vector3.forward * 50 * Time.deltaTime);
                //transform.localRotation = Quaternion.Euler(0,0,10);
            }
        }
        else if (movehor < 0)
        {
            if (transform.rotation.z < .13)
            {
                transform.Rotate(Vector3.forward * 50 * Time.deltaTime);
            }
        }

        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, quaternion.identity, Time.deltaTime * 5);

            //transform.rotation = new Quaternion(0,0,0,0);
        }

        //rb.AddForce(new Vector2(3*movehor,3*movever),ForceMode2D.Force);
        /*if (Input.GetButton("Vertical"))
        {
            rb.velocity = new Vector2(0,2);
        }
        
        if (Input.GetButton("Horizontal"))
        {
            rb.velocity = new Vector2(2,rb.velocity.y);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "coin")
        {
            Instantiate(coinEffect, other.transform.position, other.transform.rotation);
            coins.Play();
            other.gameObject.SetActive(false);
            IncreaseAssetsByVal(10);
            UIController.assetsTextInGame.text = PlayerPrefs.GetInt("Assets").ToString();

            CalculateAndShowNetWorth();
            //   UIController.assetsTextInLevelComplete.text = PlayerPrefs.GetInt("Assets").ToString();

            ;
        }

        if (other.tag == "BlueGem")
        {
            gems.Play();
            other.gameObject.SetActive(false);
            IncreaseAssetsByVal(20);
            UIController.assetsTextInGame.text = PlayerPrefs.GetInt("Assets").ToString();
            CalculateAndShowNetWorth();
            //   UIController.assetsTextInLevelComplete.text = PlayerPrefs.GetInt("Assets").ToString();

            ;
        }

        if (other.tag == "PinkGem")
        {
            gems.Play();

            other.gameObject.SetActive(false);
            IncreaseAssetsByVal(100);
            UIController.assetsTextInGame.text = PlayerPrefs.GetInt("Assets").ToString();
            CalculateAndShowNetWorth();
            //   UIController.assetsTextInLevelComplete.text = PlayerPrefs.GetInt("Assets").ToString();

            ;
        }

        if (other.tag == "GreenGem")
        {
            gems.Play();

            other.gameObject.SetActive(false);
            IncreaseAssetsByVal(40);
            UIController.assetsTextInGame.text = PlayerPrefs.GetInt("Assets").ToString();
            CalculateAndShowNetWorth();
            //   UIController.assetsTextInLevelComplete.text = PlayerPrefs.GetInt("Assets").ToString();

            ;
        }

        if (other.tag == "YellowGem")
        {
            gems.Play();

            other.gameObject.SetActive(false);
            IncreaseAssetsByVal(10);
            UIController.assetsTextInGame.text = PlayerPrefs.GetInt("Assets").ToString();
            CalculateAndShowNetWorth();
            //   UIController.assetsTextInLevelComplete.text = PlayerPrefs.GetInt("Assets").ToString();

            ;
        }

        if (other.tag == "PinkBlueGem")
        {
            gems.Play();

            other.gameObject.SetActive(false);
            IncreaseAssetsByVal(30);
            UIController.assetsTextInGame.text = PlayerPrefs.GetInt("Assets").ToString();
            CalculateAndShowNetWorth();
            //   UIController.assetsTextInLevelComplete.text = PlayerPrefs.GetInt("Assets").ToString();

            ;
        }

        if (other.tag == "PurpleGem")
        {
            gems.Play();

            other.gameObject.SetActive(false);
            IncreaseAssetsByVal(50);
            UIController.assetsTextInGame.text = PlayerPrefs.GetInt("Assets").ToString();
            CalculateAndShowNetWorth();
            //   UIController.assetsTextInLevelComplete.text = PlayerPrefs.GetInt("Assets").ToString();

            ;
        }

        if (other.tag == "Bill")
        {
            IncreaseLiabilitiesByVal(10);
            StartCoroutine(MinusScoreRoutine(-10));
            UIController.liabilitiesTextInGame.text = PlayerPrefs.GetInt("Liabilities").ToString();
            if (EnemyScript.instance != null)
            {
                Destroy(EnemyScript.instance.projectile);
            }

            CalculateAndShowNetWorth();
            //  EnemyProjectile.instance.StartCoroutine( EnemyProjectile.instance.ProjectileRoutine()) ;
            //  Destroy(other.gameObject);
            //   UIController.assetsTextInLevelComplete.text = PlayerPrefs.GetInt("Assets").ToString();

            ;
        }

        if (other.tag == "Loan")
        {
            IncreaseLiabilitiesByVal(30);
            StartCoroutine(MinusScoreRoutine(-30));
            // EnemyProjectile.instance.StartCoroutine( EnemyProjectile.instance.ProjectileRoutine()) ;
            UIController.liabilitiesTextInGame.text = PlayerPrefs.GetInt("Liabilities").ToString();
            if (EnemyScript.instance != null)
            {
                Destroy(EnemyScript.instance.projectile);
            }

            CalculateAndShowNetWorth();
            //   Destroy(other.gameObject);
            //   UIController.assetsTextInLevelComplete.text = PlayerPrefs.GetInt("Assets").ToString();

            ;
        }

        if (other.tag == "Debt")
        {
            IncreaseLiabilitiesByVal(50);
            StartCoroutine(MinusScoreRoutine(-30));
            //  EnemyProjectile.instance.StartCoroutine( EnemyProjectile.instance.ProjectileRoutine()) ;
            UIController.liabilitiesTextInGame.text = PlayerPrefs.GetInt("Liabilities").ToString();
            if (EnemyScript.instance != null)
            {
                Destroy(EnemyScript.instance.projectile);
            }

            CalculateAndShowNetWorth();
            //    Destroy(other.gameObject);
            //   UIController.assetsTextInLevelComplete.text = PlayerPrefs.GetInt("Assets").ToString();

            ;
        }

        if (other.tag == "Purple")
        {
            IncreaseLiabilitiesByVal(50);
            StartCoroutine(MinusScoreRoutine(-50));
            UIController.liabilitiesTextInGame.text = PlayerPrefs.GetInt("Liabilities").ToString();
            if (EnemyScript.instance != null)
            {
                Destroy(EnemyScript.instance.otherprojectile);
            }

            CalculateAndShowNetWorth();
            //   UIController.assetsTextInLevelComplete.text = PlayerPrefs.GetInt("Assets").ToString();

            ;
        }

        if (other.tag == "Rainbow")
        {
            IncreaseAssetsByVal(40);
            StartCoroutine(MinusScoreRoutine(-40));
            UIController.liabilitiesTextInGame.text = PlayerPrefs.GetInt("Liabilities").ToString();
            if (EnemyScript.instance != null)
            {
                Destroy(EnemyScript.instance.otherprojectile);
            }

            CalculateAndShowNetWorth();
            //   UIController.assetsTextInLevelComplete.text = PlayerPrefs.GetInt("Assets").ToString();

            ;
        }

        if (other.tag == "Blue")
        {
            IncreaseLiabilitiesByVal(30);
            StartCoroutine(MinusScoreRoutine(-30));
            UIController.liabilitiesTextInGame.text = PlayerPrefs.GetInt("Liabilities").ToString();
            if (EnemyScript.instance != null)
            {
                Destroy(EnemyScript.instance.otherprojectile);
            }

            CalculateAndShowNetWorth();
            //   UIController.assetsTextInLevelComplete.text = PlayerPrefs.GetInt("Assets").ToString();

            ;
        }

        if (other.tag == "Brown")
        {
            IncreaseLiabilitiesByVal(10);
            StartCoroutine(MinusScoreRoutine(-10));
            UIController.liabilitiesTextInGame.text = PlayerPrefs.GetInt("Liabilities").ToString();
            if (EnemyScript.instance != null)
            {
                Destroy(EnemyScript.instance.otherprojectile);
            }

//            Destroy(EnemyScript.instance.otherprojectile);
            CalculateAndShowNetWorth();
            //   UIController.assetsTextInLevelComplete.text = PlayerPrefs.GetInt("Assets").ToString();

            ;
        }

        if (other.tag == "Green")
        {
            IncreaseLiabilitiesByVal(20);
            StartCoroutine(MinusScoreRoutine(-20));
            UIController.liabilitiesTextInGame.text = PlayerPrefs.GetInt("Liabilities").ToString();
            if (EnemyScript.instance != null)
            {
                Destroy(EnemyScript.instance.otherprojectile);
            }

            //    Destroy(EnemyScript.instance.otherprojectile);
            CalculateAndShowNetWorth();
            //   UIController.assetsTextInLevelComplete.text = PlayerPrefs.GetInt("Assets").ToString();

            ;
        }


        Debug.Log(Assets);
    }

    void IncreaseAssetsByVal(int IncreaseBy)
    {
        int totalAssetsValue = PlayerPrefs.GetInt("Assets");
        Assets += IncreaseBy;
        PlayerPrefs.SetInt("Assets", totalAssetsValue + IncreaseBy);
    }

    void IncreaseLiabilitiesByVal(int IncreasedBy)
    {
        int totalLiabilitiesValue = PlayerPrefs.GetInt("Liabilities");
        Liabilities += IncreasedBy;
        PlayerPrefs.SetInt("Liabilities", totalLiabilitiesValue + IncreasedBy);
    }

    void CalculateAndShowNetWorth()
    {
        netWorth = Assets - Liabilities;
        UIController.netWorthTextInGame.text = netWorth.ToString();
        UIController.netWorthTextInLevelComplete.text = netWorth.ToString();
    }
}