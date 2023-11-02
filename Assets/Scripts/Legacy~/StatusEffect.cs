using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Status Effect")]
public class StatusEffect : ScriptableObject
{
    [PreviewField(60), HideLabel]
    [HorizontalGroup("Split", 60)]
    public Sprite _sprite;

    [VerticalGroup("Split/Right"), LabelWidth(120)]
    public string _name;
    [VerticalGroup("Split/Right"), LabelWidth(120)]
    public Color _spriteColor;
    [VerticalGroup("Split/Right"), LabelWidth(120)]
    public AudioClip startEffectClip;
    [VerticalGroup("Split/Right"), LabelWidth(120)]
    public AudioClip endEffectClip;

    [TitleGroup("Effect Data")]
    public int DOTAmount;
    public float movementPenalty;
    public float duration;
}
