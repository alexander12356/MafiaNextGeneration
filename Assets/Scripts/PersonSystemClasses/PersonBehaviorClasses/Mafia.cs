﻿using System;
using UnityEngine;

namespace MafiaNextGeneration.PersonSystemClasses.PersonBehaviorClasses
{
    public class Mafia : BaseBehavior
    {
        private Vector2 m_MovingTarget;

        protected override void Start()
        {
            base.Start();
            BehaviorType = "Mafia";
        }

        public override void UpdateBehavior()
        {
            switch (CurrentState)
            {
                case State.Patrol:
                    PatrolUpdate();
                    break;
                case State.RunAway:
                    RunUpdate();
                    break;
            }
        }

        private void RunUpdate()
        {
            throw new NotImplementedException();
        }
    }
}