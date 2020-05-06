using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private AudioSource _audioSource;

    private GameManagerSystem _gameManagerSystem;
    private int _lives;
    private float _screenWidth;
    private float _startY;

    public GameObject Bullet;
    public Text LivesText;
    public int MaxLives = 3;
    public AudioClip ShootSound;
    public float Speed = 5;
    public AudioClip TakeDamageSound;

    public bool IsAlive { get; private set; }

    public void Start()
    {
        _gameManagerSystem = FindObjectOfType<GameManagerSystem>();

        IsAlive = true;
        _lives = MaxLives;
        LivesText.text = "Lives: " + _lives;

        _audioSource = GetComponent<AudioSource>();
        _screenWidth = Mathf.Abs(Camera.main.ViewportToWorldPoint(Vector3.zero).x) - GetComponent<SpriteRenderer>().size.x / 2;
        _startY = transform.position.y;
    }

    public void Update()
    {
        if (_gameManagerSystem.State == GameManagerSystem.GameState.GamePlay)
        {
            if (IsAlive)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Instantiate(Bullet, transform.position, Quaternion.identity);
                    _audioSource.PlayOneShot(ShootSound);
                }

                var velocity = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
                velocity = Mathf.Clamp(transform.position.x + velocity, -_screenWidth, _screenWidth);
                var position = new Vector3(velocity, _startY);
                transform.position = position;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "EnemyBullet(Clone)")
        {
            Destroy(other.gameObject);
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        _audioSource.PlayOneShot(TakeDamageSound);
        _lives -= 1;
        if (_lives <= 0)
        {
            _lives = 0;
            Destroy(gameObject);
            IsAlive = false;
        }

        LivesText.text = "Lives: " + _lives;
    }
}