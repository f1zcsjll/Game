using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic.Entity;

namespace Logic.Motor
{
    public class PlayerMotor : Base.BaseMotor
    {
        Player player;
        protected override void Init(UnityEngine.AI.NavMeshAgent Agent)
        {
            base.Init(Agent);
        }
        public override void InitEntity(long Id, string Name, Define.EntityType Type)
        {
            player = new Player();
            player.Init(Id, Name, Type);
            Init(gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>());
        }

        public override Base.BaseEntity GetEntity()
        {
            return player;
        }
    }
}