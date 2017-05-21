using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MafiaNextGeneration.PersonSystemClasses.PersonBehaviorClasses
{
    public abstract class BaseBehavior : MonoBehaviour
    {
        public enum State
        {
            Patrol,
            Hunting,
            RunAway
        }

        public State CurrentState;
        public string BehaviorType;
        public float MOVING_RANGE = 0.01f;

        private Vector2 m_MovingTarget;

        protected SpriteRenderer m_SpriteRenderer;

        public abstract void UpdateBehavior();

        public void Awake()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected virtual void Start()
        {
            m_MovingTarget = PersonManager.Instance.GetRandomPos();
        }

        public void SetState(State newState)
        {
            CurrentState = newState;
        }

        protected void PatrolUpdate()
        {
            if (Vector2.Distance(transform.position, m_MovingTarget) < MOVING_RANGE)
            {
                m_MovingTarget = PersonManager.Instance.GetRandomPos();
            }
            transform.position = Vector3.MoveTowards(transform.position, m_MovingTarget, Time.deltaTime);
        }
    }
}
