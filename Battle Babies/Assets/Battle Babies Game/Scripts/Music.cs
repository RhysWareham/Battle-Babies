using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

	// Use this for initialization

	public AudioClip audioToPlay;
	private AudioSource audioSource;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		audioSource = GetComponent<AudioSource>();
	}

	public void Play()
	{
		audioSource.Play();
	}

	public void Stop()
	{
		audioSource.Stop();
	}

	public void ChangeClip(AudioClip clip)
	{
		audioToPlay = clip;
		if (audioSource.clip != clip)
		{
			audioSource.clip = clip;
			GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().Play();
		}
		
	}
}
