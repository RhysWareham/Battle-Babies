using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltPoseidon : MonoBehaviour
{

	public GameObject caster;
	private float dmgPerSec = 5;
	private float dmgDelay = 0.3f;
	private float dmgTimer;
	private bool psStarted = false;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{


		if (GetComponent<ParticleSystem>().IsAlive() == false && caster.GetComponent<Poseidon>().usingUlt == false)
		{
			Destroy(gameObject);
		}
	}

	private void OnParticleCollision(GameObject other)
	{
		if (caster.GetComponent<Poseidon>().usingUlt == true && other.layer == 8 && other != caster)
		{
			if (dmgTimer <= 0)
			{
				other.GetComponent<PlayerController>().TakeDamage(dmgPerSec, new Vector2(other.transform.position.x, GetComponent<ParticleSystem>().transform.position.y), false, 0);
				dmgTimer = dmgDelay;
			}
			else
			{
				dmgTimer -= Time.deltaTime;
			}
		}
	}
}
