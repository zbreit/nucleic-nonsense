using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class EnvironmentGenerator : MonoBehaviour
{
    [SerializeField]
    private Vector2 dimensions;

    [SerializeField]
    private uint maxFoodCount;

    [SerializeField]
    private float foodSpawnRateSeconds;

    [SerializeField]
    private GameObject wallPrefab;

    [SerializeField]
    private GameObject foodPrefab;

    public HashSet<Food> spawnedFood;


    // Start is called before the first frame update
    void Start()
    {
        BuildWalls();
        StartCoroutine(SpawnFoodCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
    }

    void BuildWalls()
    {
        float wallSize = wallPrefab.transform.localScale.x;

        // Build left and right walls (they will be extra tall!)
        BuildWall(Vector3.left * (dimensions.x + wallSize) / 2, new Vector2(wallSize, dimensions.y + 2 * wallSize));
        BuildWall(Vector3.right * (dimensions.x + wallSize) / 2, new Vector2(wallSize, dimensions.y + 2 * wallSize));

        // Build top and bottom walls (they will be short)
        BuildWall(Vector3.up * (dimensions.y + wallSize) / 2, new Vector2(dimensions.x, wallSize));
        BuildWall(Vector3.down * (dimensions.y + wallSize) / 2, new Vector2(dimensions.x, wallSize));
    }

    void BuildWall(Vector3 centerPosition, Vector2 dimensions)
    {
        GameObject wall = Instantiate(wallPrefab, centerPosition, Quaternion.identity);

        wall.transform.localScale = new Vector3(dimensions.x, dimensions.y, 1);
    }


    IEnumerator SpawnFoodCoroutine()
    {
        while(true)
        {
            // Wait until next coroutine call before spawning a new food
            if(spawnedFood.Count >= maxFoodCount)
            {
                yield return null;
                continue;
            }

            SpawnFood();
            yield return new WaitForSeconds(foodSpawnRateSeconds);
        }
    }

    void SpawnFood()
    {
        float x = Random.Range(-1f, 1f) * (dimensions.x - foodPrefab.transform.localScale.x) / 2;
        float y = Random.Range(-1f, 1f) * (dimensions.y - foodPrefab.transform.localScale.y) / 2;
        float z = 0;
        Vector3 spawnLocation = new Vector3(x, y, z) + transform.position;
        GameObject foodObject = Instantiate(foodPrefab, spawnLocation, Quaternion.identity);
        Food food = foodObject.GetComponent<Food>();

        // Decrement food count on death
        spawnedFood.Add(food);
        food.OnEaten += () => spawnedFood.Remove(food);
    }
}
