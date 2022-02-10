using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpedOver : MonoBehaviour
{
    private bool _jumpedOver = false;
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Dog") && !_jumpedOver)
        {
            GameManager.instance.IncrementScore(1);
            _jumpedOver = true;
        }
    }
}
