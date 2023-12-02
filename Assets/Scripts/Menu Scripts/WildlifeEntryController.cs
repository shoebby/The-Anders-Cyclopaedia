using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WildlifeEntryController : MonoBehaviour
{
    [Header("Wildlife Data Reference")]
    [SerializeField] private PageData data;

    [Header("Header Components")]
    [SerializeField] private TextMeshProUGUI wildlifeName;
    [SerializeField] private TextMeshProUGUI scientificName;
    [SerializeField] private TextMeshProUGUI classification;

    [Header("Portrait Components")]
    [SerializeField] private Image portrait;
    [SerializeField] private TextMeshProUGUI portraitSubline;

    [Header("Other Components")]
    [SerializeField] private TextMeshProUGUI height;
    [SerializeField] private TextMeshProUGUI lifespan;
    [SerializeField] private TextMeshProUGUI maturity;
    [SerializeField] private TextMeshProUGUI breedingAge;
    [SerializeField] private TextMeshProUGUI young;
    [SerializeField] private TextMeshProUGUI population;
    [SerializeField] private TextMeshProUGUI temperament;
    [SerializeField] private TextMeshProUGUI diet;

    void Start()
    {
        wildlifeName.text = data.wildlifeName;
        scientificName.text = data.scientificName;
        classification.text = data.wildlifeClass.ToString();

        portrait.sprite = data.portrait;
        portraitSubline.text = data.portraitSubline;

        height.text = data.height;
        lifespan.text = data.lifespan;
        maturity.text = data.maturity;
        breedingAge.text = data.breedingAge;
        young.text = data.young;
        population.text = data.wildlifePopulation.ToString();
        temperament.text = data.wildlifeTemperament.ToString();
        diet.text = data.diet;
    }
}
