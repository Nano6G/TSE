using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // Empty enemy item, more can be added for special enemies?
    public Transform[] enemyList;
    public int enemyCount;
    //stores the waves
    public WaveScriptableObject[] waves;

    float timeForWave = 10f, countdown = 2f;
    bool LevelOver, countingdown = true;
    private int waveNumber = 0;

    void Update()
    {
        if (!LevelOver)
        {
            if (countdown <= 0 && waveNumber < waves.Length)
            {
                StartCoroutine(SpawnWave());
                countdown = timeForWave;
                countingdown = false;
            }
            else if (waveNumber >= waves.Length && enemyCount == 0)
            {
                GetComponentInParent<Manager>().WinEvent();
                LevelOver = true;
            }
            if(countingdown)
                countdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave()
    {
        //Select each enemy
        for (int i = 0; i < waves[waveNumber].wave.Length; i++)
        {
            //Spawn the current enemy 
            for (int j = 0; j < waves[waveNumber].wave[i].numToSpawn; j++)
            {
                // Create a single enemy and assign values
                GameObject spawned = Instantiate(enemyList[0].gameObject, transform.position, transform.rotation);
                // Lots of public variables, could be made into GET:SET for oop
                spawned.GetComponent<EnemyScript>().assignStats(waves[i].wave[i].enemy.speed, waves[i].wave[i].enemy.health, waves[i].wave[i].enemy.value, waves[i].wave[i].enemy.sprite, waves[i].wave[i].enemy.anim);
                enemyCount++;
                // Pause before next enemy, necessary to prevent too much overlap
                yield return new WaitForSeconds(.5f);
            }
        }
        countingdown = true;
        //End of wave
        Debug.Log("Wave over!");
        waveNumber++;
        if (waveNumber >= waves.Length)
            Debug.Log("WAVES OVER LEVEL COMPLETE");
    }

}
