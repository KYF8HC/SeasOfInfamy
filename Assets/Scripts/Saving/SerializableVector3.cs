using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    [Serializable]
    public class SerializableVector3
    {
        private float x;
        private float y;
        private float z;

        public SerializableVector3(Vector3 vector3)
        {
            x = vector3.x;
            y = vector3.y;
            z = vector3.z;
        }
        
        public Vector3 ConvertToVector3()
        {
            return new Vector3(x, y, z);
        }
    }
}