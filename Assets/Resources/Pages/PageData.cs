using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PageData", order = 1)]
public class PageData : ScriptableObject
{
    public string wildlifeName;
    public string scientificName;
    public enum Classification { NYMPH, FENRIR, KAPPA, GORGON, PHOENIX }
    public Classification wildlifeClass;

    public Sprite portrait;
    public string portraitSubline;

    public string height;
    public string lifespan;
    public string maturity;
    public string breedingAge;
    public string young;
    public enum Population { Common, Uncommon, Rare, Unique }
    public Population wildlifePopulation;

    public enum Temperament { Docile, Active, Skittish, Aggressive}
    public Temperament wildlifeTemperament;

    public string diet;
}
