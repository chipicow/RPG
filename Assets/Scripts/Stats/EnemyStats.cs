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
    public delegate void OnDeath(string prefabName);
    public event OnDeath onDeath;

    public override void Die()
    {
        base.Die();
        Vector3 deathLocation = gameObject.transform.position;
        onDeath.Invoke(this.gameObject.name);
        Destroy(gameObject);
        DropLoot(deathLocation);
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
