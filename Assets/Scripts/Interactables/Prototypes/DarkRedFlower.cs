using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRedFlower : Entity
{ 
    public override void Collect()
    {
        if (!CheckRange(1.5f))
            return;

        Debug.Log("Collecting " + actorName);
    }

    public override void Feel()
    {
        Debug.Log("Feeling " + actorName);
    }

    public override void Listen()
    {
        Debug.Log("Listening to " + actorName);
    }

    public override void Look()
    {
        Debug.Log("Looking at " + actorName);
    }

    public override void Smell()
    {
        Debug.Log("Smelling " + actorName);
    }

    public override void Taste()
    {
        Debug.Log("Tasting " + actorName);
        InflictStatusEffect(player, statusEffects[0]);
    }

}