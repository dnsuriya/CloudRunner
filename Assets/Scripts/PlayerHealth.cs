using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int m_health = 3;
    public bool m_isDead;
    private bool m_death = false;
    public int m_deathPlane = -10;
    public float m_deathTime = 3;

    // Use this for initialization
    void Start ()
    {
        m_isDead = false;
        GetComponent<Collider2D>().enabled = true;
    }
	
	// Update is called once per frame
	void Update ()
    {

        if(m_isDead == true)
        {
            Death();
        }

        // Check Position - ifs its below the death line restart line
        //if (gameObject.transform.position.y < m_deathPlane)
        //{
        //    Death();
        //}
    }

	IEnumerator DeathRoutine()
    {
        if (m_death == false)
        {
            m_death = true;
            // Player Has Died
            Debug.Log("Player Has Died");
            yield return new WaitForSeconds(m_deathTime);
            Debug.Log("Restarting Level");
            // Restart the active scene

            Restart();
        }

		yield return null;
	}

    public void DamagePlayer(int _damage)
    {
        if(m_isDead == false)
        {
            m_health -= _damage;

            if (m_health <= 0)
            {
                m_isDead = true;
            }
        }
       
    }

    public void KillPlayer()
    {
        m_health = 0;
        m_isDead = true;
    }

    private void Death()
    {
        if (m_death == false)
        {
           
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 500);
            GetComponent<Rigidbody2D>().gravityScale = 1;

            StartCoroutine("DeathRoutine");

        }

        
    }

    private void Restart()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
