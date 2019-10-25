using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour {

	[SerializeField]
	private EventSystem eventSystem;

	[SerializeField]
	private GameObject optionsStart;

	[SerializeField]
	private GameObject menuStart;

	[SerializeField]
	private GameObject controlsStart;

	[SerializeField]
	private AudioClip clip;

	private void Start()
	{
		GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().ChangeClip(clip);
	}

	public void PlayGame() {
		SceneManager.LoadScene("CharacterSelect");
	}

	public void QuitGame() {
		Application.Quit();
	}

	public void SetOptions()
	{
		eventSystem.SetSelectedGameObject(optionsStart);
	}

	public void SetMenu()
	{
		eventSystem.SetSelectedGameObject(menuStart);
	}

	public void SetControls()
	{
		eventSystem.SetSelectedGameObject(controlsStart);
	}
}
