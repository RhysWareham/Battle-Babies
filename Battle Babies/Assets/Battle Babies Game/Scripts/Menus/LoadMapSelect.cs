using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMapSelect : MonoBehaviour {

	[SerializeField]
	GameObject txtP1;

	[SerializeField]
	GameObject txtP2;

	[SerializeField]
	GameObject txtP3;

	[SerializeField]
	GameObject txtP4;

	[SerializeField]
	GameObject btnStart;

	private GameObject[] txtArray = new GameObject[4];
	private bool allReady;

	[SerializeField]
	private AudioClip clip;

	private void Start()
	{
		GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().ChangeClip(clip);

		allReady = true;

		txtArray[0] = txtP1;
		txtArray[1] = txtP2;
		txtArray[2] = txtP3;
		txtArray[3] = txtP4;
	}


	private void Update()
	{
		//bool ready = true;
		//string[] connectedControllers = Input.GetJoystickNames();
		//if (connectedControllers.Length > 0)
		//{
		//	for (int i = 0; i < connectedControllers.Length; i++)
		//	{
		//		Debug.Log(txtArray[i].activeSelf.ToString());

		//		if (txtArray[i].activeSelf == false)
		//		{
		//			ready = false;
		//		}
		//	}
		//}


		bool ready = false;
		GameObject[] cursorArray = GameObject.FindGameObjectsWithTag("Selecting");
		if (cursorArray.Length == 0)
		{
			ready = true;
		}

		if (ready == true)
		{
			allReady = true;
		}
		else
		{
			allReady = false;
		}






		//if (ready == false)
		//{
		//	allReady = false;
		//}
		//else
		//{
		//	allReady = true;
		//}

		if (allReady == true)
		{
			btnStart.SetActive(true);
		}
		else
		{
			btnStart.SetActive(false);
		}

		if (btnStart.activeSelf == true && Input.GetButtonDown("StartButtonP1"))
		{
			SceneManager.LoadScene("MapSelect");
			
		}
		
	}
		
}

