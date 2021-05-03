using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float speed = 4.0f;
    [SerializeField] private GameObject explosionPrefab;
    private SpawnManager _spawnManager;
    [SerializeField] private AudioSource explosionSound;
    private Player _player;
    private Animator _animator;
    private static readonly int OnEnemyDeath = Animator.StringToHash("OnEnemyDeath");


    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) Debug.LogError("The Player is Null!");
        _animator = GetComponent<Animator>();
        if (_animator == null) Debug.LogError("The Animator is Null!");
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    private void Update()
    {
            transform.Translate(Vector3.left * (Time.deltaTime * speed));
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

        if (other.CompareTag("Laser"))
        {
            _player.AddScore(20);
            Destroy(other.gameObject);
            _animator.SetTrigger(OnEnemyDeath);
            speed = 0;
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, 0.418f);
            explosionSound.enabled = true;
            explosionSound.Play();
        }
    }
}