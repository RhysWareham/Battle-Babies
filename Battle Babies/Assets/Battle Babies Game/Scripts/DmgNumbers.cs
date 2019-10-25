using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgNumbers : MonoBehaviour {

	[SerializeField]
	private TextMesh textMesh;

	private float timeDelay = 0.15f;
	private float timer;

	// Use this for initialization
	void Start () {
		timer = timeDelay;
	}
	
	// Update is called once per frame
	void Update () {

		

		if (textMesh.color.a > 0.1)
		{

			if (timer >= 0)
			{
				textMesh.color = Color.Lerp(textMesh.color, Color.clear, 0.02f);
				gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
				//Debug.Log(textMesh.color.a);
				timer = timeDelay;
			}
			else
			{
				timer -= Time.deltaTime;
			}

			
		}
		else
		{
			Destroy(gameObject);
		}
		
	}

	public void textToShow(string text, bool isCrit)
	{
		if (isCrit == true)
		{
			textMesh.color = Color.yellow;
		}
		textMesh.text = text;
	}
}
