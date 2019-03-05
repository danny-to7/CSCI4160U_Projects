using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] public int damage = 1;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.isTrigger != true && collider.CompareTag("Enemy"))
        {
            Debug.Log("Hit");

            //Get enemy position relative to attack to determine knockback direction
            Enemy enemy = collider.GetComponent<Enemy>();
            float enemyX = enemy.transform.position.x;
            float attackX = gameObject.transform.position.x;

            if (enemyX < attackX)
            {
                collider.SendMessageUpwards("Knockback", 1);
            } else if (enemyX > attackX)
            {
                collider.SendMessageUpwards("Knockback", 2);

            }

        }
    }
}
