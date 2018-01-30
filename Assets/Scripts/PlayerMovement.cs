	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float m_playerSpeed = 10;
 	public float m_playerJumpPower = 500;
    public bool m_facingRight = true;
    public bool m_isJump;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		PlayerMove ();

		if (Input.GetButtonDown ("Jump")) {
			PlayerJump ();
		}
	}

	private void PlayerMove()
	{
        // COntrols
        // Animations
        // Player Direction
        // Physics
    

		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Input.GetAxis ("Horizontal") * m_playerSpeed, gameObject.GetComponent<Rigidbody2D> ().velocity.y);
	}

	private void PlayerJump()
	{
        if(m_isJump == false)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * m_playerJumpPower);
            m_isJump = true;
        }
		
	}
}
