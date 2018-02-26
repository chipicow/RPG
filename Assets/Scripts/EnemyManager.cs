using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    #region Singleton
    public static EnemyManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    private List<GameObject> ExistingEnemies;

    void Start()
    {
        ExistingEnemies = new List<GameObject>();
    }

    void Update()
    {
        EnemyStats[] whatever = FindObjectsOfType(typeof(EnemyStats)) as EnemyStats[];
        foreach (var enemy in whatever)
        {
            if (!ExistingEnemies.Contains(enemy.gameObject))
            {
                Debug.Log("mob add to enemymanager");
                ExistingEnemies.Add(enemy.gameObject);
                enemy.onDeath += Respawn;
            }
        }
    }

    void Respawn(GameObject enemy)
    {
        var toBeRespawned = ExistingEnemies[ExistingEnemies.IndexOf(enemy)];
        StartCoroutine(WaitForRespawn(toBeRespawned.GetComponent<EnemyStats>()));
    }


    IEnumerator WaitForRespawn(EnemyStats enemy)
    {
        yield return new WaitForSeconds(enemy.RespawnTimer);
        CreateEnemy(enemy.gameObject);
        ExistingEnemies.Remove(enemy.gameObject);
        Destroy(enemy.gameObject);
    }
    void CreateEnemy(GameObject enemy)
    {
        Vector3 pos = RandomCircle(enemy.transform.position, 20f);
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, enemy.transform.position - pos);
        var clone = Instantiate(Resources.Load(enemy.gameObject.name), pos, rot);
        clone.name = clone.name.Substring(0, clone.name.Length - 7);
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}
