using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 4.0f;
    private Player _player;
    private Animator _animator;
    private static readonly int OnEnemyDeath = Animator.StringToHash("OnEnemyDeath");
    [SerializeField] private AudioSource explosionSound;
    [SerializeField] private GameObject laserPrefab;
    
    private float _fireRate = 3.0f;
    private float _canFire = -1;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) Debug.LogError("The Player is Null!");
        _animator = GetComponent<Animator>();
        if (_animator == null) Debug.LogError("The Animator is Null!");
    }

    private void Update()
    {
        FireLaser();
       CalculateMovement();
    }
    
    private void FireLaser()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            Instantiate(laserPrefab, transform.position + new Vector3(-1.50f, 0, 0), Quaternion.identity);

        }
       
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.left * (speed * Time.deltaTime));

        if (!(transform.position.x <= -9.3f)) return;
        var randomY = Random.Range(-4.53f, 5.42f);
        transform.position = new Vector3(9.5f, randomY, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.Damage();
            _animator.SetTrigger(OnEnemyDeath);
            speed = 0;
            Destroy(gameObject, 0.418f);
            explosionSound.enabled = true;
            explosionSound.Play();
        }
        else if (other.CompareTag("Laser"))
        {
            _player.AddScore(10);
            Destroy(other.gameObject);
            speed = 0;
            _animator.SetTrigger(OnEnemyDeath);
            
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject,  0.55f);
            
            explosionSound.enabled = true;
            explosionSound.Play();
        }
    }
}