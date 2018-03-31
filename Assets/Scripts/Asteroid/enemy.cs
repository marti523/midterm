using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject[] ufo;
    public static int spawnCount;
    public static float spawnRate;

    void Start()
    {
        spawnCount = 1;
        spawnRate = 1.0f;
        StartCoroutine(Spawn());
       
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                Vector3 randomPosition = new Vector3(6, 0, Random.Range(-7.0f, 7.0f));
                Instantiate(ufo[Random.Range(0, ufo.Length)], randomPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }
}