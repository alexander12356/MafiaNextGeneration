using UnityEngine;

namespace Beansjam.PersonSystemClasses.PersonClasses
{
    public class Person : MonoBehaviour
    {
        private enum State
        {
            Patrol
        }

        private State m_State = State.Patrol;
        private Vector3 m_MovingTarget;
        private float MOVING_RANGE = 0.01f;

        public Vector2 randomBounds;

        private void Start()
        {
            m_MovingTarget = transform.position;
        }

        private void Update()
        {
            switch(m_State)
            {
                case State.Patrol:
                    PatrolUpdate();
                    break;
            }
        }

        private void PatrolUpdate()
        {
            if (Vector3.Distance(transform.position,  m_MovingTarget) < MOVING_RANGE)
            {
                m_MovingTarget = PersonManager.Instance.GetRandomFromRect();
            }
            transform.position = Vector3.MoveTowards(transform.position, m_MovingTarget, Time.deltaTime);
        }
    }
}