using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{

    #region Singleton
    public static EnemyZone instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public string ZoneDescription;
    public GameObject[] MonstersList;
    public float Radius;
    public Vector3 center;
    void Start()
    {
        foreach (var monster in MonstersList)
        {
            CreateEnemy(monster);
        }
    }

    void Update()
    {

    }

    void Respawn(string prefabName)
    {
        Debug.Log(prefabName);
        var enemy = (GameObject)Resources.Load("MonstersPrefabs/" + prefabName);
        StartCoroutine(WaitForRespawn(enemy.GetComponent<EnemyStats>()));
    }


    IEnumerator WaitForRespawn(EnemyStats enemy)
    {
        yield return new WaitForSeconds(enemy.RespawnTimer);
        CreateEnemy(enemy.gameObject);
    }
    void CreateEnemy(GameObject enemy)
    {
        Vector3 pos = RandomCircle(center, Radius);
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
        var clone = (GameObject)Instantiate(Resources.Load("MonstersPrefabs/" + enemy.gameObject.name), pos, rot);
        clone.name = clone.name.Substring(0, clone.name.Length - 7);
        clone.GetComponent<EnemyStats>().onDeath += Respawn;
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
}
