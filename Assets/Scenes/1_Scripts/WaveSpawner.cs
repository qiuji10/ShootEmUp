using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState {SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    [SerializeField]
    private float waveCounddown;
    public float waveIntervalTime = 5f;
    private float searchCountdown = 1f;
    private int nextWave = 0;

    public Wave[] waves;
    public Transform[] spawnPoint;

    public SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        waveCounddown = waveIntervalTime;
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
                return;
        }

        if (waveCounddown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpwanWave(waves[nextWave]));
            }
        }
        else
            waveCounddown -= Time.deltaTime;
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCounddown = waveIntervalTime;

        //looping wave, can modify what you want
        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
        }
        else
            nextWave++;
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
                return false;   
        }
        return true;
    }

    IEnumerator SpwanWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Transform _sp = spawnPoint[Random.Range(0, spawnPoint.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
