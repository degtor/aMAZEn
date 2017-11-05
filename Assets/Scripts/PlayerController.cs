using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;

	private int Count;
	private Rigidbody rb;

	void Start ()
	{
		Count = 0;
		SetCountText ();
		winText.text = "";

		rb = GetComponent<Rigidbody> ();	
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce(movement*speed);
	}
		
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("PickUp"))
		{
			other.gameObject.SetActive (false);
			Count++;
			SetCountText ();
		}
	}

	void SetCountText () {
		countText.text = "Count: " + Count.ToString();

		if (Count >= 11)
		{
			winText.text = "You Win!";
		}
	}
}
