using System;
using System.Collections;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField] private float speed = 8.0f;
    private bool _isparentNotNull;
    private Player _player;
    [SerializeField] private AudioSource hitAudio;
   


    // Update is called once per frame
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) Debug.LogError("The Player is Null!");
       
        _isparentNotNull = transform.parent != null;
    }


    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.left * (speed * Time.deltaTime));

        if (transform.position.x <= -11.47f)
        {
            if (_isparentNotNull) Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.Damage();
            _player.StartHitAnimation();
  
            Destroy(gameObject, 0.1f);
            hitAudio.enabled = true;
            hitAudio.Play();

            
        }
    }

  
}