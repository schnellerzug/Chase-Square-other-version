using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "new Item")]
public class Item : ScriptableObject
{
    public string id;
    public int amount;
    public float actuelCooldown;
    public float actuelDuration;
    public bool active = false;

    public  virtual IEnumerator Use()
    {
       yield return null;
            
    }

    public virtual void OnBuy()
    {
        amount += 1;
    }

    public virtual void OnGameEnd()
    {

    }
}
