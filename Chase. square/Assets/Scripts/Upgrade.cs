using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : ScriptableObject
{
    public int actuelLevel;
    public int maxLevel;

    public virtual void Increase()
    {
        if (actuelLevel < maxLevel)
            actuelLevel++;
    }
}
