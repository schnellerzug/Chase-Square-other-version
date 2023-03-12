using System.Collections;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "new Shield ")]
public class Shield : Item
{
    private ParticleSystem ps;
    private CircleCollider2D collider;

    
    public float cooldown;
    private float startTime;

    public float duration;
    public float size = 1.6f;

    
    public override IEnumerator Use()
    {
        if (amount <= 0)
            yield break;

        amount -= 1;

        var sh = GameObject.Find("Shield");
        ps = sh.GetComponent<ParticleSystem>();
        collider = sh.GetComponent<CircleCollider2D>();
        
        ps.Stop(); 
        var main = ps.main;
        main.duration = duration;
        ps.Play();
        
        collider.enabled = true;
        active = true;
        startTime = Time.time;
        actuelDuration = duration;
        while(actuelDuration> 0)
        {
            actuelDuration = duration - (Time.time - startTime);
            yield return null;
        }
        //yield return new WaitForSeconds(duration);

        collider.enabled = false;
        active = false;

        startTime = Time.time;
        actuelCooldown = cooldown;
        while(actuelCooldown > 0)
        {
            actuelCooldown  = cooldown - (Time.time - startTime);
            yield return null;
        }
        actuelCooldown = 0;
    }

}
