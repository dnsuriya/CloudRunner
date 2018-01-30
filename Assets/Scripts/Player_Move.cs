using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{

    public float m_playerSpeed = 10;
    public float m_playerJumpPower = 3000;//1250;
    private bool m_facingRight = true;
    private float m_moveX;
    public bool m_isGrounded;
    public bool m_autoRun = true;

    private bool m_startedMoving = false;
    private bool m_hasMoved = false;

    private Vector3 m_prevPos;
    private Vector3 m_currentPos;

    public float m_stuckTime = 3.0f;
    private float m_curStuckTime;


    // Use this for initialization
    void Start()
    {
        m_curStuckTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_prevPos = m_currentPos;
        if (GetComponentInParent<PlayerHealth>().m_isDead == false)
        {
            PlayerMove();
        }
        m_currentPos = transform.position;

        if(AproxEqual(m_currentPos, m_prevPos,0.001f))
        {
            m_hasMoved = false;
        }
        else
        {
            m_startedMoving = true;
            m_hasMoved = true;
        }



        CheckStuck();

    }

    private void PlayerMove()
    {

        // Controls
        if (m_autoRun == false)
        {
            m_moveX = Input.GetAxis("Horizontal");
            if ((Input.GetButtonDown("Jump")) &&
                (m_isGrounded == true))
            {
                PlayerJump();
            }
        }
        else
        {
            m_moveX = 1;
        }
       

        // Animations

        // Player Direction

        if (((m_moveX > 0.0f) && (m_facingRight == false)) ||
            ((m_moveX < 0.0f) && (m_facingRight == true)))
        {
            FlipPlayer();
        }
         
        // Physics
        
        gameObject.GetComponent<Rigidbody2D>().velocity = 
                    new Vector2(m_moveX * m_playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);


    }

    public void PlayerJump(float _jumpPower = 1000)
    {
        // Jump Code
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * _jumpPower);
        m_isGrounded = false;
    }

    private void FlipPlayer()
    {
        // FLip the player to face the new direction
        m_facingRight = !m_facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player has collided with " + collision.collider.name);

        if(collision.gameObject.tag == "Ground")
        {
            m_isGrounded = true;
        }
    }

    private void CheckStuck()
    {
        if(m_hasMoved == false)
        {
            m_curStuckTime += Time.deltaTime;
        }
        else
        {
            m_curStuckTime = 0.0f;
        }

        if(m_curStuckTime >= m_stuckTime)
        {
            // Player Is Stuck Resart level
            GetComponentInParent<PlayerHealth>().KillPlayer();
        }
        
    }

    private bool AproxEqual(Vector3 _A, Vector3 _B, float _delta)
    {
        bool result = false;   

        if( (Mathf.Abs(_A.x - _B.x) <= _delta) &&
            (Mathf.Abs(_A.y - _B.y) <= _delta) &&
            (Mathf.Abs(_A.z - _B.z) <= _delta))
        {
            result = true;
        }

        return result;
    }
}
