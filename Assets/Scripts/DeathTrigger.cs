using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {

    private GameObject m_player;
    public bool m_instaDeath = true;
    public int m_damage = 1;

    // Use this for initialization
    void Start ()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (m_player == null)
            return;
        
        if(other == m_player.GetComponent<CapsuleCollider2D>())
        {
            if(m_instaDeath == true)
            {
                m_player.GetComponent<PlayerHealth>().KillPlayer();
            }
            else
            {
                m_player.GetComponent<PlayerHealth>().DamagePlayer(m_damage);
            }            
        }
    }

}
