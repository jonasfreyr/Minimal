using System.Collections;
using UnityEngine;

public class bone : MonoBehaviour
{
    private AudioSource _collectSound;

    private void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Dog")) return;
        GameManager.instance.IncrementScore(5);
        _collectSound.Play();

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        
        StartCoroutine(DestroyBone());

    }

    IEnumerator DestroyBone()
    {

        yield return new WaitForSeconds(1);
        
        Destroy(gameObject);
    }
    
    private void Start()
    {
        _collectSound = GetComponent<AudioSource>();
    }
}
