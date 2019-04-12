using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCheck : MonoBehaviour
{
    public bool dead;

    void Start()
    {
        dead = false;
    }

    void Die()
    {
        dead = true;
    }
}
