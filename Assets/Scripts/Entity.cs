using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : Actor
{
    protected float interactRange;
    protected static PlayerMovement player; 
    protected Vector2 playerPos;

    [TitleGroup("Additional Stats")]
    [ReadOnly, SerializeField] protected float atkDamage;

    [TitleGroup("References")]
    [SerializeField] protected StatusEffect[] statusEffects;
    [OnValueChanged("InitStats", includeChildren:true)]
    [SerializeField] protected EntityStats stats;
    [SerializeField] protected SpriteRenderer entitySprite;

    [TitleGroup("Action Logs")]
    [ReadOnly, TextArea(3, 5)] [SerializeField] protected string log_collect;
    [ReadOnly, TextArea(3, 5)] [SerializeField] protected string log_listen;
    [ReadOnly, TextArea(3, 5)] [SerializeField] protected string log_look;
    [ReadOnly, TextArea(3, 5)] [SerializeField] protected string log_smell;
    [ReadOnly, TextArea(3, 5)] [SerializeField] protected string log_taste;
    [ReadOnly, TextArea(3, 5)] [SerializeField] protected string log_feel;

    protected virtual void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();

        //initialize local stats
        InitStats();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Highlighter"))
        {
            Debug.Log("interactable is highlighted");

            player.SetTargetEntity(this);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Highlighter"))
        {
            Debug.Log("interactable is no longer highlighted");

            player.ClearTargetEntity();
        }
    }

    protected virtual void InitStats()
    {
        if (stats != null)
        {
            actorName = stats._name;
            entitySprite.sprite = stats._sprite;
            health = stats.GetStat(EntityStats.Stat.health);
            moveDelay = stats.GetStat(EntityStats.Stat.moveDelay);
            atkDamage = stats.GetStat(EntityStats.Stat.atkDamage);
        }
    }

    protected virtual bool CheckRange(float range)
    {
        playerPos = player.transform.position;
        if (Vector2.Distance(playerPos, transform.position) > range)
        {
            Debug.Log(actorName + " is out of reach");
            return false;
        }
        return true;
    }

    protected virtual void EntityDeath()
    {
        //Kill the entity
    }

    public abstract void Listen();
    public abstract void Look();
    public abstract void Taste();
    public abstract void Smell();
    public abstract void Feel();
    public abstract void Collect();
}
