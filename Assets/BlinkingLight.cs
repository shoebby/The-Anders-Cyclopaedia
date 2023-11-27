using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    private Light _light;
    private float timerCurrent;
    [SerializeField] private float timerMax = 1f;

    void Awake()
    {
        _light = GetComponent<Light>();
        timerCurrent = timerMax;
    }

    void Update()
    {
        if (timerCurrent <= 0)
        {
            _light.enabled = !_light.enabled;
            timerCurrent = timerMax;
        }
        timerCurrent -= Time.deltaTime;
    }
}
