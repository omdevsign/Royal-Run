    using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float appleSpawnChance = 0.3f;
    [SerializeField] float coinSpawnChance = 0.5f;
    [SerializeField] float coinSeparationLength = 2f;
    [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f };
    LevelGenerator levelGenerator;
    ScoreManager scoreManager;
    List<int> availableLanes = new List<int> { 0, 1, 2 };
    void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }
    public void Init(LevelGenerator levelGenerator, ScoreManager scoreManager)
    {
        this.levelGenerator = levelGenerator;
        this.scoreManager = scoreManager;
    }
    void SpawnFences()
    {
        int fenceToSpawn = Random.Range(0, lanes.Length);
        for(int i = 0; i < fenceToSpawn; i++)
        {
            if (availableLanes.Count <= 0) break;
            int selectedLane = SelectLane();

            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }

    void SpawnApple()
    {
        if (Random.value > appleSpawnChance || availableLanes.Count <= 0) return;
        
        int selectedLane = SelectLane();

        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
        Apple newApple = Instantiate(applePrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Apple>();
        newApple.Init(levelGenerator);
    }
    void SpawnCoins()
    {
        if (Random.value > coinSpawnChance || availableLanes.Count <= 0) return;
        
        int selectedLane = SelectLane();

        int maxCoinsToSpawn = 6;
        int coinsToSpawn = Random.Range(1, maxCoinsToSpawn);

        float topOfChunkZPos = transform.position.z + (coinSeparationLength * 2f);

        for (int i = 0; i < coinsToSpawn; i++)
        {
            float spawnPositionZ = topOfChunkZPos - (i * coinSeparationLength);
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPositionZ);
            Coin newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Coin>();
            newCoin.Init(scoreManager);
        }
    }
    int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }
}
