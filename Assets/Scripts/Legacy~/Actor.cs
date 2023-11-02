using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [TitleGroup("Core Stats")]
    [ReadOnly, SerializeField] public string actorName;
    [ReadOnly, SerializeField] protected float health;
    [ReadOnly, SerializeField] protected float moveDelay;

    [TitleGroup("Status Effect Sprite")]
    [SerializeField] protected SpriteRenderer effectSpriteRenderer;
    protected Dictionary<StatusEffect, float> currentEffects = new Dictionary<StatusEffect, float>();
    protected float timeUntilTick = 1f;
    protected float timeToMove;

    protected virtual void Update()
    {
        if (currentEffects.Count > 0)
            timeUntilTick -= Time.deltaTime;
        timeToMove -= Time.deltaTime;

        if (currentEffects.Count > 0 && timeUntilTick < 0f)
        {
            for (int i = 0; i < currentEffects.Count; ++i)
            {
                effectSpriteRenderer.sprite = currentEffects.ElementAt(i).Key._sprite;
                effectSpriteRenderer.color = currentEffects.ElementAt(i).Key._spriteColor;

                KeyValuePair<StatusEffect, float> tempCopy = currentEffects.ElementAt(i);

                if (tempCopy.Value <= 0)
                {
                    moveDelay -= tempCopy.Key.movementPenalty;
                    currentEffects.Remove(tempCopy.Key);
                }
                else
                {
                    currentEffects[tempCopy.Key] -= 1f;
                    
                    if (tempCopy.Key.DOTAmount > 0f)
                        HurtActor(tempCopy.Key.DOTAmount);

                    Debug.Log(tempCopy.Key._name + " applied to " + actorName + ", dealing " + tempCopy.Key.DOTAmount + " damage.");
                }
            }
            timeUntilTick = 1f;
        }
        else if (currentEffects.Count <= 0)
        {
            effectSpriteRenderer.sprite = null;
        }

    }

    protected virtual void SlowActor(Actor target, float slowAmount)
    {
        target.moveDelay += slowAmount;
    }

    protected virtual void HurtActor(int hurtAmount)
    {
        health -= hurtAmount;
    }

    #region Status Effect Handling
    protected virtual void InflictStatusEffect(Actor target, StatusEffect effect)
    {
        Debug.Log(target.actorName + " has been affected by " + effect._name);

        target.currentEffects.Add(effect, effect.duration);
        SlowActor(target, effect.movementPenalty);

        InvokeRepeating("HandleStatusEffects", 0, 1);
    }

    protected virtual void HandleStatusEffects()
    {
        //ToDo: move update function here and get it functioning
    }

    protected virtual void RemoveStatusEffect(Actor target, StatusEffect effect)
    {
        if (target.currentEffects.ContainsKey(effect))
        {
            target.currentEffects.Remove(effect);
            Debug.Log("Removed " + effect._name + " from " + target.actorName);
        }
    }
    #endregion
}
