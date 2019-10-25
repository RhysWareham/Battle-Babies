using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour {

	public GameObject caster;

	[SerializeField]
	private LayerMask players;

	private void OnParticleCollision(GameObject other)
	{
		if (other.layer == 8)
		{
			if (other != caster)
			{
				other.GetComponent<PlayerController>().TakeDamage(10, new Vector2(other.transform.position.x, GetComponent<ParticleSystem>().transform.position.y), true, 10);
			}
		}
	}
	
}
