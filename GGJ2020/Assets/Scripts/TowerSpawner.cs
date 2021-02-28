using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> towers;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine(towers));
    }

    private IEnumerator SpawnRoutine(List<GameObject> towers)
    {
        while (true)
        {
            GameObject [] spawnedTowers = GameObject.FindGameObjectsWithTag("PlayerTowers");

            if (spawnedTowers.Length == 0)
            {
                break;
            }

            int chosenIndex = Random.Range(0, spawnedTowers.Length - 1);
            int chosenTower = Random.Range(0, towers.Count);
            spawnedTowers[chosenIndex].GetComponentInChildren<TowerHealthManager>().SpawnTower(towers[chosenTower]);

            yield return new WaitForSeconds(10.0f);
        }
    }
}
