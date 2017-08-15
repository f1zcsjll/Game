using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Entity
{
    public class Player : Base.BaseEntity
    {
        public override void Init(long Id, string Name, Define.EntityType Type)
        {
            base.Init(Id, Name, Type);
        }
    }
}