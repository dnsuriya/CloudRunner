using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityMove : MonoBehaviour
{

    public float m_Speed = 10;
    public int m_xMoveDirection = 1;

    // Use this for initialization
    //void Start() {

    //}

    protected RaycastHit2D GetFirstRaycastHit(Vector2 direction, int index)
    {
        RaycastHit2D[] hits = new RaycastHit2D[3];
        Physics2D.RaycastNonAlloc(transform.position, direction, hits);
        return hits[index];
    }

    // Update is called once per frame
    //void Update()
    //{
    //    RaycastHit2D hit = GetFirstRaycastHit(new Vector2(m_xMoveDirection, 0));// Physics2D.Raycast(transform.position, new Vector2(m_xMoveDirection, 0));
    //    if(hit.distance <= m_hitDistance)
    //    {
    //        if (hit.collider.tag == "Obstical")
    //        {
    //            ChangeDirection();
    //        }
    //    }

    //    Move();
    //}

    protected void Move()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(m_xMoveDirection, 0) * m_Speed;
    }

    protected void ChangeDirection()
    {
        m_xMoveDirection *= -1;

        // FLip the player to face the new direction
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

    }
}
