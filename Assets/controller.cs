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
    private bool justJumped;
    public Animator _legAnimator;
    public Animator _duckAnimator;
    public Animator _tailAnimatori;

    public BoxCollider2D boxCollider;
    public Transform areaMover;
    
    public AudioSource barkingSound;

    public GameObject barkingObject;
    
    private bool _hitWall;
    
    private void OnCollisionEnter2D (Collision2D other)
    {
        if (boxCollider.IsTouching(other.collider))
        {
            transform.parent = areaMover;
            _hitWall = true;
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.GameOver();
    }

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();

        var child = transform.Find("GroundSensor");

        _sensor = child.GetComponent<Sensor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_hitWall || !GameManager.instance.gameStarted) return;

        if (!_sensor.Sense()) justJumped = false;
        
        if (_sensor.Sense() && inAir && !justJumped)
        {
            _legAnimator.SetTrigger("Landed");
            inAir = false;
        }

        else if (!inAir && _sensor.Sense() && Input.GetMouseButton(0) && !justJumped)
        {
            justJumped = true;
            
            inAir = true;

            barkingObject.SetActive(true);
            
            StartCoroutine(BarkingSoundFlair());
            
            barkingSound.Play();
                
            _sensor.Disable(.1f);
            
            _legAnimator.SetTrigger("Jump");
            
            _body.velocity = Vector2.zero;
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

    IEnumerator BarkingSoundFlair()
    {
        float currentTime = 0;
        
        while (currentTime <= 0.5f)
        {
            currentTime += Time.deltaTime;
            yield return 0;
        }

        barkingObject.SetActive(false);
    }
    
}
