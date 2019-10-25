using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour {

	[SerializeField]
	private ParticleSystem portal;

	GameObject endPortal;
	private bool startedPlaying = false;


	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		

		if (portal.IsAlive() == false && startedPlaying == true)
		{
			startedPlaying = false;
			Destroy(gameObject);
			
		}
		else
		{
			startedPlaying = true;
		}



	}


	private void OnTriggerEnter2D(Collider2D other)
	{

			GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");

			foreach (GameObject p in portals)
			{
				if (p != gameObject)
				{
					endPortal = p;
					StartCoroutine(deactivatePortal(endPortal));
				}
			}

			other.gameObject.transform.position = endPortal.transform.position;
	}

	IEnumerator deactivatePortal(GameObject endPortal)
	{
		endPortal.GetComponent<BoxCollider2D>().enabled = false;
		yield return new WaitForSeconds(2);
		endPortal.GetComponent<BoxCollider2D>().enabled = true;
	}
}
