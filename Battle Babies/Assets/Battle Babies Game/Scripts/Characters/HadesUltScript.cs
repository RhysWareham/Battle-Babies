using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesUltScript : MonoBehaviour {

	public GameObject caster;
	private float force = 5;
	private float dmgPerSec = 2;
	private float dmgDelay = 0.3f;
	//private float dmgTimer;


	private List<float> forces;
	private List<GameObject> playersInRange;
	private List<float> dmgTimers;

	// Use this for initialization
	void Start () {
		forces = new List<float>();
		playersInRange = new List<GameObject>();
		dmgTimers = new List<float>();
	}
	
	// Update is called once per frame
	void Update () {
		

		if (playersInRange.Count > 0)
		{
			for (int i = 0; i < playersInRange.Count; i++)
			{
					if (dmgTimers[i] <= 0)
					{
						playersInRange[i].GetComponent<PlayerController>().TakeDamage(dmgPerSec, transform.position, false, 0);
						forces[i] += force;
						dmgTimers[i] = dmgDelay;
					}
					else
					{
						dmgTimers[i] -= Time.deltaTime;
					}


				if (caster.GetComponent<Hades>().usingUlt == false)
				{
					RemovePlayer(playersInRange[i]);
				}
			}

			
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (playersInRange.Contains(collision.gameObject) == false && collision.gameObject.layer == 8 && caster.GetComponent<Hades>().usingUlt == true && collision.gameObject != caster)
		{
			playersInRange.Add(collision.gameObject);
			float newForce = 0;
			forces.Add(newForce);
			float newTimer = 0;
			dmgTimers.Add(newTimer);
			//Debug.Log("Added");
		}
		
	}


	private void OnTriggerExit2D(Collider2D collision)
	{

		//force = 0;
		//dmgTimer = 0;


		RemovePlayer(collision.gameObject);


	}

	private void RemovePlayer(GameObject playerInRange)
	{
		if (playersInRange.Contains(playerInRange) == true)
		{
			forces.RemoveAt(playersInRange.IndexOf(playerInRange));
			dmgTimers.RemoveAt(playersInRange.IndexOf(playerInRange));
			playersInRange.Remove(playerInRange);
			StartCoroutine(playerInRange.GetComponent<PlayerController>().Knockback2(force, transform.position));
			//Debug.Log("Removed");
		}
	}
}
