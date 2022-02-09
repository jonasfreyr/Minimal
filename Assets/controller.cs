using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public float jumpForce;
    private Rigidbody2D _body;

    private Sensor _sensor;

    private bool inAir;
    public Animator _legAnimator;
    public Animator _duckAnimator;
    public Animator _tailAnimatori;

    public BoxCollider2D boxCollider;
    public Transform areaMover;
    
    private AudioSource _barkingSound;
    
    private void OnCollisionEnter2D (Collision2D other)
    {
        if (boxCollider.IsTouching(other.collider))
        {
            transform.parent = areaMover;
        }
    }

    
    private void OnDestroy()
    {
        GameManager.instance.GameOver();
    }

    private void Start()
    {
        _barkingSound = GetComponent<AudioSource>();

        _body = GetComponent<Rigidbody2D>();

        var child = transform.Find("GroundSensor");

        _sensor = child.GetComponent<Sensor>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_sensor.Sense() && inAir)
        {
            _legAnimator.SetTrigger("Landed");
            inAir = false;
        }

        if (!inAir && _sensor.Sense() && Input.GetMouseButton(0))
        {

            inAir = true;
            
            _barkingSound.Play();
            
            _sensor.Disable(.1f);
            
            _legAnimator.SetTrigger("Jump");
            
            _body.AddForce(new Vector2(0, jumpForce));
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            _duckAnimator.SetTrigger("Duck");
            _tailAnimatori.SetTrigger("Duck");
        }

        else if (Input.GetMouseButtonUp(1))
        {
            _duckAnimator.SetTrigger("Unduck");
            _tailAnimatori.SetTrigger("Unduck");
        }
    }
}
