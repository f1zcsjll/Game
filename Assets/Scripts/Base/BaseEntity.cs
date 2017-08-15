using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

namespace Base
{
    public abstract class BaseEntity
    {
        protected long id;
        protected string name;
        protected EntityType type;
        protected Vector3 GridPosition = Vector3.zero;
        protected float Direction = 0;

        public virtual void Init(long Id, string Name, EntityType Type)
        {
            id = Id;
            name = Name;
            type = Type;
        }

        public long ID { get { return id; } }
        public string Name { get { return name; } }
        public EntityType Type { get { return type; } }
        
        public Vector4 Position
        {
            get
            {
                return new Vector4(GridPosition.x, GridPosition.y, GridPosition.z, Direction);
            }
            set
            {
                Direction = value.w;
                GridPosition = new Vector3(value.x, value.y, value.z);
            }
        }
    }
}