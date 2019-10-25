using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusAttacks : MonoBehaviour {

    [SerializeField]
    public GameObject lightening;
    public Transform firePoint;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown(PlayerController.controllerPrefix + "Fire1"))
        //{
        //    GameObject newBolt = (GameObject)Instantiate(lightening, transform.position, transform.rotation);
        //}

        if (Input.GetButtonDown("BButtonP1"))
        {
            Instantiate(lightening, firePoint.position, firePoint.rotation);
        }
        //if (Input.GetButtonDown("P1Heavy"))
        //{

        //}

    }

    void Shoot()
    {
        Instantiate(lightening, firePoint.position, firePoint.rotation);
    }
}
