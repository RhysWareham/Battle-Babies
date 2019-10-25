using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poseidon : PlayerController
{

	[SerializeField]
	private ParticleSystem psUltPoseidon;

	public bool usingUlt = false;
	private float timeDelay = 5;
	private float timer;

	public Poseidon()
	{
		MoveSpeed *= 1.2f;
		JumpPower *= 1.1f;
		MaxHealth *= 0.8f;
		CurrentHealth = MaxHealth;
		AttackPower *= 1.2f;
	}


	public override void MeleeAttack()
	{
		base.MeleeAttack();
	}

	public override void RangedAttack()
	{
		projectile.GetComponent<TridentShoot>().caster = gameObject;
		base.RangedAttack();
        
    }

	public override void Special()
	{
		base.Special();
		usingUlt = true;

	}
	protected override void Update()
	{
		base.Update();

		if (usingUlt == true)
		{
			int counter = 0;
			Vector3 bottomLeft = new Vector3(0, 0, 0);
			Vector3 bottomRight = new Vector3(1, 0, 0);

			Vector3 bottomLeftWorld = Camera.main.ViewportToWorldPoint(bottomLeft);
			Vector3 bottomRightWorld = Camera.main.ViewportToWorldPoint(bottomRight);



			int rnd = Random.Range(3, 5);
			ParticleSystem[] particleSystems = new ParticleSystem[rnd];

			for (int i = 0; i < particleSystems.Length; i++)
			{
				particleSystems[i] = Instantiate(psUltPoseidon);
				int rndX = Random.Range((int)bottomLeftWorld.x, (int)bottomRightWorld.x);
				particleSystems[i].transform.position = new Vector3(rndX, particleSystems[i].transform.position.y, particleSystems[i].transform.position.z);


			}

			StartCoroutine(CamShake(0.2f, 1f, 0.02f));

			while (counter < particleSystems.Length)
			{
				if (timer <= 0)
				{
					particleSystems[counter].Play();
					counter++;
					timer = timeDelay;
				}
				else
				{
					timer -= Time.deltaTime;
				}
			}

			usingUlt = false;
			


		}
        else
        {
            animator.SetBool("ultAttacked", false);
        }
	}

		

	public IEnumerator CamShake(float shakePower, float shakeTime, float shakeSpeed)
	{
		for (float i = 0; i < shakeTime; i += (shakeSpeed * 4))
		{
			cam.transform.position = new Vector3(cam.transform.position.x + shakePower, cam.transform.position.y, cam.transform.position.z);
			yield return new WaitForSeconds(shakeSpeed);
			cam.transform.position = new Vector3(cam.transform.position.x - shakePower, cam.transform.position.y, cam.transform.position.z);
			yield return new WaitForSeconds(shakeSpeed);
			cam.transform.position = new Vector3(cam.transform.position.x - shakePower, cam.transform.position.y, cam.transform.position.z);
			yield return new WaitForSeconds(shakeSpeed);
			cam.transform.position = new Vector3(cam.transform.position.x + shakePower, cam.transform.position.y, cam.transform.position.z);
			yield return new WaitForSeconds(shakeSpeed);
		}

	}
}
