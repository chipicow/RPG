using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyStats : CharacterStats
{
    public int RespawnTimer;

    [System.Serializable]
    public class ItemAndDropRate
    {
        public Equipment equipment;
        public int dropChance;
    }

    public List<ItemAndDropRate> Drops = new List<ItemAndDropRate>();
    public delegate void OnDeath(GameObject enemy);
    public OnDeath onDeath;
    public override void Die()
    {
        base.Die();
        Vector3 deathLocation = gameObject.transform.position;
        gameObject.SetActive(false);
        DropLoot(deathLocation);
        if (onDeath != null)
            onDeath.Invoke(this.gameObject);
    }

    


    public void DropLoot(Vector3 position)
    {
        foreach (var item in Drops)
        {
            if (DropOrNot(item.dropChance))
            {
                Instantiate(Resources.Load(item.equipment.name), position, new Quaternion());
            }
        }
    }

    private bool DropOrNot(int itemDropChance)
    {
        System.Random gen = new System.Random();
        int prob = gen.Next(100);
        return prob <= itemDropChance;
    }
}
