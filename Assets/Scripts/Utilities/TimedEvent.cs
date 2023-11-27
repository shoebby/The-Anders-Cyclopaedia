using UnityEngine;
using UnityEngine.Events;

public class TimedEvent : MonoBehaviour
{
    [SerializeField]
    private float timerMax = 1f;

    [SerializeField]
    private UnityEvent onTimerZeroEvent = default;

    private float timerCurrent;

    public UnityEvent OnTimerZero => onTimerZeroEvent;

    void Awake()
    {
        timerCurrent = timerMax;
    }

    void Update()
    {
        if (timerCurrent <= 0)
        {
            OnTimerZero.Invoke();
            timerCurrent = timerMax;
        }
        timerCurrent -= Time.deltaTime;
    }
}
