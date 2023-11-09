using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLineHandler : MonoBehaviour
{
    [SerializeField] private GameObject _timeLine;
    [SerializeField] private Image _sliderFill;
    [SerializeField] private float _currentValue = 1f;
    [SerializeField] private float _maxValue = 1f;
    private Slider _slider;
    private RectTransform _sliderRect;

    private void Awake()
    {
        _slider = _timeLine.GetComponent<Slider>();
        _sliderRect = _timeLine.GetComponent<RectTransform>();
        _sliderRect.position = new Vector2(60, _sliderRect.position.y);

        SetMaxValue(_maxValue);
        Setvalue(_currentValue);
    }

    public void Show()
    {
        _sliderRect.anchoredPosition = new Vector2(-60, _sliderRect.anchoredPosition.y);
    }

    public void Hide()
    {
        _sliderRect.anchoredPosition = new Vector2(60, _sliderRect.anchoredPosition.y);
    }

    public void SetMaxValue(float max)
    {
        _slider.maxValue = max;
    }

    public void Setvalue(float value)
    {
        _slider.value = value;
    }

    internal void IndicateDebuff()
    {
        StartCoroutine(IndicatorRecolorRoutine(true));
    }

    private IEnumerator IndicatorRecolorRoutine(bool debuffed)
    {
        if (debuffed)
        {
            while (_sliderFill.color.g > 0 &&
                _sliderFill.color.b > 0)
            {
                _sliderFill.color = new Color(
                    _sliderFill.color.r,
                    _sliderFill.color.g - 0.2f,
                    _sliderFill.color.b - 0.2f
                    );
                yield return new WaitForFixedUpdate();
            }

            while (_sliderFill.color.g < 1 &&
                _sliderFill.color.b < 1)
            {
                _sliderFill.color = new Color(
                    _sliderFill.color.r,
                    _sliderFill.color.g + 0.2f,
                    _sliderFill.color.b + 0.2f
                    );
                yield return new WaitForFixedUpdate();
            }
            _sliderFill.color = new Color(1, 1, 1);
        }
        else
        {
            while (_sliderFill.color.r > 0 &&
            _sliderFill.color.b > 0)
            {
                _sliderFill.color = new Color(
                    _sliderFill.color.r - 0.2f,
                    _sliderFill.color.g,
                    _sliderFill.color.b - 0.2f
                    );
                yield return new WaitForFixedUpdate();
            }

            while (_sliderFill.color.r < 1 &&
                _sliderFill.color.b < 1)
            {
                _sliderFill.color = new Color(
                    _sliderFill.color.r + 0.2f,
                    _sliderFill.color.g,
                    _sliderFill.color.b + 0.2f
                    );
                yield return new WaitForFixedUpdate();
            }
            _sliderFill.color = new Color(1, 1, 1);
        }
        yield return null;
    }

    internal void IndicateDebuffReset()
    {
        StartCoroutine(IndicatorRecolorRoutine(false));
    }
}
