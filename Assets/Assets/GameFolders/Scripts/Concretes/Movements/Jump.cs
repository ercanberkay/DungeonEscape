using System.Collections;
using System.Collections.Generic;
using UdemyProje3.Abstracts.Controllers;
using UdemyProje3.Abstracts.Movements;
using UnityEngine;

namespace UdemyProje3.Movements
{
    public class Jump:IJump
    {
        float jumpForce = 350f;
        Rigidbody2D _rigidbody;

        public Jump(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void TickWithFixedUpdate()
        {
            _rigidbody.velocity = Vector2.zero;

            _rigidbody.AddForce(Vector2.up * jumpForce);

            _rigidbody.velocity = Vector2.zero;
        }
    }
}

