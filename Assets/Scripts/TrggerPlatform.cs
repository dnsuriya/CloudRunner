using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrggerPlatform : MonoBehaviour {

    private GameObject m_player;

    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == m_player.GetComponent<CapsuleCollider2D>())
        {
            if (GetComponentInParent<MovingPlatform>() != null)
            {
                GetComponentInParent<MovingPlatform>().BeginMove();
            }
            else
            {
                m_player.GetComponent<Player_Move>().PlayerJump(3850);
            }
        }
    }
}
