using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject parentObject;

    public GameObject[] enemies;
    public int spawnCount1 = 3;
    public int spawnCount2 = 3;

    public void Awake()
    {
        // gets children of enemies parent object

        int numChildren = parentObject.transform.childCount;
        enemies = new GameObject[numChildren];

        for (int i = 0; i < numChildren; i++)
        {
            enemies[i] = parentObject.transform.GetChild(i).gameObject;
        }
    }

    public void SpawnEnemies()
    {
        // populates new array with specific number of enemies

        GameObject[] randomEnemies = new GameObject[spawnCount1 + spawnCount2];
        List<int> usedIndexes = new List<int>();

        // room 1
        for (int i = 0; i < spawnCount1; i++)
        {
            int index1 = Random.Range(0, 5);

            // ignores duplicate randomization
            while (usedIndexes.Contains(index1))
            {
                index1 = Random.Range(0, 5);
            }

            randomEnemies[i] = enemies[index1];
            usedIndexes.Add(index1);
        }

        // room 2
        for (int i = 3; i < (spawnCount1 + spawnCount2); i++)
        {
            int index2 = Random.Range(6, enemies.Length);

            // ignores duplicate randomization
            while (usedIndexes.Contains(index2))
            {
                index2 = Random.Range(6, enemies.Length);
            }

            randomEnemies[i] = enemies[index2];
            usedIndexes.Add(index2);
        }

        // activates randomly chosen enemies
        for (int i = 0; i < randomEnemies.Length; i++)
        {
            randomEnemies[i].SetActive(true);
        }
    }

}