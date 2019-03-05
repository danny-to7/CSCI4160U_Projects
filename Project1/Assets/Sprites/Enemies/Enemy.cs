using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float speed = -1f;
    [SerializeField] public float range = 3f;
    [SerializeField] public int hp = 3;
    [SerializeField] private float knockbackX = 1000f;
    [SerializeField] private float knockbackY = 500f;
    public Transform origin;
    Vector2 dir = new Vector2(-1,0);
    Rigidbody2D myBody;
    Transform myTransform;
    float myWidth;
    
    void Start()
    {
        myTransform = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        //reverse patrol direction when meets an edge
        RaycastHit2D cast = Physics2D.Raycast(origin.position, dir, range);
        if (cast != true)
        {
            speed *= -1;
            dir *= -1;
            Flip();
        }

        myBody.velocity = new Vector2(speed, myBody.velocity.y);
    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //knockback on attack receive
    void Knockback(int dir)
    {
        if (dir == 1)
        {
            Debug.Log("left");
            myBody.AddForce(new Vector3(-knockbackX, knockbackY, 0f));
        } else if (dir == 2)
        {
            Debug.Log("right");
            myBody.AddForce(new Vector3(knockbackX, knockbackY, 0f));
        }
        hp--;
         if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
