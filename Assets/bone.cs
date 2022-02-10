using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Dog")) return;
        GameManager.instance.IncrementScore(5);
            
        Destroy(gameObject);
    }
}
