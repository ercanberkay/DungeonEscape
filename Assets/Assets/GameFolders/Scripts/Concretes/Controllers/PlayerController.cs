﻿using System.Collections;
using System.Collections.Generic;
using UdemyProje3.Abstracts.Animations;
using UdemyProje3.Abstracts.Combats;
using UdemyProje3.Abstracts.Controllers;
using UdemyProje3.Abstracts.Inputs;
using UdemyProje3.Abstracts.Movements;
using UdemyProje3.Animations;
using UdemyProje3.Inputs;
using UdemyProje3.Managers;
using UdemyProje3.Movements;
using UdemyProje3.Uis;
using UnityEngine;

namespace UdemyProje3.Controllers
{
    public class PlayerController : MonoBehaviour, IEntityController
    {
        [SerializeField] float moveSpeed = 3f;

        IPlayerInput _input;
        IMover _mover;
        IMyAnimation _animation;
        IFlip _flip;
        IJump _jump;
        IOnGround _onGround;
        IHealth _health;

        float _horizontal;
        bool _isJump = false;

        private void Awake()
        {
            _input = new MobileInput();
            _mover = new MoverWithTranslate(this,moveSpeed);
            _animation = new CharacterAnimation(GetComponent<Animator>());
            _flip = new FlipWithTransform(this);
            _jump = new Jump(GetComponent<Rigidbody2D>());
            _onGround = GetComponent<IOnGround>();
            _health = GetComponent<IHealth>();
        }

        private void OnEnable()
        {
            _health.OnDead += _animation.DeadAnimation;
            _health.OnDead += GameManager.Instance.SaveScore;
        }

        private void Start()
        {
            GameOverObject gameOverObject = FindObjectOfType<GameOverObject>();
            gameOverObject.SetPlayerHealth(_health);
        }

        private void Update()
        {
            if (_health.IsDead) return;

            _horizontal = _input.Horizontal;

            if (_input.AttackButtonDown)
            {
                _animation.AttackAnimation();
                return;
            }

            if (_input.JumpButtonDown && _onGround.IsGround)
            {
                _isJump = true;
            }

            _animation.JumpAnimation(!_onGround.IsGround);
            _animation.MoveAnimation(_horizontal);
        }

        private void FixedUpdate()
        {
            _flip.FlipCharacter(_horizontal);
            _mover.Tick(_horizontal);

            if (_isJump && _onGround.IsGround)
            {
                _jump.TickWithFixedUpdate();
                _isJump = false;
            }
        }
    }
}

