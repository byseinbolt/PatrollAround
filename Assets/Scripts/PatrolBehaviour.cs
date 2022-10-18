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
    [SerializeField] private float _delayBeforeNextPoint;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _travelTime;

    private float _currentTime;
    private float _currentDelay;
    private int _currentPointIndex;

    private void Awake()
    {
        SetNextPoints();
    }

    private void SetNextPoints()
    {
        var nextPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;

        _startPosition = _patrolPoints[_currentPointIndex].position;
        _endPosition = _patrolPoints[nextPointIndex].position;
        _travelTime = Vector3.Distance(_startPosition, _endPosition)/_speed;

        _currentPointIndex = nextPointIndex;
    }

    private void Update()
    {
        _currentDelay += Time.deltaTime;
        if (_currentDelay<_delayBeforeNextPoint)
        {
            return;
        }

        MoveToNextPoint();
        
        if (_currentTime > _travelTime)
        {
            _currentDelay = 0;
            _currentTime = 0f;
            SetNextPoints();
        }
    }

    private void MoveToNextPoint()
    {
        _currentTime += Time.deltaTime;
        var progress = _currentTime / _travelTime;
        transform.position = Vector3.Lerp(_startPosition, _endPosition, progress);
    }
    

    
}
