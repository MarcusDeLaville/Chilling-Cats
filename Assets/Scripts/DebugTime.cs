using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTime : MonoBehaviour
{
    [SerializeField] private Slider _timeScaleSlider;
    [SerializeField] private Text _textTime;

    [SerializeField] private int _secondsPassed;

    [SerializeField] private GameObject _particle;

    private void Awake()
    {
        _secondsPassed = PlayerPrefs.GetInt("SecondsPassed");

        StartCoroutine(CountingTime());
    }

    public void SetTimeScale()
    {
        Time.timeScale = _timeScaleSlider.value;
    }

    private IEnumerator CountingTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _secondsPassed++;
            DrawTime(_secondsPassed);
        }
    }

    private void DrawTime(int value)
    {
        int globalSeconds = value;

        int hourses = globalSeconds / 3600;
        int minutes = (globalSeconds % 3600) / 60;

        _textTime.text = $"Часы: {hourses} / Минуты: {minutes}";
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("SecondsPassed", _secondsPassed);
    }

    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetInt("SecondsPassed", _secondsPassed);
    }

    public void SwitchParticleState()
    {
        _particle.SetActive(!_particle.activeSelf);
    }
}
