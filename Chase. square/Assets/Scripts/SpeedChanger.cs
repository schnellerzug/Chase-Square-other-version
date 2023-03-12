using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "new TimeChanger ")]
public class SpeedChanger : Item
{
    public float changeMult;

    private float startTime;
    public float cooldown;
    public float duration;
    


    public override IEnumerator Use()
    {
        if (amount <= 0)
            yield break;

        amount -= 1;

     
        GameManager.instance.speed *= changeMult;

        
        active = true;
        startTime = Time.time;
        actuelDuration = duration;
        while (actuelDuration > 0)
        {
            actuelDuration = duration - (Time.time - startTime);
            yield return null;
        }
        //yield return new WaitForSeconds(duration);

        GameManager.instance.speed /= changeMult;
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
