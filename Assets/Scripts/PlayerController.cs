using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

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

	// Variables that can be set on the Player UI Properties
	public bool speedBoost;
	public bool jumpBoost;
	public float gameTimer;
	public float timer;
	public Text displayTime;
	public Text youHaveSpeed;
	public Text youHaveJump;
	public Text countText;
	public Text healthText;
	public Text gameStatusText;
	public Text score;
	public GameObject gameOverPanel;
	public GameObject escapePanel;
	public GameObject enemyObject;
	public AudioClip enemyClip;
	public AudioClip bombClip;
	public Rigidbody rb;

	// Internal Variables
	private AudioSource source;
	private Color playerColor;
	private bool gameIsOver;
	private int health = 100;
	private int Count;
	private int speed;
	private int jump;

	// Defining the number of coins to be collected
	static int quantityCoins = 10;

	void Start ()
	{
		// Resetting health on game start or restart
		health = 100;
		healthText.text = "Health: " + health.ToString ();

		// Getting the player preferences
		difficulty = PlayerPrefs.GetInt ("difficulty");
		player = PlayerPrefs.GetInt ("player");

		// Setting game according to Player preferences
		setDifficulty(difficulty);
		setPlayer(player);

		// Resetting coin counts
		Count = 0;
		SetCountText();

		// Resetting game variables
		speedBoost = false;
		gameIsOver = false;
		youHaveJump.text = "";
		youHaveSpeed.text = "";
		displayTime.text = "";
		speed = speedPlayerOne;

		// Setting up player control
		rb = GetComponent<Rigidbody> ();	
		playerColor = GetComponent<Renderer>().material.color;

	}

	// Frame Update method appropriade for Rigid Body
	void FixedUpdate () 
	{
		// Setting up axis movement
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// Updating the time only if game is still running
		if (gameIsOver == false) {
			SetGameTime ();
		}

		// Setting up jump and movement
		Vector3 jumpAction = new Vector3 (0.0f, jump, 0.0f);
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);

		// Extra Jump
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

		// escape function
		else if (Input.GetKeyDown ("escape")) {
			if (!escapePanel.activeSelf) {
				escapePanel.SetActive (true);
			} else {
				escapePanel.SetActive (false);
			}
		}

	}
		
	// Runs after the scene has been processed
	void LateUpdate() {
		// Checking if player has fallen outside of the maze or has lost its health
		if ((rb.transform.position.y <= 0 && gameIsOver == false) || (health <= 0)) {
			GameOver ("lost");
		} else if (Count >= quantityCoins) {
			GameOver ("won");
		}
	}

	void GameOver(string gameStatus){

		// Changing this global boolean
		gameIsOver = true;

		// Storing game ending time
		timer = gameTimer;

		// Checking if the game has been won of lost
		if (gameStatus == "won") {
			gameStatusText.text = "You won!\nYou completed the maze in " + timer.ToString() + "s";
			score.text = "Your score: " + timer.ToString();
		} else {
			gameStatusText.text = "You lost!";
		}

		// Showing Game Over Panel
		gameOverPanel.SetActive (true);
	}

	// Detecting collisions between player and game objects
	void OnTriggerEnter(Collider other) 
	{
		// Detecting collisions with coins
		if (other.gameObject.CompareTag ("pointPickUp"))
		{
			Count++;
			SetCountText ();
			other.gameObject.SetActive(false);

		}
		// Detecting collisions with special jump pill
		else if (other.gameObject.CompareTag("jumpPickUp"))
		{
			jump = 400;
			GetComponent<Renderer> ().material.color = Color.yellow;
			youHaveJump.text = "Press SPACE for temporary Super Jump!";
			Invoke("youHaveJumpText", 3);
			Invoke("endJumpBooster", 10f);
			other.gameObject.SetActive(false);

		} 
		// Detecting collisions with special speed pill
		else if (other.gameObject.CompareTag("speedPickUp")) 
		{
			speedBoost = true;
			GetComponent<Renderer> ().material.color = Color.red;
			youHaveSpeed.text = "Press S for temporary Super Speed!";
			Invoke ("youHaveSpeedText", 3);
			Invoke ("endSpeedBooster", 5f);
			other.gameObject.SetActive (false);
		}
		// Detecting collisions with a bomb trap that is available in difficult mode
		else if (other.gameObject.CompareTag("bombTrap")) 
		{
			source = other.gameObject.GetComponent<AudioSource>();
			source.PlayOneShot(bombClip, 1F);
			rb.AddForce (new Vector3 (0.0f, 500, 0.0f));
		}
		// Detecting collisions with the enemy available in difficult mode
		else if (other.gameObject.CompareTag("enemy")) 
		{
			// Since the enemy can reduce health, we only detect collision until game over
			if (!gameIsOver) {
				// Playing collision sound
				source = other.gameObject.GetComponent<AudioSource>();
				source.PlayOneShot(enemyClip, 1F);
				// Reducing player speed upon collision
				rb.velocity = Vector3.zero;
				// Decreasing health during contact
				health--;
				SetHealth ();
			}
		}

	}

	// Shows the number of coins that are left to be picked-up
	void SetCountText () {
		countText.text = "Remaining \nCoins: " + (quantityCoins - Count).ToString();
	}

	// Updating the health display
	void SetHealth () {
		healthText.text =  "Health: " + health.ToString();
	}

	// Updating the time display
	void SetGameTime () {
		gameTimer = Time.timeSinceLevelLoad;	
		displayTime.text = "Time: " + gameTimer.ToString();
	}

	// Restituting normal player color and speed
	void endSpeedBooster () {
		rb.GetComponent<Renderer> ().material.color = playerColor;
		speed = 10;
	}

	// Restituting normal player color and jump-ability
	void endJumpBooster () {
		speedBoost = false;
		rb.GetComponent<Renderer> ().material.color = playerColor;
		jump = 200;
	}

	// Setting up the game according user preference for difficulty
	void setDifficulty(int difficulty) {
		// Difficulty 1 = Hard mode
		if (difficulty == 1) {
			// Running to all bomb trap elements to activate them
			GameObject[] goArray = GameObject.FindGameObjectsWithTag("bombTrap");
			if (goArray.Length > 0) {
				for (int i = 0; i < goArray.Length; i++) {
					// Activating element that has Mesh Renderer and Box Collider deactivated via UI
					goArray[i].GetComponent<MeshRenderer>().enabled = true;
					goArray[i].GetComponent<BoxCollider> ().enabled = true;
				}
			}
			// Activating the maze enemy
			enemyObject.SetActive (true);
		}
	}

	// Setting up the player character according to user preferences
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

	// Setting up feedback text
	void youHaveJumpText() {
		youHaveJump.text = "";
	}

	// Setting up feedback text
	void youHaveSpeedText() {
		youHaveSpeed.text = "";
	}
}
