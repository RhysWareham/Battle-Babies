using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hades : PlayerController
{

	[SerializeField]
	private ParticleSystem psUltHades;

	public bool usingUlt = false;

	public Hades()
	{
		MoveSpeed *= 0.8f;
		JumpPower *= 1.0f;
		MaxHealth *= 1.5f;
		CurrentHealth = MaxHealth;
		AttackPower *= 1.5f;
	}

	public override void MeleeAttack()
	{
		base.MeleeAttack();
	}

	public override void RangedAttack()
	{
		projectile.GetComponent<OrbShoot>().caster = gameObject;
		base.RangedAttack();
    }

	public override void Special()
	{
		base.Special();
		psUltHades.Play();
		psUltHades.GetComponent<CircleCollider2D>().enabled = true;
		psUltHades.GetComponent<HadesUltScript>().caster = gameObject;
	}

	protected override void Update()
	{
		base.Update();
		if (psUltHades.isPlaying == true)
		{
			usingUlt = true;
		}
		else
		{
			usingUlt = false;
			psUltHades.GetComponent<CircleCollider2D>().enabled = false;
            animator.SetBool("ultAttacked", false);
		}
	}


}
