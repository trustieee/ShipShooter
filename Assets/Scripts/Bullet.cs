using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _screenHeight;

    public float Speed;

    // Start is called before the first frame update
    private void Start() => _screenHeight = Camera.main.ViewportToWorldPoint(Vector3.one).y;

    private void FixedUpdate()
    {
        if (transform.position.y > _screenHeight)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void Update() => transform.position += Vector3.up * Time.deltaTime * Speed;
}