using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public bool speedBoost;
	public bool jumpBoost;
	public Text countText;
	public Text winText;
	public float timer;
	public float jumpValue;

	private int Count;
	private Rigidbody rb;
	private Color playerColor;

	void Start ()
	{
		Count = 0;
		jumpValue = 200.0f;
		SetCountText ();
		winText.text = "";

		rb = GetComponent<Rigidbody> ();	
		playerColor = GetComponent<Renderer> ().material.color;
	}

	void FixedUpdate () 
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 jump = new Vector3 (0.0f, jumpValue, 0.0f);
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
	
		rb.AddForce(movement*speed);

		// Jumper
		if (Input.GetKeyDown("space") && rb.transform.position.y <= 0.5f) {
			rb.AddForce (jump);
		} 
		// Speed booster
		else if (Input.GetKeyDown("s")) {
			if (speed == 50.0) {
				speed = 10;
			} else {
				speed = 50;
			}
		}
			
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("pointPickUp"))
		{
			Count++;
			SetCountText ();
			//other.gameObject.SetActive(false);

		} 
		else if (other.gameObject.CompareTag("jumpPickUp"))
		{
			jumpValue = 400.0f;
			GetComponent<Renderer> ().material.color = Color.yellow;
			Invoke("endJumpBooster", 10f);
			other.gameObject.SetActive(false);

		} 
		else if (other.gameObject.CompareTag("speedPickUp")) 
		{
			speed = 50;
			GetComponent<Renderer> ().material.color = Color.red;
			Invoke ("endSpeedBooster", 5f);
			other.gameObject.SetActive (false);
		}

	}

	void SetCountText () {
		countText.text = "Count: " + Count.ToString();

		if (Count >= 11)
		{
			winText.text = "You Win!";
		}
	}

	void endSpeedBooster () {
		rb.GetComponent<Renderer> ().material.color = playerColor;
		speed = 10;
	}

	void endJumpBooster () {
		rb.GetComponent<Renderer> ().material.color = playerColor;
		jumpValue = 200.0f;
	}
}
