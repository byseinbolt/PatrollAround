using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _detentionBeforeNextPoint;

    private float _currentTime;
    private float _currentDetention;
    private int _firstIndex = 0;
    private int _secondIndex = 1;
    private bool _isLastPoint;

    private void Update()
    {
        _currentDetention += Time.deltaTime;
        if (_currentDetention<_detentionBeforeNextPoint)
        {
            return;
        }

        CheckStartOver();

        var distance = Vector3.Distance(_patrolPoints[_firstIndex].position, _patrolPoints[_secondIndex].position);
        var travelTime = distance / _speed;
        _currentTime += Time.deltaTime;
        var progress = _currentTime / travelTime;

        var newPosition = Vector3.Lerp(_patrolPoints[_firstIndex].position, _patrolPoints[_secondIndex].position,
            progress);
        transform.position = newPosition;

        if (!(_currentTime > travelTime)) return;
        _currentDetention = 0f;
        _currentTime = 0f;
        _firstIndex++;
        _secondIndex++;

        if (!_isLastPoint) return;
        _firstIndex = 0;
        _secondIndex = 1;
        _isLastPoint = false;
    }

    private void CheckStartOver()
    {
        if (_secondIndex >= _patrolPoints.Length)
        {
            _firstIndex = _patrolPoints.Length - 1;
            _secondIndex = 0;
            _isLastPoint = true;
        }
    }

   
}
