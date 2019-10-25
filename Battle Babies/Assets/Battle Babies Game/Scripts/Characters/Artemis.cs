using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Artemis : PlayerController
{
    public bool usingUlt = false;

	[SerializeField]
	private ParticleSystem ultArtemis;


	public Artemis()
	{
		MoveSpeed *= 1.5f;
		JumpPower *= 1.2f;
		MaxHealth *= 0.8f;
		CurrentHealth = MaxHealth;
	}

	public override void MeleeAttack()
	{
		base.MeleeAttack();
	}

	public override void RangedAttack()
	{
		projectile.GetComponent<ArrowShoot>().caster = gameObject;
		base.RangedAttack();
        
	}

	public override void Special()
	{
		base.Special();
		ultArtemis.Play();
		

	}

	protected override void Update()
	{
		base.Update();



		if (ultArtemis.isPlaying == true)
		{
			usingUlt = true;
			animator.SetBool("chargeEnded", true);
		}
		else
		{
			animator.SetBool("chargeEnded", false);
			usingUlt = false;
			animator.SetBool("ultAttacked", false);
		}
	}
}
