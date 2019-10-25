using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

	[SerializeField]
	private GameObject item;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

        collision.gameObject.GetComponent<PlayerController>().itemAcquired(item);
        

			
	}
}
