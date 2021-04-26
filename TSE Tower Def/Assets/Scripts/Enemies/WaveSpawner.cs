using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // Empty enemy item, more can be added for special enemies?
    public Transform[] enemyList;
    //stores the waves
    public WaveScriptableObject[] waves;

    float timeForWave = 5f, countdown = 2f;

    private int waveNumber = 0;

    void Update()
    {
        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeForWave;
        }
        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        //messy system until i can work out how to assign the number to spawn under the enemy, but it works for now
        for (int i = 0; i < waves[waveNumber].enemies.Length; i++)
        {
            for (int j = 0; j < waves[waveNumber].numToSpawn[i]; j++)
            {
                // Create a single enemy and assign values
                GameObject spawned = Instantiate(enemyList[0].gameObject, transform.position, transform.rotation);
                // Lots of public variables, should be made into GET:SET for oop
                spawned.GetComponent<EnemyScript>().assignStats(waves[i].enemies[i].speed, waves[i].enemies[i].health, waves[i].enemies[i].sprite);
                // Pause before next enemy, necessary to stop too muhv overlap
                yield return new WaitForSeconds(.3f);
            }
        }


        if (waveNumber < waves.Length - 1)
            waveNumber++;
        else
        //End of wave
        { }
    }
}
