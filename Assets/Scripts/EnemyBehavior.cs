using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _diedAt;
    private GameManagerSystem _gameManagerSystem;
    private bool _hasShotYet;
    private float _nextShootTime;

    public GameObject Bullet;
    public GameObject ExplosionAnimation;
    public AudioClip ExplosionSound;
    public float FireRate = 5f;
    public AudioClip ShootSound;

    public bool IsAlive { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        _gameManagerSystem = FindObjectOfType<GameManagerSystem>();
        _audioSource = GetComponent<AudioSource>();
        IsAlive = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_gameManagerSystem.State == GameManagerSystem.GameState.GamePlay)
        {
            if (IsAlive)
            {
                if (!_hasShotYet)
                {
                    _nextShootTime = Time.time + Random.Range(0.1f, 3.0f);
                    _hasShotYet = true;
                }

                if (Time.time > _nextShootTime)
                {
                    _nextShootTime = Time.time + FireRate;
                    Instantiate(Bullet, transform.position + Vector3.down, Quaternion.identity);
                    _audioSource.pitch = Random.Range(1.8f, 3.0f);
                    _audioSource.PlayOneShot(ShootSound);
                }
            }
            else
            {
                if (Time.time > _diedAt + 1)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Bullet(Clone)" && IsAlive)
        {
            IsAlive = false;
            _diedAt = Time.time;

            Destroy(other.gameObject);
            _audioSource.PlayOneShot(ExplosionSound);
            Instantiate(ExplosionAnimation, transform.position, Quaternion.identity);
            GetComponent<Renderer>().enabled = false;
        }
    }
}