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

    public EnemyScriptableObject[] enemies;

    void Update()
    {
        if (!LevelOver)
        {
            //Countdown if the wave is not the last
            if (countdown <= 0 && waveNumber < waves.Length)
            {
                StartCoroutine(SpawnWave());
                countdown = timeForWave;
                countingdown = false;
            }
            //if the wave is the last and all enemies are defeated, que win event
            else if (waveNumber >= waves.Length && enemyCount == 0)
            {
                GetComponentInParent<Manager>().WinEvent();
                LevelOver = true;
            }
            //countdown
            if(countingdown)
                countdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave()
    {
        //CURRENTLY WONT SPAWN 3RD ENEMY TYPE, UNKNOWN WHY
        float speed, health;
        int value, ID;
        RuntimeAnimatorController animator;
        //waves[i].wave[i].enemy.speed, waves[i].wave[i].enemy.health, waves[i].wave[i].enemy.value, waves[i].wave[i].enemy.sprite, waves[i].wave[i].enemy.anim, waves[i].wave[i].enemy.ID
        //Select each enemy
        for (int i = 0; i < waves[waveNumber].wave.Length; i++)
        {
            //Spawn the current enemy 
            for (int j = 0; j < waves[waveNumber].wave[i].numToSpawn; j++)
            {
                ID = waves[waveNumber].wave[i].enemy.ID;
                Debug.Log(waves[i].wave[i].enemy);
                GameObject spawned;
                if (waves[waveNumber].wave[i].enemy.enemytype == 1)
                {
                    // Create a single boss enemy and assign values
                    Debug.Log("BOSS");
                    spawned = Instantiate(enemyList[1].gameObject, transform.position, transform.rotation);
                }
                else
                {
                    // Create a single enemy and assign values
                    spawned = Instantiate(enemyList[0].gameObject, transform.position, transform.rotation);
                }
                //Get enemy scriptable object components from the wave scriptable object
                spawned.GetComponent<EnemyScript>().assignSelf(enemies[ID]);
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
