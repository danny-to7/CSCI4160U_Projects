using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float speed = -1f;
    [SerializeField] public float range = 3f;

    public LayerMask enemyMask;
    public Transform origin;
    Vector2 dir = new Vector2(-1,0);
    Rigidbody2D myBody;
    Transform myTransform;
    float myWidth;
    
    void Start()
    {
        myTransform = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
    }
    
    void FixedUpdate()
    {
        RaycastHit2D cast = Physics2D.Raycast(origin.position, dir, range);
        if (cast == true)
        {
            if (cast.collider.CompareTag("Ground"))
            {
            }
        } else
        {
            speed *= -1;
            dir *= -1;
            Flip();
        }

        myBody.velocity = new Vector2(speed, myBody.velocity.y);
   /*     Vector2 lineCastPos = myTransform.position - myTransform.right * myWidth;
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);

        //rotate when enemy meets edge of platform
        if (!isGrounded)
        {
            Vector3 currentRotation = myTransform.eulerAngles;
            currentRotation.y += 180;
            myTransform.eulerAngles = currentRotation;
        }

        Vector2 myVelocity = myBody.velocity;
        myVelocity.x = -myTransform.right.x * speed;
        myBody.velocity = myVelocity; */
    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
