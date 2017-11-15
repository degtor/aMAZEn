using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Player characteristic default values
	private int speed;
	private int jump;

	static int number_of_coins = 1;

	// Variables related to player preferences
	private int player;
	private int difficulty;

	// Set-up of player 1 (Speedy Ballzales) characteristics
	static Color colorPlayerOne = Color.blue;
	static int speedPlayerOne = 10;
	static int jumpPlayerOne = 90;

	// Set-up of player 2 (Ballsy Jumper) characteristics
	static Color colorPlayerTwo = Color.red;
	static int speedPlayerTwo = 5;
	static int jumpPlayerTwo = 180;

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
	public Text healthText;

	public AudioClip bombClip;
	private AudioSource source;

	// **
	private bool gameIsOver;
	public Text gameStatusText;
	public Text score;
	public GameObject gameOverPanel;
	public AudioClip enemyClip;
	public int health = 100;

	void Start ()
	{
		health = 100;
		// Getting the player preferences
		difficulty = PlayerPrefs.GetInt ("difficulty");
		player = PlayerPrefs.GetInt ("player");

		// Setting game according to Player preferences
		setDifficulty(difficulty);
		setPlayer(player);

		displayTime.text = "";
		Count = 0;
		SetCountText();
		healthText.text = "Health: " + health.ToString ();

		speedBoost = false;

		youHaveJump.text = "";
		youHaveSpeed.text = "";
		speed = speedPlayerOne;

		rb = GetComponent<Rigidbody> ();	
		playerColor = GetComponent<Renderer>().material.color;

		//source = GetComponent<AudioSource>();
		// **
		gameIsOver = false;

	}

	void FixedUpdate () 
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// **
		if (gameIsOver == false) {
			SetGameTime ();
		}

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

		// stop function
		else if (Input.GetKeyDown ("b")) {
			rb.velocity = Vector3.zero;
		}

	}

	// **

	// Runs after the scene has been processed
	void LateUpdate() {
		// Checking if player has fallen outside of the maze or has lost its health
		if ((rb.transform.position.y <= 0 && gameIsOver == false) || (health <= 0)) {
			GameOver ("lost");
		} else if (Count >= number_of_coins) {
			GameOver ("won");
		}
	}

	// Runs once 
	void Awake() {

	}

	void GameOver(string gameStatus){
		gameIsOver = true;
		timer = gameTimer;

		// Checking if the game has been won of lost
		if (gameStatus == "won") {
			gameStatusText.text = "You won!\nYou completed the maze in " + timer.ToString() + "s";
			score.text = "Your score: " + timer.ToString();
		} else {
			gameStatusText.text = "You lost!";
		}

		gameOverPanel.SetActive (true);
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

		else if (other.gameObject.CompareTag("enemy")) 
		{
			if (!gameIsOver) {
				source = other.gameObject.GetComponent<AudioSource>();
				source.PlayOneShot(enemyClip, 1F);
				rb.velocity = Vector3.zero;
				health--;
				SetHealth ();
			}
		}

	}

	void SetCountText () {
		//Shows the number of coins that are left to be picked-up
		countText.text = "Remaining \nCoins: " + (number_of_coins - Count).ToString();
	}

	void SetHealth () {
		healthText.text =  "Health: " + health.ToString();

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
		gameTimer = Time.timeSinceLevelLoad;	
		displayTime.text = "Time: " + gameTimer.ToString();
	}

	void setDifficulty(int difficulty) {
		if (difficulty == 1) {
			GameObject[] goArray = GameObject.FindGameObjectsWithTag("bombTrap");

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
