using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

namespace Base
{
    public abstract class BaseEntity
    {
        public long ID;
        public string Name;
        public abstract EntityType F_GetType();
        Vector3 GridPosition = Vector3.zero;
        float Direction = 0;
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
        public abstract string F_GetPath();
    }
}