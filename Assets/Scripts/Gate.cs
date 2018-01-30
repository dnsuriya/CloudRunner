using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour {

    public bool m_start;
    private GameObject m_player;

	// Use this for initialization
	void Start ()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");

        if (m_start)
        {
            m_player.transform.position = this.transform.position;
        }
		
        
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(m_start == false)
        {
            if (other == m_player.GetComponent<CapsuleCollider2D>())
            {
                // Reached end Gate
                int index = SceneManager.GetActiveScene().buildIndex;
                int sceneCount = SceneManager.sceneCountInBuildSettings;
                index++;
                if (index >= sceneCount)
                {
                    // End of game
                    SceneManager.LoadScene(0);
                }
                else
                {
                    // Load the Next Stage
                    SceneManager.LoadScene(index);
                }
            }
            
        }
    }



}
