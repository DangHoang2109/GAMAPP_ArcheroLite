using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Chịu trách nhiệm sinh Enemy
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public Enemy _enemy;

    public float _thoiGianSinhEnemy = 30;//second
    public float _timerSinhEnemy = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Sinh ra 5 enemy
        for (int i = 0; i < 5; i++)
        {
            SpawnEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _timerSinhEnemy += Time.deltaTime;
        if(_timerSinhEnemy >= _thoiGianSinhEnemy)
        {
            _timerSinhEnemy = 0f;
            SpawnEnemy();
        }
    }
    /// <summary>
    /// Sinh ra một Enemy tại vị trí ngẫu nhiên
    /// </summary>
    void SpawnEnemy()
    {
        //ngẫu nhiên vị trí sinh ra trong không gian Camera
        //API ngẫu nhiên: Random.Range(minRandom, maxRandom);
        Vector3 enemyPosition = new Vector3(
            x: Random.Range(-9f, 9f),
            y: Random.Range(-4f, 4f));

        Instantiate(_enemy, enemyPosition, Quaternion.identity);
    }
}
