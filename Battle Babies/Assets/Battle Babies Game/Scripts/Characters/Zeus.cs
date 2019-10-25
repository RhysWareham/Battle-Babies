using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeus : PlayerController
{
	[SerializeField]
	private ParticleSystem psUltCharging;
	[SerializeField]
	private ParticleSystem psUltLightning;

	private bool ultStarted = false;
	private bool ultCharged = false;
	private float timeDelay = 0.05f;
	private float timer;


	public Zeus()
	{
		MoveSpeed *= 1f;
		JumpPower *= 1f;
	}

	protected override void Update()
	{
		base.Update();
        if (psUltCharging.IsAlive() == true)
        {
            ultStarted = true;
            if(timer <= 0)
            {
                light.intensity -= 0.2f;
                timer = timeDelay;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        if (ultStarted == true && psUltCharging.IsAlive() == false)
        {
            //Debug.Log("Working");
            StartLightning();
            animator.SetBool("ultAttacked", false);
        }
        if (ultStarted == true && psUltLightning.isPlaying == true)
        {
            ultStarted = false;
        }

        if (ultStarted == false && light.intensity < lightDefIntensity)
        {

        }






		if (ultCharged == false && psUltCharging.IsAlive() == false && light.intensity < lightDefIntensity)
		{
			if (timer <= 0)
			{
				light.intensity += 0.2f;
				timer = timeDelay;
			}
			else
			{
				timer -= Time.deltaTime;
			}
		}



	}


	public override void MeleeAttack()
	{
		base.MeleeAttack();
	}

	public override void RangedAttack()
	{
		projectile.GetComponent<LighteningShoot>().caster = gameObject;
		base.RangedAttack();
        
    }

	public override void Special()
	{
		base.Special();
		psUltCharging.Play();
		psUltLightning = Instantiate(psUltLightning);
		psUltLightning.GetComponent<Lightning>().caster = gameObject;
	}

	





	private void StartLightning()
	{
		psUltLightning.Play();
	}
}
