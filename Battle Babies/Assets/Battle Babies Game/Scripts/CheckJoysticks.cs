using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckJoysticks : MonoBehaviour {

	[SerializeField]
	private GameObject cursor1;

	[SerializeField]
	private GameObject cursor2;

	[SerializeField]
	private GameObject cursor3;

	[SerializeField]
	private GameObject cursor4;

	[SerializeField]
	private GameObject txtJoin1;

	[SerializeField]
	private GameObject txtJoin2;

	[SerializeField]
	private GameObject txtJoin3;

	[SerializeField]
	private GameObject txtJoin4;


	private GameObject[] cursorArray = new GameObject[4];
	private GameObject[] txtArray = new GameObject[4];
	private float alphaVal;

	private void Start()
	{
		cursorArray[0] = cursor1;
		cursorArray[1] = cursor2;
		cursorArray[2] = cursor3;
		cursorArray[3] = cursor4;

		txtArray[0] = txtJoin1;
		txtArray[1] = txtJoin2;
		txtArray[2] = txtJoin3;
		txtArray[3] = txtJoin4;

		alphaVal = 0.01f;
	}

	// Update is called once per frame
	void Update () {



		string[] connectedControllers = Input.GetJoystickNames();

		if (connectedControllers.Length > 0)
		{
			for (int i = 1; i < connectedControllers.Length; i++)
			{
				if (cursorArray[i].activeSelf == false)
				{
					txtArray[i].SetActive(true);

					txtArray[i].GetComponent<TextMeshProUGUI>().alpha -= alphaVal;

					if (txtArray[i].GetComponent<TextMeshProUGUI>().alpha < 0.3f)
					{
						alphaVal = -alphaVal; ;
					}
					if (txtArray[i].GetComponent<TextMeshProUGUI>().alpha == 1f)
					{
						alphaVal = -alphaVal;
					}
				}
				
				if (string.IsNullOrEmpty(connectedControllers[i]))
				{
					//Debug.Log("Controller: " + i + " is disconnected");
					cursorArray[i].SetActive(false);
					cursorArray[i].tag = "Untagged";
					txtArray[i].SetActive(false);
				}
				else
				{
					//Debug.Log("Controller " + i + " is connected using: " + connectedControllers[i]);

					int controllerNum = i + 1;

					if (Input.GetButtonDown("AButtonP" + controllerNum) && cursorArray[i].activeSelf == false)
					{

						cursorArray[i].SetActive(true);
						cursorArray[i].tag = "Selecting";
						txtArray[i].SetActive(false);
					}
				}
			}
		}

		



	}
}
