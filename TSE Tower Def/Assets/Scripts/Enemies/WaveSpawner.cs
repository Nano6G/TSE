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
        //Select each enemy
        for (int i = 0; i < waves[waveNumber].enemies.Length; i++)
        {
            //Spawn the current enemy 
            for (int j = 0; j < waves[waveNumber].numToSpawn[i]; j++)
            {
                // Create a single enemy and assign values
                GameObject spawned = Instantiate(enemyList[0].gameObject, transform.position, transform.rotation);
                // Lots of public variables, could be made into GET:SET for oop
                spawned.GetComponent<EnemyScript>().assignStats(waves[i].enemies[i].speed, waves[i].enemies[i].health, waves[i].enemies[i].sprite, waves[i].enemies[i].anim);
                // Pause before next enemy, necessary to prevent too much overlap
                yield return new WaitForSeconds(.3f);
            }
        }


        if (waveNumber < waves.Length - 1)
            waveNumber++;
        else
        //End of wave
        {
            Debug.Log("Wave over!");
        }
    }
}
