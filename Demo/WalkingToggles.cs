using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Volorf.VolumeUI;

public class WalkingToggles : MonoBehaviour
{
    [SerializeField] GameObject _walkingTogglePrefab;
    [SerializeField] int _width;
    [SerializeField] int _height;
    [SerializeField] float _xOffset;
    [SerializeField] float _yOffset;

    void Start()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Vector3 position = new Vector3(i * _yOffset - _width / 2f * _yOffset, 0, j * _xOffset - _height / 2f * _xOffset);
                GameObject toggleObj = Instantiate(_walkingTogglePrefab, position, Quaternion.identity);
                toggleObj.GetComponent<WalkingToggle>().currentColor = j;
                toggleObj.name = $"WalkingToggle_{i}_{j}";
            }
        }
    }
}
