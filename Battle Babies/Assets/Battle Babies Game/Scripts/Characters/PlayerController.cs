using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	[SerializeField]
	private GameObject dmgNumber;

	[SerializeField]
	protected Camera cam;
	[SerializeField]
	protected Light light;

	[SerializeField]
	private ParticleSystem UltAura;

    private Rigidbody2D rb;
    private float xVel = 0f;
	private float yVel = 0f;
	private bool facingRight = true;
	public string playerNumber = "P1";

	//Misc variables
	private float knockbackModX = 5f;
	private float knockbackModY = 1f;
	private bool isKnockbacked = false;
	private Vector3 camDefPos;
	private float camDefZoom;
	private float maxZoomLevel = 11;
	protected float lightDefIntensity;
	private bool isBlocking = false;


	//Item Variables
	private string itemHeld;
	private float itemLength = 10f;
	private float itemTimer;



	//PlayerStats
	protected float MaxHealth = 100;
	protected float CurrentHealth = 100;
	protected float MoveSpeed = 5;
	protected float JumpPower = 11;
	protected float AttackPower = 5;
	protected float LightAttackForce = 10;
	protected float HeavyAttackForce = 10;
	protected int critChance = 15;
	protected float critMultiplier = 1.5f;
    public float speed = 6;

    private bool isGrounded = true;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private int extraJumps;
    public int extraJumpValue;

    //UltStats
    protected float UltChargeMax = 35;
	protected float UltCharge = 0f;
	protected float UltChargeRate = 1f;
	protected float UltChargeTimeDelay = 1f;
	protected float UltChargeTimer;

	//Melee variables
	public float meleeRange;
	public Transform meleePos;
	private float timeBtwAttack;
	public float startTimeBtwAttack = 0.3f;
	public LayerMask whatIsEnemies;

    //Ranged variables
    [SerializeField]
    protected GameObject projectile;
    [SerializeField]
    protected Transform firePoint;


    [SerializeField]
    protected Animator animator;


	//UI Stuff
	public GameObject healthBar;
    public GameObject ultBar;
	public GameObject currentItem;
	public Sprite defaultItem;


    protected virtual void Start()
	{
        rb = GetComponent<Rigidbody2D>();

        cam = Camera.main;
		camDefPos = cam.transform.position;
		camDefZoom = cam.orthographicSize;
		light = GameObject.FindGameObjectWithTag("GameLighting").GetComponent<Light>();
		lightDefIntensity = light.intensity;

		GameplayHandler.activePlayers.Add(gameObject);


    }

	protected virtual void Update()
	{

		if (GameplayHandler.IsPaused() == false)
		{
			PlayerMove();

			if (UltChargeTimer <= 0)
			{
				if (UltCharge < UltChargeMax && UltAura.isEmitting == false)
				{
					ChargeUlt();
					UltChargeTimer = UltChargeTimeDelay;
				}
				else
				{
					UltAura.Play();
				}
			}
			else
			{
				UltChargeTimer -= Time.deltaTime;
			}

			isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);




			if (timeBtwAttack <= 0)
			{
				animator.SetBool("shortAttacked", false);
				animator.SetBool("longAttacked", false);
				if (Input.GetButtonDown("XButton" + playerNumber))
				{
					animator.SetBool("shortAttacked", true);
					MeleeAttack();
					timeBtwAttack = startTimeBtwAttack;
				}
				if (Input.GetButtonDown("BButton" + playerNumber))
				{
					animator.SetBool("longAttacked", true);
					RangedAttack();
					timeBtwAttack = startTimeBtwAttack;
				}

			}
			else
			{
				timeBtwAttack -= Time.deltaTime;
			}
			if (Input.GetAxis("LTButton" + playerNumber) > 0.2 && Input.GetAxis("RTButton" + playerNumber) > 0.2 && UltCharge >= UltChargeMax)
			{
				Special();
			}
			if (Input.GetButton("LBButton" + playerNumber) || Input.GetButton("RBButton" + playerNumber))
			{
				Block();
			}
			if (Input.GetButtonUp("LBButton" + playerNumber) || Input.GetButtonUp("RBButton" + playerNumber))
			{
				Unblock();
			}
			//if (Input.GetButtonDown("SelectButtonP1"))
			{
				//Score()
			}
			if (Input.GetButtonDown("StartButton" + playerNumber))
			{
				GameplayHandler.GiveControl(playerNumber);
				GameplayHandler.PauseMenu();
				
			}


			if (itemHeld != null)
			{
				if (itemTimer >= itemLength)
				{
					currentItem.GetComponent<SpriteRenderer>().sprite = defaultItem;
					itemEffect(true);
					itemHeld = null;
					itemTimer = 0;
				}
				else
				{
					itemTimer += Time.deltaTime;
				}
			}
		}
		else
		{
			if (Input.GetButtonDown("StartButton" + playerNumber))
			{
				GameplayHandler.PauseMenu();
			}
		}
	}

		

    //Take damage
	public void TakeDamage(float dmgTaken, Vector2 attackPos, bool addKnockback, float attackForce)
	{
		bool critCheck = isCrit();
		if (isDead() == false)
		{
			if (isBlocking == true)
			{
				dmgTaken *= 0.5f;
			}
			if (critCheck == true)
			{
				dmgTaken *= critMultiplier;
			}
			GameObject dmgNumberClone = Instantiate(dmgNumber);
			dmgNumberClone.GetComponent<DmgNumbers>().textToShow(dmgTaken.ToString(), critCheck);
			dmgNumberClone.transform.position = new Vector3(transform.position.x, transform.position.y + (GetComponent<BoxCollider2D>().size.y * 2), dmgNumberClone.transform.position.z);
			AdjustHealth(-dmgTaken);


			if (addKnockback == true)
			{
				StartCoroutine(Knockback2(attackForce, attackPos));
			}

		}

	}

    //Critical hits
	private bool isCrit()
	{
		bool crit = false;

		int rndSystem = Random.Range(1, 100);

		for (int i = 1; i <= critChance; i++)
		{
			if (rndSystem == i)
			{
				crit = true;
			}
		}
		return crit;
	}

    //Check if dead
	private bool isDead()
	{
		if (CurrentHealth <= 0)
		{
			GameplayHandler.activePlayers.Remove(gameObject);
			Destroy(gameObject);
			return true;
		}
		else
		{
			return false;
		}
	}

    //Function for movement
	public void PlayerMove()
	{
        xVel = Input.GetAxis("Horizontal" + playerNumber) * MoveSpeed;
        rb.velocity = new Vector2(xVel, rb.velocity.y);

		yVel = 1 * JumpPower;



        animator.SetBool("moving", true);
		if (xVel == 0)
        {
            animator.SetBool("moving", false);
        }
        if (facingRight == false && xVel > 0)
		{
			Flip();
		}
		else if (facingRight == true && xVel < 0)
		{
			Flip();
		}

		if (isKnockbacked == false)
		{

			if (Input.GetAxis("Horizontal" + playerNumber) != 0)
			{
				rb.AddForce(new Vector2(xVel, 0));
			}

            //Jumping
            if (isGrounded == true)
            {
                extraJumps = extraJumpValue;
            }
            if (Input.GetButtonDown("AButton" + playerNumber) && extraJumps > 0 || Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
            {
                rb.velocity = Vector2.up * JumpPower;
                extraJumps--;
            }
            else if (Input.GetButtonDown("AButton" + playerNumber) && extraJumps == 0 && isGrounded == true || Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
            {
                rb.velocity = Vector2.up * JumpPower;
            }

        }
	}

	void Flip()
	{
		facingRight = !facingRight;
		transform.Rotate(0f, 180f, 0f);

	}

    //Knockback
	public IEnumerator Knockback2(float force, Vector2 attackPos)
	{
		isKnockbacked = true;
		float tempKnockModX = knockbackModX;
		float tempKnockModY = knockbackModY;

		if (attackPos.x > transform.position.x)
		{
			tempKnockModX = -knockbackModX;
		}
		if (attackPos.y > transform.position.y && isGrounded == false)
		{
			tempKnockModY = -knockbackModY;

		}

		float healthBonus = (1 - (CurrentHealth / MaxHealth)) * 10;
		healthBonus = (healthBonus * healthBonus) / 10;

		tempKnockModX += healthBonus + force;
		tempKnockModX *= 2;
		tempKnockModY += healthBonus + force;

		rb.AddForce(new Vector2(tempKnockModX, tempKnockModY), ForceMode2D.Impulse);
		yield return new WaitForSeconds(0.5f);
		isKnockbacked = false;
	}

    //Blocking
	protected void Block()
	{
		isBlocking = true;
	}
	protected void Unblock()
	{
		isBlocking = false;
	}

    //Melee attacks
	public virtual void MeleeAttack()
	{

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(meleePos.position, meleeRange, whatIsEnemies);
		for (int i = 0; i < enemiesToDamage.Length; i++)
		{
			if (enemiesToDamage[i].gameObject != gameObject)
			{
				enemiesToDamage[i].gameObject.GetComponent<PlayerController>().TakeDamage(AttackPower, (Vector2)transform.position, true, LightAttackForce);
			}

		}

    }

	//Used to visualise melee range
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(meleePos.position, meleeRange);
	}

    //Ranged attacks
	public virtual void RangedAttack()
	{
        Instantiate(projectile, firePoint.position, firePoint.rotation);
    }

	public virtual void Special()
	{
        animator.SetBool("ultAttacked", true);
        UltCharge = 0;
		UltAura.Stop();
	}

	public void ChargeUlt()
	{
		UltCharge = Mathf.Clamp(UltCharge + GetUltChargeRate(), 0, UltChargeMax);
        ultBar.GetComponent<HealthBar>().SetSize(UltCharge / UltChargeMax);
	}

	public float GetUltChargeRate()
	{
		float ultChargeRateTemp = UltChargeRate;

		ultChargeRateTemp += 2 * (1 - (CurrentHealth / MaxHealth));
		return ultChargeRateTemp;
	}


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == 9)
		{
			isGrounded = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.layer == 9)
		{
			isGrounded = false;
		}
	}

	public void itemAcquired(GameObject item)
	{
		if (itemHeld != null)
		{
			itemEffect(true);
			itemTimer = 0;
		}
		itemHeld = item.tag;
        currentItem.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
		itemEffect(false);
		Destroy(item);
		
	}

	private void itemEffect(bool undo)
	{
		switch (itemHeld)
		{
			case "Ares":
				AresPotion(undo);
				break;
			case "Dionysus":
				DionysusPotion();
				break;
			case "Hypnos":
				HypnosPotion(undo);
				break;
			case "Tyche":
				TychePotion(undo);
				break;
			case "Aphrodite":
				AphroditePotion(undo);
				break;
			case "BabyBottle":
				BabyBottle(undo);
				break;
		}
	}

	private void AresPotion(bool undo)
	{
		if (undo)
		{
			AttackPower /= 1.5f;
		}
		else
		{
			AttackPower *= 1.5f;
		}

	}
	private void DionysusPotion()
	{
		MoveSpeed = -MoveSpeed;
	}
	private void HypnosPotion(bool undo)
	{
		if (undo)
		{
			MoveSpeed /= 0.5f;
		}
		else
		{
			MoveSpeed *= 0.5f;
		}


	}
	private void TychePotion(bool undo)
	{
		if (undo)
		{
			critChance /= 2;
		}
		else
		{
			critChance *= 2;
		}

	}
	private void AphroditePotion(bool undo)
	{
		if (!undo)
		{
			AdjustHealth(30f);
		}
		
		
	}
	private void BabyBottle(bool undo)
	{
		if (!undo)
		{
			UltCharge = UltChargeMax;
			ultBar.GetComponent<HealthBar>().SetSize(UltCharge / UltChargeMax);
		}
		
	}

	private void AdjustHealthBar()
	{
		healthBar.GetComponent<HealthBar>().SetSize(CurrentHealth / MaxHealth);
	}

	public void AdjustHealth(float changeInHealth)
	{
		CurrentHealth = Mathf.Clamp(CurrentHealth + changeInHealth, 0, MaxHealth);
		AdjustHealthBar();
	}


	







	//   IEnumerator StartUltCam()
	//{
	//	StartCoroutine(UltCam2(gameObject.transform.position, true));
	//	yield return new WaitForSeconds(2f);
	//	StartCoroutine(UltCam2(camDefPos, false));
	//}

	//IEnumerator UltCam2(Vector2 endPos, bool ZoomIn)
	//{
	//	float changeInX = endPos.x - cam.transform.position.x;
	//	float changeInY = endPos.y - cam.transform.position.y;

	//	while ((Vector2)cam.transform.position != endPos)
	//	{
	//		cam.transform.position = new Vector3(cam.transform.position.x + (changeInX / 30), cam.transform.position.y + (changeInY / 30), cam.transform.position.z);

	//		if (ZoomIn == true)
	//		{
	//			if (cam.orthographicSize > 2.5)
	//			{
	//				cam.orthographicSize -= 0.1f;
	//			}
	//		}
	//		else if (ZoomIn == false)
	//		{
	//			if (cam.orthographicSize < camDefZoom)
	//			{
	//				cam.orthographicSize += 0.1f;
	//			}
	//		}
	//		yield return new WaitForSeconds(0.01f);
	//	}

	//}


	//IEnumerator Knockback(GameObject attack)
	//{
	//	isKnockbacked = true;
	//	knockbackModX = attack.GetComponent<Rigidbody2D>().mass * attack.GetComponent<Rigidbody2D>().velocity.x;
	//	knockbackModY = attack.GetComponent<Rigidbody2D>().mass * attack.GetComponent<Rigidbody2D>().velocity.y;

	//	rb.AddForce(new Vector2(knockbackModX, knockbackModY));

	//	yield return new WaitForSeconds(3f);
	//	isKnockbacked = false;
	//}
}





