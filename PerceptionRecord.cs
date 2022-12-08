using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.AI {
    public class PerceptionRecord : ScriptableObject
    {
        public GameObject perceivedObject;
        public Vector3 perceivedObjectPosition;
        public Perceptor perceptor;
        public Vector3 perceptorPosition;
        public string perceivedActionId;

        public float timestamp;

        public void Initialize(GameObject perceivedObject, Perceptor perceptor) {
            this.perceivedObject = perceivedObject;
            this.perceptor = perceptor;

            this.perceivedObjectPosition = this.perceivedObject.transform.position;
            this.perceptorPosition = this.perceptor.transform.position;

            timestamp = Time.time;
        }
    }
}
