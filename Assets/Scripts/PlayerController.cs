using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Player characteristic default values
	private int speed;
	private int jump;

	static int number_of_coins = 20;

	// Variables related to player preferences
	private int player;
	private int difficulty;

	// Set-up of player 1 (Speedy Ballzales) characteristics
	static Color colorPlayerOne = Color.blue;
	static int speedPlayerOne = 20;
	static int jumpPlayerOne = 200;

	// Set-up of player 2 (Ballsy Jumper) characteristics
	static Color colorPlayerTwo = Color.red;
	static int speedPlayerTwo = 10;
	static int jumpPlayerTwo = 400;

	private int Count;
	public Rigidbody rb;
	private Color playerColor;

	public bool speedBoost;
	public bool jumpBoost;
	public float gameTimer;
	public float timer;

	public Text displayTime;
	public Text youHaveSpeed;
	public Text youHaveJump;
	public Text countText;
	public Text winText;

	public AudioClip bombClip;
	private AudioSource source;

	void Start ()
	{
		// Getting the player preferences
		difficulty = PlayerPrefs.GetInt ("difficulty");
		player = PlayerPrefs.GetInt ("player");

		// Setting game according to Player preferences
		setDifficulty(difficulty);
		setPlayer(player);

		displayTime.text = "";
		Count = 0;
		SetCountText();
		winText.text = "";

		speedBoost = false;

		youHaveJump.text = "";
		youHaveSpeed.text = "";
		speed = speedPlayerOne;

		rb = GetComponent<Rigidbody> ();	
		playerColor = GetComponent<Renderer>().material.color;

		//source = GetComponent<AudioSource>();

	}

	void FixedUpdate () 
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		SetGameTime ();

		Vector3 jumpAction = new Vector3 (0.0f, jump, 0.0f);
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
	
		rb.AddForce (movement * speed);

		// Jumper
		if (Input.GetKeyDown("space") && rb.transform.position.y <= 0.5f) {
			rb.AddForce (jumpAction);
		}

		// Speed booster
		else if (Input.GetKeyDown("s")) {
			if (speedBoost) {
				speed = 50;
			} else {
				speed = 10;
			}
		}

		// STOP function
		else if (Input.GetKeyDown ("b")) {
			rb.velocity = Vector3.zero;
		}
			
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("pointPickUp"))
		{
			Count++;
			SetCountText ();
			other.gameObject.SetActive(false);

		} 
		else if (other.gameObject.CompareTag("jumpPickUp"))
		{
			jump = 400;
			GetComponent<Renderer> ().material.color = Color.yellow;
			youHaveJump.text = "Press SPACE for temporary Super Jump!";
			Invoke("youHaveJumpText", 3);
			Invoke("endJumpBooster", 10f);
			other.gameObject.SetActive(false);

		} 
		else if (other.gameObject.CompareTag("speedPickUp")) 
		{
			speedBoost = true;
			GetComponent<Renderer> ().material.color = Color.red;
			youHaveSpeed.text = "Press S for temporary Super Speed!";
			Invoke ("youHaveSpeedText", 3);
			Invoke ("endSpeedBooster", 5f);
			other.gameObject.SetActive (false);
		}

		else if (other.gameObject.CompareTag("bombTrap")) 
		{
			source = other.gameObject.GetComponent<AudioSource>();
			source.PlayOneShot(bombClip, 1F);
			rb.AddForce (new Vector3 (0.0f, 500, 0.0f));
		}

	}

	void SetCountText () {
		//Shows the number of coins that are left to be picked-up
		countText.text = "Remaining \nCoins: " + (number_of_coins - Count).ToString();

		//Checking if all the coins have been picked up
		if (Count >= number_of_coins)
		{
			winText.text = "You Win!";
		}
	}

	void endSpeedBooster () {
		rb.GetComponent<Renderer> ().material.color = playerColor;
		speed = 10;
	}

	void endJumpBooster () {
		speedBoost = false;
		rb.GetComponent<Renderer> ().material.color = playerColor;
		jump = 200;
	}

	void SetGameTime () {
		gameTimer = Time.time;	
		displayTime.text = "Time: " + gameTimer.ToString();
	}

	void setDifficulty(int difficulty) {
		if (difficulty == 1) {
			GameObject[] goArray = GameObject.FindGameObjectsWithTag("bombTrap");
			Debug.Log(goArray.Length);

			if (goArray.Length > 0) {
				for (int i = 0; i < goArray.Length; i++) {
					goArray[i].GetComponent<MeshRenderer>().enabled = true;
					goArray[i].GetComponent<BoxCollider> ().enabled = true;
				}
			}
		}
	}

	void setPlayer(int player) {
		if (player == 0) {
			rb.GetComponent<Renderer>().material.color = colorPlayerOne;
			speed = speedPlayerOne;
			jump = jumpPlayerOne;
		} else if (player == 1) {
			rb.GetComponent<Renderer>().material.color = colorPlayerTwo;
			speed = speedPlayerTwo;
			jump = jumpPlayerTwo;
		}
	}

	void youHaveJumpText() {
		youHaveJump.text = "";
	}

	void youHaveSpeedText() {
		youHaveSpeed.text = "";
	}
}
