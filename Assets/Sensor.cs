using UnityEngine;

public class Sensor : MonoBehaviour
{
    private bool _isSensing;
    private float _time;
    
    public bool Sense()
    {
        return _isSensing;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Ground") && Time.time > _time)
        {
            _isSensing = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Ground") && Time.time > _time)
        {
            _isSensing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Ground") && Time.time > _time)
        {
            _isSensing = false;
        }
    }

    public void Disable(float time)
    {
        _isSensing = false;
        _time = Time.time + time;
    }
}
