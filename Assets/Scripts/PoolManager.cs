using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {
    // could inherit from MonoSingleton but I dont feel like implementing that class and dealing with its issues

    [SerializeField] private int _defaultPoolSize = 10;
    [SerializeField] private List<GameObject> _enemyPrefabs; // can make this a list to randomly choose either range or melee AI
    private List<GameObject> _enemyPool = new List<GameObject>();

    // Start is called before the first frame update
    void Start() {
        GenerateEnemies(_defaultPoolSize);
    }

    // Update is called once per frame
    void Update() {

    }

    // called at start to fill the pool
    List<GameObject> GenerateEnemies(int amount) {
        for (int i = 0; i < amount; i++) {
            int r = Random.Range(0, _enemyPrefabs.Count);
            GameObject enemy = Instantiate(_enemyPrefabs[r]);
            enemy.SetActive(false);

            _enemyPool.Add(enemy);
        }

        return _enemyPool;
    }

    // called to spawn a new enemy from the pool, makes pool bigger if pool is empty
    public GameObject RequestEnemy() {
        foreach (GameObject enemy in _enemyPool) {
            if(enemy.activeInHierarchy == false) {
                enemy.SetActive(true);
                return enemy;
            }
        }

        int r = Random.Range(0, _enemyPrefabs.Count);
        GameObject newEnemy = Instantiate(_enemyPrefabs[r]);
        _enemyPool.Add(newEnemy);

        return newEnemy;
    }

    // called to add enemy back to pool
    public void DespawnEnemy(GameObject enemy) {
        enemy.SetActive(false);
    }
}
