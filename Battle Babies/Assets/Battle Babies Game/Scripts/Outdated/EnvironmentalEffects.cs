using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalEffects : MonoBehaviour {

	[SerializeField]
	private ParticleSystem pSystem;
	[SerializeField]
	private GameObject clouds;

	private bool ready = false;
	private bool isRunning = true;

	private void Start()
	{
		PositionClouds();

	}
	IEnumerator MoveClouds()
	{

			while (clouds.transform.position.x < 0)
			{
				clouds.transform.position += new Vector3(0.2f, 0, 0);
				yield return new WaitForSeconds(0.001f);
				//ready = true;
			}
			StartCoroutine(ChangeColor());
			//if (ready == true)
			//{
			//	StartCoroutine(ChangeColor());
			//	ready = false;
			//}

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine(MoveClouds());
		}




			if (isRunning == false)
		{
			StartLightning();
		}

	}

	void StartLightning()
	{
		if (pSystem.isStopped)
		{
			var emission = pSystem.emission;
			emission.rateOverTime = 6f;
			emission.enabled = true;
			pSystem.Play();
		}
	}

	IEnumerator ChangeColor()
	{
		isRunning = true;
		for (float i = 1f; i > 0.15f; i -= 0.02f)
		{
			for (int j = 0; j < clouds.transform.childCount; j++)
			{

				Color newColor = clouds.transform.GetChild(j).GetComponent<SpriteRenderer>().color;

				newColor.r = i;
				newColor.g = i;
				newColor.b = i;
				clouds.transform.GetChild(j).GetComponent<SpriteRenderer>().color = newColor;

				yield return new WaitForSeconds(.001f);
			}
		}
		isRunning = false;
	}



	void PositionClouds() {
		Vector3 topLeft = new Vector3(0f, 1f, 0f);
		clouds.transform.position = Camera.main.ViewportToWorldPoint(topLeft);
		clouds.transform.position = new Vector3(clouds.transform.position.x - clouds.GetComponent<RectTransform>().rect.width / 2,
			clouds.transform.position.y - clouds.GetComponent<RectTransform>().rect.height / 5,
			0f);
		clouds.SetActive(true);
	}
}
