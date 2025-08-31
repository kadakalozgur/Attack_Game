using System.Collections;
using System.Collections.Generic;
using UnityEngine;



///////////////////////////A large part of this place was made by artificial intelligence///////////////////////////



public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    private class EnemySpawnData
    {
        public GameObject template;   
        public Vector3 position;
        public Quaternion rotation;
    }

    public float respawnDelay = 3f;

    private List<EnemySpawnData> spawnPoints = new List<EnemySpawnData>();
    private List<GameObject> aliveEnemies = new List<GameObject>();
    private bool isRespawning = false;

    private PlayerHealthBar playerHealthBar;

    void Start()
    {
        playerHealthBar = FindObjectOfType<PlayerHealthBar>();


        EnemyHealthBar[] enemies = FindObjectsOfType<EnemyHealthBar>();

        foreach (var enemyHealth in enemies)
        {
            GameObject original = enemyHealth.gameObject;

            EnemySpawnData data = new EnemySpawnData()
            {

                position = original.transform.position,
                rotation = original.transform.rotation

            };

            GameObject template = Instantiate(original);
            template.name = original.name + "_Template";
            template.SetActive(false); 
          

            data.template = template;

            spawnPoints.Add(data);

            aliveEnemies.Add(original);
        }
    }

    void Update()
    {
        if (playerHealthBar != null && playerHealthBar.isDead)
            return;

        CleanDeadEnemies();

        if (aliveEnemies.Count == 0 && !isRespawning && spawnPoints.Count > 0)
        {
            StartCoroutine(RespawnEnemies());
        }
    }

    private void CleanDeadEnemies()
    {
        for (int i = aliveEnemies.Count - 1; i >= 0; i--)
        {
            GameObject go = aliveEnemies[i];

            if (go == null)
            {
                aliveEnemies.RemoveAt(i);
                continue;
            }

            Enemy enemyComp = go.GetComponent<Enemy>();
            if (enemyComp == null)
            {

                aliveEnemies.RemoveAt(i);
                continue;

            }

            if (!enemyComp.enabled || !go.activeInHierarchy)
            {

                aliveEnemies.RemoveAt(i);

            }
        }
    }

    private IEnumerator RespawnEnemies()
    {
        isRespawning = true;

        yield return new WaitForSeconds(respawnDelay);

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            EnemySpawnData data = spawnPoints[i];
            if (data.template == null)
            {
              
                continue;

            }

            GameObject newEnemy = Instantiate(data.template, data.position, data.rotation);

            newEnemy.name = data.template.name.Replace("_Template", "");

            newEnemy.SetActive(true);
            aliveEnemies.Add(newEnemy);
        }

        isRespawning = false;
    }
    public void SetRespawnDelay(float seconds)
    {

        respawnDelay = seconds;

    }
}
