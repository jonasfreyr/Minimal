using System;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    private bool isSensing;
    private float _time = 0;
    
    public bool Sense()
    {
        return isSensing;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Ground") && Time.time > _time)
        {
            isSensing = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Ground") && Time.time > _time)
        {
            isSensing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Ground") && Time.time > _time)
        {
            isSensing = false;
        }
    }

    public void Disable(float time)
    {
        isSensing = false;
        _time = Time.time + time;
    }
}
