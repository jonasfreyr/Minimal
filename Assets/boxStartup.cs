using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxStartup : MonoBehaviour
{
    private List<GameObject> _otherBoxes;
    private Collider2D _collider;
    private SpriteRenderer _renderer;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _otherBoxes.Add(other.gameObject);
        
        Debug.Log(other);
    }


    // Start is called before the first frame update
    void Start()
    {
        _otherBoxes = new List<GameObject>();
        _collider = GetComponent<PolygonCollider2D>();

        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;

        if (_collider.bounds.Contains(worldPosition) && Input.GetMouseButtonUp(0))
        {
            
            
            foreach (var box in _otherBoxes)
            {
                SpriteRenderer renderer = box.GetComponent<SpriteRenderer>();

                renderer.color = _renderer.color;

            }
        }
    }
}
