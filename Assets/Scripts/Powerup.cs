using UnityEngine;
using UnityEngine.Serialization;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private int powerupID; // 0 = Triple Shot; 1 = Super Speed; 2 = Shields
    [SerializeField] private AudioClip audioClip;


    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.left * (speed * Time.deltaTime));

        if (transform.position.x < -9.4f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        var player = other.transform.GetComponent<Player>();
        if (player == null) return;
       
       AudioSource.PlayClipAtPoint(audioClip, transform.position);
        
        switch (powerupID)
        {
            case 0:
                player.ActivateTripleShot();
                Destroy(gameObject);
              
                break;
            case 1:
                player.ActivateSpeedBoost();
               Destroy(gameObject);
                
                break;
            case 2:
                player.ActivateShieldBoost();
                Destroy(gameObject);
                
                break;
        }
    }
}