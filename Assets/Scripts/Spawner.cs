using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Enemy;
    public int MaxEnemies = 1;

    // Start is called before the first frame update
    private void Start()
    {
        for (var i = 0; i < MaxEnemies; i++)
        {
            var position = new Vector2(transform.position.x + i * 2, transform.position.y);
            Instantiate(Enemy, position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    private void Update() { }
}