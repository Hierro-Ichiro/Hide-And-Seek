using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace HideAndSeek
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1; //скорость перемещения
        [SerializeField] private float _rotateSpeed = 1; //скорость вращения
        [SerializeField] private float _stopTime = 1; //время пребывания на месте
        [SerializeField] private GameObject _allPoints; //объект из которого берутся значения точек перемещния
        [SerializeField] private RayScan _rayScan;

        [SerializeField] private string targetTag = "Player";

        [HideInInspector] public event Action targetFound;

        private Sequence _seq;
        private List<Vector3> _points = new List<Vector3>();

        private Transform _target;

        void Start()
        {
            Transform[] transforms = _allPoints.GetComponentsInChildren<Transform>();

            for (int i = 1; i < transforms.Length; i++)
            {
                _points.Add(transforms[i].position);
            }

            _seq = DOTween.Sequence();

            Patrolling();
        }

        private void Patrolling() //метод патрулирования
        {
            _seq = DOTween.Sequence();

            for (int i = 0; i < _points.Count; i++)
            {
                float distance; //переменная для расчёта расстояний между точками

                if (i == 0)
                {
                    distance = Vector3.Distance(transform.position, _points[i]);
                }
                else
                {
                    distance = Vector3.Distance(_points[i - 1], _points[i]);
                }

                _seq
                    .Append(transform.DOLookAt(_points[i], 1 / _rotateSpeed)) //для упрощения реализации было использовано одно и то же количество время для поворота, хотя можно посчитать угол между двумя объектами и из него выщитывать скорость поворота 
                    .Append(transform.DOMove(_points[i], distance  / _moveSpeed))
                    .AppendInterval(_stopTime);
            }

            _seq.AppendCallback(Patrolling);
        }

        public void SetTarget() //для сканера выставляется объект игрока
        {
            _target = GameObject.FindGameObjectWithTag(targetTag).transform;

            _rayScan.SetTarget(_target);
        }

        public void LookAtTarget() //враг смотрит на игрока, используется при проигрыше
        {
            _seq.Kill();

            transform.DOLookAt(_target.position, 1 / _rotateSpeed);
        }

        private void Update() //каждый кадр создаётся поле зрения врага
        {
            if (_rayScan.RayToScan())
            {
                targetFound?.Invoke();
                LookAtTarget();
            }
        }
    }
}
