using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSyrupFlower : Entity
{
    public override void Collect()
    {
        ActionLog.Log.LogAction(ActionLog.Log.actionColor_interact, log_collect);
    }

    public override void Feel()
    {
        ActionLog.Log.LogAction(ActionLog.Log.actionColor_interact, log_feel);
    }

    public override void Listen()
    {
        ActionLog.Log.LogAction(ActionLog.Log.actionColor_interact, log_listen);
    }

    public override void Look()
    {
        ActionLog.Log.LogAction(ActionLog.Log.actionColor_interact, log_look);

        RemoveStatusEffect(player, statusEffects[0]);
    }

    public override void Smell()
    {
        ActionLog.Log.LogAction(ActionLog.Log.actionColor_interact, log_smell);

        InflictStatusEffect(player, statusEffects[0]);
    }

    public override void Taste()
    {
        ActionLog.Log.LogAction(ActionLog.Log.actionColor_interact, log_taste);

        InflictStatusEffect(player, statusEffects[1]);
    }

}
