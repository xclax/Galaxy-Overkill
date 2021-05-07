using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject tripleShotPrefab;
    [SerializeField] private float fireRate = 0.15f;
    [SerializeField] private int lives = 3;
    [SerializeField] private bool isTripleShotActive;
    [SerializeField] private bool isSpeedBoostActive;
    [SerializeField] private bool isShieldBoostActive;
    [SerializeField] private GameObject shieldVisualizer;
   [SerializeField] private int score;
    [SerializeField] private AudioSource laserSound;
    [SerializeField] private AudioSource explosionSound;

    private static readonly int OnPlayerHit = Animator.StringToHash("OnPlayerHit");
    
    private float _canFire = -1.0f;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private Animator _animator;


    // Start is called before the first frame update
    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null) Debug.LogError("The Spawn Manager is Null!");
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        transform.position = new Vector3(-9, -0.14f, 0);
        _animator = GetComponent<Animator>();
        if (_animator == null) Debug.LogError("The Animator is Null!");
    }

    // Update is called once per frame
    private void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) FireLaser();
    }

    private void FireLaser()
    {
        _canFire = Time.time + fireRate;


        if (isTripleShotActive)
            Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
        else
            Instantiate(laserPrefab, transform.position + new Vector3(1.50f, 0, 0), Quaternion.identity);

        laserSound.enabled = true;
        laserSound.Play();
    }

    private void CalculateMovement()

    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var direction = new Vector3(horizontalInput, verticalInput, 0);

        if (isSpeedBoostActive)
            speed = 10.0f;

        transform.Translate(direction * (speed * Time.deltaTime));

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9.3f, 9.3f),
            Mathf.Clamp(transform.position.y, -4.53f, 6.0f), 0);

        if (transform.position.x > 11.3f)
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        else if (transform.position.x < -11.3f) transform.position = new Vector3(11.3f, transform.position.y, 0);
    }

    public void Damage()
    {
        if (!isShieldBoostActive)
        {
            lives--;
            _uiManager.UpdateLives(lives);
        }


        if (lives == 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
            _uiManager.CheckForBestScore(score);
            explosionSound.enabled = true;
            explosionSound.Play();
        }
    }

    public void ActivateTripleShot()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }


    private IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isTripleShotActive = false;
    }

    public void ActivateSpeedBoost()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }


    private IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
        speed = 5.0f;
    }

    public void ActivateShieldBoost()
    {
        isShieldBoostActive = true;
        shieldVisualizer.SetActive(true);
        StartCoroutine(ShieldBoostPowerDownRoutine());
    }


    private IEnumerator ShieldBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isShieldBoostActive = false;
        shieldVisualizer.SetActive(false);
    }

    public void AddScore(int points)
    {
        score += points;
        _uiManager.UpdateScore(score);
    }

    public void StartHitAnimation()
    {
        _animator.SetTrigger(OnPlayerHit);
    }
}