using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreChoices : MonoBehaviour {

	[SerializeField]
	private GameObject pnlPlayer1;
	[SerializeField]
	private GameObject pnlPlayer2;
	[SerializeField]
	private GameObject pnlPlayer3;
	[SerializeField]
	private GameObject pnlPlayer4;

	public static Sprite player1;
	public static Sprite player2;
	public static Sprite player3;
	public static Sprite player4;

	public static Sprite[] players;
	public static string[] playersTag;

	private GameObject[] panels = new GameObject[4];

	private void OnEnable()
	{
		panels[0] = pnlPlayer1;
		panels[1] = pnlPlayer2;
		panels[2] = pnlPlayer3;
		panels[3] = pnlPlayer4;

		GameObject[] activePlayers = GameObject.FindGameObjectsWithTag("Selected");
		players = new Sprite[activePlayers.Length];
		playersTag = new string[activePlayers.Length];


		for (int i = 0; i < players.Length; i++)
		{
			players[i] = panels[i].GetComponent<Image>().sprite;
			playersTag[i] = panels[i].tag;
		}
	}
}
