using System.Collections;
using System.Collections.Generic;
using UdemyProje3.Abstracts.Movements;
using UdemyProje3.Controllers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UdemyProje3.Movements
{
    public class MoverWithVelocity : IMover
    {
        Rigidbody2D _rigidbody2D;

        float _moveSpeed = 10f;

        public MoverWithVelocity(PlayerController playerController)
        {
            _rigidbody2D = playerController.GetComponent<Rigidbody2D>();
        }

        public void Tick(float horizontal)
        {
            if (_rigidbody2D.velocity.magnitude >= 5f) return;

            Vector2 direction = new Vector2(horizontal, 0f);
            Vector2 movement = direction.normalized * Time.fixedDeltaTime * _moveSpeed;

            _rigidbody2D.velocity = movement;
        }
    }
}

