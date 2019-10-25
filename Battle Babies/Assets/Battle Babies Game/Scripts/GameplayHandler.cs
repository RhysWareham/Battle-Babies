using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameplayHandler : MonoBehaviour
{
	[SerializeField]
	private AudioClip clip;


	[SerializeField]
	private EventSystem eventSystem;

	[SerializeField]
	protected Camera cam;

	[SerializeField]
	private GameObject[] potions;

	[SerializeField]
	private GameObject portal;

	private GameObject portal1;
	private GameObject portal2;

	[SerializeField]
	private GameObject floor;

	[SerializeField]
	private GameObject ultPotion;

	private int spawnChance = 15;
	private float timeDelay = 1f;
	private float timer;

	private float ultPotionDelay = 25;
    private float ultPotionTimer;

	[SerializeField]
	private GameObject pauseScreen;

	private static EventSystem EventSystem;

	private static GameObject PauseScreen;

    public static List<GameObject> activePlayers = new List<GameObject>();
    private List<GameObject> playersNotInView = new List<GameObject>();
    //private List<GameObject> activeItems = new List<GameObject>();


    private Vector3 camDefPos;
	private float camDefZoom;
	private float maxZoomLevel = 11;


    [SerializeField]
    private GameObject winStatement;


	// Use this for initialization
	void Start()
	{
		ultPotionTimer = ultPotionDelay;
		//portal.GetComponentInChildren<ParticleSystem>().Play();
		camDefPos = cam.transform.position;
		camDefZoom = cam.orthographicSize;
		PauseScreen = pauseScreen;
		EventSystem = eventSystem;

		GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().ChangeClip(clip);


	}

	// Update is called once per frame
	void Update()
	{

		if (PauseScreen.activeSelf == false)
		{
			if (timer <= 0)
			{
				if (SpawnCheck(spawnChance))
				{
					int itemNum = Random.Range(0, potions.Length);
					GameObject item = potions[itemNum];
					SpawnItem(item);
				}
				timer = timeDelay;
			}
			else
			{
				timer -= Time.deltaTime;
			}


			if (ultPotionTimer <= 0)
			{
				SpawnItem(ultPotion);
				ultPotionTimer = ultPotionDelay;
			}
			else
			{
				ultPotionTimer -= Time.deltaTime;
			}



			//foreach(GameObject item in activeItems)
			//{
			//    Rigidbody2D rb = item.GetComponent<Rigidbody2D>();


			//    Debug.Log(item.name);

			//    checkOfScreen(rb);
			//}




			if (activePlayers.Count > 0)
			{
				foreach (GameObject player in activePlayers)
			    {
				    Rigidbody2D rb = player.GetComponent<Rigidbody2D>();


				    if (rb.transform.position.x < -11.0f || rb.transform.position.x > 11.0f ||
				    rb.transform.position.y < -4.5f || rb.transform.position.y > 4.5f)
				    {
					    if (playersNotInView.Contains(player) == false)
					    {
						    playersNotInView.Add(player);
					    }


				    }
				    else
				    {
					    if (playersNotInView.Contains(player) == true)
					    {
						    playersNotInView.Remove(player);
					    }
				    }

				    if (playersNotInView.Count >= 1)
				    {
					    if (cam.orthographicSize < maxZoomLevel)
					    {
						    cam.orthographicSize += 0.1f;
					    }
				    }
				    else
				    {
					    if (cam.orthographicSize > camDefZoom)
					    {
						    cam.orthographicSize -= 0.1f;
					    }

				    }

				    if (activePlayers.Count == 1)
				    {
					    winStatement.SetActive(true);

                    }

				    checkDead(rb);
			    }
            }
			

		}

	}

	private bool SpawnCheck(int spawnChance)
	{
		bool spawn = false;
		int rndSystem = Random.Range(1, 100);

		for (int i = 1; i <= spawnChance; i++)
		{
			if (rndSystem == i)
			{
				spawn = true;
			}
		}
		return spawn;
	}

	private void SpawnItem(GameObject item)
	{
        
		Instantiate(item);
		float rndX = Random.Range(floor.transform.position.x - (floor.GetComponent<BoxCollider2D>().size.x), floor.transform.position.x + (floor.GetComponent<BoxCollider2D>().size.x));
		item.transform.position = new Vector3(rndX, 5, item.transform.position.z);
        //activeItems.Add(item);
	}

    //private void checkOfScreen(Rigidbody2D rb)
    //{
    //    if (rb.transform.position.x < -28.0f || rb.transform.position.x > 28.0f ||
    //        rb.transform.position.y < -20.0f || rb.transform.position.y > 20.0f)
    //    {
    //        activeItems.Remove(rb.gameObject);
    //        Destroy(rb.gameObject);
    //    }
    //}
    
    /// Does not work - Will could not find what was wrong

   
    private void checkDead(Rigidbody2D rb)
    {
        if (rb.transform.position.x < -28.0f || rb.transform.position.x > 28.0f ||
            rb.transform.position.y < -20.0f || rb.transform.position.y > 20.0f)
        {
            activePlayers.Remove(rb.gameObject);
            playersNotInView.Remove(rb.gameObject);
			rb.gameObject.GetComponent<PlayerController>().AdjustHealth(-1000);
            Destroy(rb.gameObject);

        }

    }

    
	public static void PauseMenu()
	{
		PauseScreen.SetActive(!PauseScreen.activeSelf);
		Physics2D.autoSimulation = !Physics2D.autoSimulation;
	}

	public static bool IsPaused()
	{
		bool isPaused = false;

		if (PauseScreen.activeSelf == true)
		{
			isPaused = true;
		}

		return isPaused;
	}

	public static void GiveControl(string playerNum)
	{
		EventSystem.SetSelectedGameObject(PauseScreen.transform.Find("MainMenu").gameObject);
		EventSystem.GetComponent<StandaloneInputModule>().horizontalAxis = "DPadHorizontal" + playerNum;
		EventSystem.GetComponent<StandaloneInputModule>().verticalAxis = "DPadVertical" + playerNum;
		EventSystem.GetComponent<StandaloneInputModule>().submitButton = "AButton" + playerNum;
		EventSystem.GetComponent<StandaloneInputModule>().cancelButton = "BButton" + playerNum;
	}


}


//if (portal.activeInHierarchy == false)
			//{
			//	if (SpawnCheck(80))
			//	{
			//		portal1 = Instantiate(portal);
			//		float rndX = Random.Range(floor.transform.position.x - (floor.GetComponent<BoxCollider2D>().size.x), floor.transform.position.x + (floor.GetComponent<BoxCollider2D>().size.x));
			//		portal1.transform.position = new Vector3(rndX, 3, portal1.transform.position.z);

			//		portal2 = Instantiate(portal);
			//		if (portal1.transform.position.x > 0)
			//		{
			//			portal2.transform.position = new Vector3(0 - portal1.transform.position.x, 3, portal1.transform.position.z);
			//		}
			//		else
			//		{
			//			portal2.transform.position = new Vector3(0 - portal1.transform.position.x, 3, portal1.transform.position.z);
			//		}


			//	}
			//}

			//if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
			//{
			//	win.SetActive(true);
			//}