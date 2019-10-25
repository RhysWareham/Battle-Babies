using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CursorMove : MonoBehaviour {


	[SerializeField]
	private string Horizontal;

	[SerializeField]
	private string Vertical;

	[SerializeField]
	private string AButton;

	[SerializeField]
	private string BButton;

	[SerializeField]
	private GameObject Panel;

	[SerializeField]
	private GameObject txtReady;

	private Sprite defaultSprite;
	private float alphaVal;
	private Vector2 startPos;

	// Use this for initialization
	void Start () {
		defaultSprite = Panel.GetComponent<Image>().sprite;
		alphaVal = 0.01f;
		startPos = gameObject.transform.localPosition;
	}

	// Update is called once per frame
	void Update()
	{
		if (gameObject.activeSelf == true)
		{
			RaycastCursor();
		}
			
	}

	private void OnDisable()
	{
		transform.localPosition = startPos;
		txtReady.SetActive(false);
	}




	void AButtonPressed()
	{
		gameObject.tag = "Selected";
		txtReady.SetActive(true);
		
	}
	void BButtonPressed()
	{
		gameObject.tag = "Selecting";
		txtReady.SetActive(false);
	}

	void MoveCursor()
	{
		float xPos = Input.GetAxis(Horizontal);
		xPos *= 6.0f;

		float yPos = Input.GetAxis(Vertical);
		yPos *= 6.0f;
		
		transform.localPosition = new Vector3(transform.localPosition.x + xPos, transform.localPosition.y + yPos, transform.localPosition.z);
	}

	void RaycastCursor()
	{
		Vector3 cursorPos = transform.position;
		Vector2 cursorPos2D = new Vector2(cursorPos.x, cursorPos.y);

		if (txtReady.activeSelf == false)
		{
			MoveCursor();
			RaycastHit2D hit = Physics2D.Raycast(cursorPos2D, Vector2.zero);
			if (hit.collider != null)
			{
				Panel.GetComponent<Image>().sprite = hit.collider.gameObject.GetComponent<Image>().sprite;
				Panel.tag = hit.collider.gameObject.tag;

				if (Input.GetButtonDown(AButton))
				{
					AButtonPressed();
				}
			}
			else
			{
				Panel.GetComponent<Image>().sprite = defaultSprite;
			}
		}
		else if (txtReady.activeSelf == true)
		{

			txtReady.GetComponent<TextMeshProUGUI>().alpha -= alphaVal;

			if (txtReady.GetComponent<TextMeshProUGUI>().alpha < 0.3f)
			{
				alphaVal = -alphaVal; ;
			}
			if (txtReady.GetComponent<TextMeshProUGUI>().alpha == 1f)
			{
				alphaVal = -alphaVal;
			}


			if (Input.GetButtonDown(BButton))
			{
				BButtonPressed();
			}
		}
	}
}
