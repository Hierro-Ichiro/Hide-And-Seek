using UnityEngine;

namespace HideAndSeek
{
    public class RayScan : MonoBehaviour //сканер для врагов
    {
        public int rays = 6; //количество лучей
        public int distance = 3; //длина лучей
        public float angle = 20; //угол обзора
        public Vector3 offset; //отступ от объекта

        [SerializeField] private LineRenderer _linePrefab;

        private Transform _target;

        private LineRenderer[] _lines;

        void Start()
        {
            _lines = new LineRenderer[rays * 2];

            for (int i = 0; i < _lines.Length; i++)
            {
                _lines[i] = Instantiate(_linePrefab);
            }
        }

        bool GetRaycast(Vector3 dir, LineRenderer line) //создание каждой линии сканера
        {
            bool result = false;
            Vector3 pos = transform.position + offset;
            if (Physics.Raycast(pos, dir, out var hit, distance))
            {
                if (_target != null && hit.transform == _target) //если луч упирается в игрока
                {
                    result = true;
                    line.SetPosition(0, pos);
                    line.SetPosition(1, hit.point);
                }
                else //если луч упирается в стену, луч останавливается на стене
                {
                    line.SetPosition(0, pos);
                    line.SetPosition(1, hit.point);
                }
            }
            else //в ином случае луч огранивается только своей длиной
            {
                line.SetPosition(0, pos);
                Vector3 endPosition = new Vector3(pos.x + dir.x * distance, pos.y, pos.z + dir.z * distance);
                line.SetPosition(1, endPosition);
            }
            return result;
        }

        public bool RayToScan() //создание всех лучей
        {
            bool result = false;
            bool a = false;
            bool b = false;
            float j = 0;
            for (int i = 0; i < rays; i++)
            {
                var x = Mathf.Sin(j);
                var y = Mathf.Cos(j);

                j += angle * Mathf.Deg2Rad / rays;

                Vector3 dir = transform.TransformDirection(new Vector3(x, 0, y));
                if (GetRaycast(dir, _lines[i])) a = true; //создание лучей с одной стороны

                if (x != 0)
                {
                    dir = transform.TransformDirection(new Vector3(-x, 0, y));
                    if (GetRaycast(dir, _lines[_lines.Length - i])) b = true; //создание луча с противоположной стороны
                }
            }

            if (a || b)
            {
                result = true;
                foreach (var line in _lines) //если игрок найден, окрашивается в красный цвет
                {
                    line.startColor = Color.red;
                    line.endColor = Color.red;
                }
            }

            return result;
        }

        public void SetTarget(Transform target) //установка цели
        {
            _target = target;
        }
    }
}
