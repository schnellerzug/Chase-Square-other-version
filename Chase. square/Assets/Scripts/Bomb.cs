using System.Collections;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "new Bomb")]
public class Bomb : Item
{
    
    public float cooldown;
    private float startTime;
    public override IEnumerator Use()
    {   
       if (GameManager.instance == null)
            yield break;

        if (amount <= 0)
            yield break;

       

        active = true;

        foreach (Obstacle o in GameManager.instance.obstacleSpawnwer.childs)
        {
            if (o == null)
                yield return null;
            if (!(o.gameObject.name == GameManager.instance.obstacleSpawnwer.gameObject.name))
            {
                o.gameObject.SetActive(false);

            }


        }
        foreach (Obstacle b in GameManager.instance.backgroundSpawnwer.childs)
        {
            if (!(b.gameObject.name == GameManager.instance.backgroundSpawnwer.gameObject.name))
            {
                b.gameObject.SetActive(false);
            }



        }

        amount -= 1;
        active = false;
        startTime = Time.time;
        actuelCooldown = cooldown;
        while (actuelCooldown > 0)
        {
            actuelCooldown = cooldown - (Time.time - startTime);
            yield return null;
        }
        actuelCooldown = 0;
    }

}