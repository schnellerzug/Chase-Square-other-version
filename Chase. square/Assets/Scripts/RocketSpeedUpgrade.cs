
using UnityEngine;

[CreateAssetMenu(menuName = "new RocketSpeedUpgrade")]
public class RocketSpeedUpgrade : Upgrade
{
    public float[] speed;

    public override void Increase()
    {
        base.Increase();
        Storage.instance.playerSpeedMultiplikator = speed[actuelLevel];
    }
}
    

