using System;
using UnityEngine;

namespace HideAndSeek
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;

        [HideInInspector] public event Action CompleteLevel;

        void Update() //способ управления игроком, я выбрала клавишами
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector3.back * Time.deltaTime * speed);
            }
        }

        private void OnTriggerEnter(Collider collider) //если игрок соприкасается с выходом
        {
            if (collider.gameObject.tag == "Finish") CompleteLevel?.Invoke();
        }

    }
}
