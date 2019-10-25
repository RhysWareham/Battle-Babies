using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMatch : MonoBehaviour {

	[SerializeField]
	private GameObject mapPanel;

	[SerializeField]
	private Button map1;

	[SerializeField]
	private Button map2;

    [SerializeField]
    private Button map3;

    [SerializeField]
    private Button map4;

    [SerializeField]
	private GameObject txtStart;


	private void Update()
	{

		if (txtStart.activeSelf == true && Input.GetButtonDown("StartButtonP1"))
		{
			LoadLevel();
		}

	}

	public void LoadLevel()
	{
		if (mapPanel.GetComponent<Image>().sprite == map1.image.sprite)
		{
			SceneManager.LoadScene("Level");
		}
		else if (mapPanel.GetComponent<Image>().sprite == map2.image.sprite)
		{
			SceneManager.LoadScene("Level 2");
		}
        else if (mapPanel.GetComponent<Image>().sprite == map3.image.sprite)
        {
            SceneManager.LoadScene("Level 3");
        }
        else if (mapPanel.GetComponent<Image>().sprite == map4.image.sprite)
        {
            SceneManager.LoadScene("Level 4");
        }
    }

}
