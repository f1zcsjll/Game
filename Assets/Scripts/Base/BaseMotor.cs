using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Base
{
    public abstract class BaseMotor : MonoBehaviour
    {
        NavMeshAgent agent;

        protected virtual void Init(NavMeshAgent Agent)
        {
            agent = Agent;
            agent.updateRotation = true;
            agent.ResetPath();
        }

        public abstract void InitEntity(long Id, string Name, Define.EntityType Type);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <returns></returns>
        public abstract BaseEntity GetEntity();

        /// <summary>
        /// 停止移动
        /// </summary>
        public void StopMove()
        {
            agent.ResetPath();
        }

        /// <summary>
        /// 移动到某个目标
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool MoveDestination(GameObject target)
        {
            StopMove();
            return agent.SetDestination(target.transform.position);
        }

        /// <summary>
        /// 移动到某个坐标点
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool MoveDestination(Vector3 target)
        {
            StopMove();
            return agent.SetDestination(target);
        }

        /// <summary>
        /// 瞬间偏移移动方向
        /// </summary>
        /// <param name="offset"></param>
        public void OffsetPosition(Vector3 offset)
        {
            agent.Move(offset);
        }
    }
}