using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseidonAttack : MonoBehaviour {

    [SerializeField]
    public GameObject trident;
    public Transform firePoint;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("P2Light"))
        {
            Instantiate(trident, firePoint.position, firePoint.rotation);
        }
    }

    void Shoot()
    {
        Instantiate(trident, firePoint.position, firePoint.rotation);
    }
}
