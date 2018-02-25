using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv1Skelleton : EnemyStats
{

    public override void Die()
    {
        base.Die();

        Instantiate(Resources.Load("Level1Skelleton"));
        //StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(RespawnTimer);
    }

}
