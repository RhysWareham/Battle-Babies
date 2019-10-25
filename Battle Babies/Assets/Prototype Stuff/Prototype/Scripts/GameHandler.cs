using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    [SerializeField]
    private HealthBar healthBar;


	// Use this for initialization
	void Start () {
        healthBar.SetSize(.4f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
