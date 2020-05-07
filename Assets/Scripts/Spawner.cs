using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> Enemies;
    public GameObject Enemy;
    public int MaxEnemies = 1;

    private void Start() => Enemies = new List<GameObject>(MaxEnemies);

    public void SpawnWave(int waveNumber)
    {
        for (var i = 0; i < MaxEnemies; i++)
        {
            var position = new Vector2(transform.position.x + i * 2, transform.position.y);
            Enemies.Add(Instantiate(Enemy, position, Quaternion.identity));
        }
    }
}