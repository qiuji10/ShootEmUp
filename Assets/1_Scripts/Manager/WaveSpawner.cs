using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState {SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Enemies
    {
        public Transform enemy1;
        public int count1;
        public Transform enemy2;
        public int count2;
        public Transform enemy3;
        public int count3;
    }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Enemies enemies;
        public float rate;
    }

    [Header("Wave Settings")]
    [SerializeField]
    private float waveCounddown;
    [SerializeField]
    private int waveNum = 1;
    public float waveIntervalTime = 5f;
    private float searchCountdown = 1f;
    private int nextWave = 0;

    private int h2 = 5, h3 = 9;
    private float fr2 = 2f, fr3 = 4f, bs2 = 5f, s1 = 3f;

    [Header("UIText")]
    public Text wavesText;
    public Animator waveAnimator;

    [Header("GameObjects Initialization")]
    public GameObject[] buffPowerups;
    public Wave[] waves;
    public Transform[] spawnPoint;

    [Header("Game State")]
    public SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        waveCounddown = waveIntervalTime;
        waveAnimator = wavesText.GetComponent<Animator>();
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                waveNum++;
                WaveCompleted(waves[nextWave]);
            }
            else
                return;
        }

        if (waveCounddown <= 4)
        {
            waves[0].name = "Wave " + waveNum.ToString();
            wavesText.text = waves[0].name;
            waveAnimator.SetBool("waitingNextWave", true);
        }

        if (waveCounddown <= 0)
        {
            waveAnimator.SetBool("waitingNextWave", false);
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpwanWave(waves[nextWave]));
            }
        }
        else
            waveCounddown -= Time.deltaTime;
    }

    void WaveCompleted(Wave wave)
    {
        state = SpawnState.COUNTING;
        waveCounddown = waveIntervalTime;

        float enemyCount1 = wave.enemies.count1 * 1.2f;
        wave.enemies.count1 = (int)Mathf.Ceil(enemyCount1);
        float enemyCount2 = wave.enemies.count2 * 1.2f;
        wave.enemies.count2 = (int)Mathf.Ceil(enemyCount2);
        float enemyCount3 = wave.enemies.count3 * 1.2f;
        wave.enemies.count3 = (int)Mathf.Ceil(enemyCount3);
        wave.rate *= 1.005f;

        h2++;
        h3++;
        s1 += 0.5f;
        bs2 += 0.5f;
        fr2 -= 0.05f;
        fr3 -= 0.01f;
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
        int one = _wave.enemies.count1, two = _wave.enemies.count2, three = _wave.enemies.count3;
        int allcount = one + two + three;
        for (int i = 0; i < allcount; i++)
        {
            int r = Random.Range(1, 5);
            switch (r)
            {
                case 1:
                    if (one == 0)
                    {
                        allcount++;
                        break;
                    }
                    SpawnEnemy(_wave.enemies.enemy1);
                    yield return new WaitForSeconds(1f / _wave.rate);
                    one--;
                    continue;

                case 2:
                    if (two == 0)
                    {
                        allcount++;
                        break;
                    }
                    SpawnEnemy(_wave.enemies.enemy2);
                    yield return new WaitForSeconds(1f / _wave.rate);
                    two--;
                    continue;

                case 3:
                    if (three == 0)
                    {
                        allcount++;
                        break;
                    }
                    SpawnEnemy(_wave.enemies.enemy3);
                    yield return new WaitForSeconds(1f / _wave.rate);
                    three--;
                    continue;

                case 4:
                    int startRand = 0;
                    startRand = Random.Range(1, 4);
                    if (startRand == 1)
                    {
                        Vector2 randPos = new Vector2(Random.Range(-6, 6), Random.Range(3, -3));
                        GameObject pwPrefab = buffPowerups[Random.Range(0, buffPowerups.Length)];
                        Instantiate(pwPrefab, randPos, transform.rotation);
                        continue;
                    }
                    else
                        break;
            }
        }
        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Transform _sp = spawnPoint[Random.Range(0, spawnPoint.Length)];
        Transform enemy = Instantiate(_enemy, _sp.position, _sp.rotation);
        if (enemy.GetComponent("Enemy1") != null)
        {                           
            enemy.GetComponent<Enemy1>().Speed = s1;
        }
        if (enemy.GetComponent("Enemy2") != null)
        {
            enemy.GetComponent<Enemy2>().Health = h2;
            enemy.GetComponent<Enemy2>().FireRate = fr2;
            enemy.GetComponent<Enemy2>().enemyBullet.GetComponent<EnemyBullet>().speed = bs2;
        }
        if (enemy.GetComponent("Enemy3") != null)
        {
            enemy.GetComponent<Enemy3>().Health = h3;
            enemy.GetComponent<Enemy3>().FireRate = fr3;
        }
    }
}
