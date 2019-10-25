using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public Transform healthBar;
    public Slider healthFill;

    public float currentHealth;

    public float maxHealth;

    public float healthBarYOffset = 2;

    #region Monobehaviour API

	
	// Update is called once per frame
	void Update () {
        PositionHealthBar();
	}

    public void ChangeHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //Percentage for slider
        healthFill.value = currentHealth / maxHealth;
    }

    private void PositionHealthBar()
    {
        Vector3 currentPos = transform.position;

        healthBar.position = new Vector3(currentPos.x, currentPos.y + healthBarYOffset, currentPos.z);
        
    }

    private void CheckDead()
    {
        //Make a function to check if player is dead, and destroy sprite if so.
        //if (currentHealth <= 0)
        //{
        //    Destroy()
        //}
    }
    #endregion
}
