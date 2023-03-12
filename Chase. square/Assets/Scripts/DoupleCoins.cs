using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "new DoupleCoin ")]
public class DoupleCoins : Item
{
    public 
        float multiplikator;
    public override IEnumerator Use()
    {
        if (amount <= 0)
            yield break;

        amount -= 1;

        active = true;
        Storage.instance.coinsMultiplier *= multiplikator;
        
    }

    public override void OnGameEnd()
    {
        active = false;
        Storage.instance.coinsMultiplier /= multiplikator;

    }

}
