using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacters : MonoBehaviour {

	[SerializeField]
	private GameObject spawnPoint1;

	[SerializeField]
	private GameObject spawnPoint2;

	[SerializeField]
	private GameObject spawnPoint3;

	[SerializeField]
	private GameObject spawnPoint4;

	private GameObject[] players;
	private GameObject[] spawnPoints = new GameObject[4];

	[SerializeField]
	private GameObject goArtemis;
	[SerializeField]
	private GameObject goZeus;
	[SerializeField]
	private GameObject goPoseidon;
	[SerializeField]
	private GameObject goHades;

    [SerializeField]
    private GameObject[] uiPlayers;


    void Start () {
		players = new GameObject[StoreChoices.players.Length];
		spawnPoints[0] = spawnPoint1;
		spawnPoints[1] = spawnPoint2;
		spawnPoints[2] = spawnPoint3;
		spawnPoints[3] = spawnPoint4;

		GameplayHandler.activePlayers.Clear();
		SpawnCharacters();
		Physics2D.autoSimulation = true;

	}
	

	void SpawnCharacters() {
		for (int i = 0; i < players.Length; i++)
		{
			//players[i] = new GameObject("player" + (i + 1));

			//players[i] = new GameObject("Player" + (i+1));
			//players[i].AddComponent<SpriteRenderer>();
			//players[i].GetComponent<SpriteRenderer>().sprite = StoreChoices.players[i];
			//players[i].transform.position = spawnPoints[i].transform.position;
			//players[i].AddComponent<BoxCollider2D>();
			//players[i].AddComponent<Rigidbody2D>().gravityScale = 1;
			//players[i].GetComponent<Rigidbody2D>().mass = 0.4f;
			//players[i].GetComponent<Rigidbody2D>().freezeRotation = true;
			//players[i].tag = StoreChoices.playersTag[i];
			//players[i].AddComponent<AssignCharacter>();


			switch (StoreChoices.playersTag[i])
			{
				case "Artemis":
					players[i] = Instantiate(goArtemis);
					break;

				case "Zeus":
					players[i] = Instantiate(goZeus);
					break;

				case "Poseidon":
					players[i] = Instantiate(goPoseidon);
					break;

				case "Hades":
					players[i] = Instantiate(goHades);
					break;


			}
			players[i].transform.position = spawnPoints[i].transform.position;
			players[i].name = "Player" + (i + 1);
			players[i].GetComponent<PlayerController>().playerNumber = "P" + (i + 1);
			players[i].GetComponent<PlayerController>().healthBar = uiPlayers[i].transform.Find("HealthBar").gameObject;
			players[i].GetComponent<PlayerController>().ultBar = uiPlayers[i].transform.Find("UltBar").gameObject;
            players[i].GetComponent<PlayerController>().currentItem = uiPlayers[i].transform.Find("CurrentItem").gameObject;
			players[i].GetComponent<PlayerController>().defaultItem = uiPlayers[i].transform.Find("CurrentItem").gameObject.GetComponent<SpriteRenderer>().sprite;
			uiPlayers[i].transform.Find("PlayerIcon").gameObject.GetComponent<SpriteRenderer>().sprite = players[i].GetComponent<SpriteRenderer>().sprite;
		}

        for (int i = players.Length; i < uiPlayers.Length; i++)
        {
            Destroy(uiPlayers[i]);
        }
	}
}
