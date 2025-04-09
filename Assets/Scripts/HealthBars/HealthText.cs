using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0, 75, 0);
    public float timeToFade = 1f;

    private RectTransform _textTransForm;

    private float _timeElapsed = 0f;
    private Color _startColor;

    private TextMeshProUGUI _textMeshPro;
    // Start is called before the first frame update
    private void Awake()
    {
        _textTransForm = GetComponent<RectTransform>();
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _startColor = _textMeshPro.color;
    }

    // Update is called once per frame
    private void Update()
    {
        _textTransForm.position += moveSpeed * Time.deltaTime;
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed < timeToFade)
        {
            float fadeAlpha = _startColor.a * (1 - _timeElapsed / timeToFade);
            _textMeshPro.color = new Color(_startColor.r, _startColor.g, _startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
