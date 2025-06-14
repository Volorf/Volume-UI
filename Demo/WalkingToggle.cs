using System.Collections.Generic;
using UnityEngine;
using Volorf.VolumeUI;

public class WalkingToggle : MonoBehaviour
{
    [SerializeField] float _stepInterval = 2f;
    [SerializeField] float _movingSpeed = 1f;
    [SerializeField] AnimationCurve _movingCurve;
    [SerializeField] Toggle _toggle;
    [SerializeField] bool _backAndForth;
    [SerializeField] bool _switchWithColors;
    [SerializeField] List<Color> _bgColors = new();
    
    float _stepTimer;
    Vector3 _startPosition;
    Vector3 _targetPosition;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_stepTimer > _stepInterval)
        {
            _toggle.IsOn = !_toggle.IsOn;
            _stepTimer = 0f;
            _startPosition = transform.position;
            _targetPosition = _startPosition + transform.right  * _movingSpeed * (_toggle.IsOn ? -1f : 1f);
        }
        
        if (_backAndForth)
        {
            Move();
        }
        else
        {
            if (_toggle.IsOn)
            {
                Move();
            }
        }
        
        _stepTimer += Time.deltaTime;
    }

    void Move()
    {
        float f = _stepTimer / _stepInterval;
        f = _movingCurve.Evaluate(f);
        Vector3 pos = Vector3.Lerp(_startPosition, _targetPosition, f);
        transform.position = pos;
    }
    
}
