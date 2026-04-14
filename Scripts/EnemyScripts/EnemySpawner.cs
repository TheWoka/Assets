using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private DrillController drill;
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnDistanceBelow = 20f;
    [SerializeField] private float spawnXRange = 4f;

    [Header("Difficulty")]
    [SerializeField] private int baseEnemiesPerSpawn = 1;
    [SerializeField] private int extraEnemyEveryFloors = 2;
    [SerializeField] private int maxEnemiesPerSpawn = 4;

    private float timer;
    private int currentFloor = 0;
    private DrillHealth drillHealth;

    void Start()
    {
        drillHealth = drill.GetComponent<DrillHealth>();
        drill.OnReachedStop += OnFloorReached;
    }

    void OnDestroy()
    {
        drill.OnReachedStop -= OnFloorReached;
    }

    void Update()
    {
        if (drill == null || enemyPrefab == null) return;

        if (drillHealth != null && drillHealth.IsDead)
        {
            timer = 0f;
            return;
        }

        if (!drill.IsMoving)
        {
            timer = 0f;
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        int enemiesPerSpawn = Mathf.Min(
            maxEnemiesPerSpawn,
            baseEnemiesPerSpawn + currentFloor / extraEnemyEveryFloors
        );

        for (int i = 0; i < enemiesPerSpawn; i++)
        {
            Vector3 drillPos = drill.transform.position;

            Vector3 spawnPos = new Vector3(
                drillPos.x + Random.Range(-spawnXRange, spawnXRange),
                drillPos.y - spawnDistanceBelow,
                0f
            );

            GameObject enemyObj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            EnemyCrawler enemy = enemyObj.GetComponent<EnemyCrawler>();

            if (enemy != null)
            {
                enemy.SetTarget(drill.transform);
                enemy.SetVerticalSpeed(drill.CurrentSpeed + 1.3f);
                Debug.Log("Цель врагу назначена: " + drill.transform.name);
            }
            else
            {
                Debug.LogError("На враге нет компонента EnemyCrawler!", enemyObj);
            }
        }
    }

    void OnFloorReached()
{
    currentFloor++;

    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    foreach (GameObject enemy in enemies)
    {
        Destroy(enemy);
    }

    timer = 0f;
}
}