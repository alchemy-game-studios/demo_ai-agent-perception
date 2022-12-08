using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.AI {
    public class SightPerceptor : Perceptor
    {
        public float sightFieldAngle = 45f;
        public LayerMask obstructionMask;

        public void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Quaternion leftRayRotation = Quaternion.AngleAxis( -sightFieldAngle, Vector3.up );
            Quaternion rightRayRotation = Quaternion.AngleAxis( sightFieldAngle, Vector3.up );
            Vector3 leftRayDirection = leftRayRotation * transform.forward;
            Vector3 rightRayDirection = rightRayRotation * transform.forward;

            Gizmos.DrawRay(this.transform.position, this.transform.forward * this.perceptionRange);
            Gizmos.DrawRay(this.transform.position, leftRayDirection * this.perceptionRange );
            Gizmos.DrawRay(this.transform.position, rightRayDirection * this.perceptionRange );
        }

        protected override List<Collider> Probe() {
            return Physics.OverlapSphere(this.transform.position, this.perceptionRange, this.sampleLayers).ToList().FindAll((x) => ColliderIsInField(x) && !ViewIsObstructed(x));
        }

        private bool ColliderIsInField(Collider collider){
            Vector3 directionToCollider = collider.transform.position - this.transform.position;
            return Vector3.Angle(this.transform.forward, directionToCollider) <= sightFieldAngle;
        }

        private bool ViewIsObstructed(Collider collider) {
            return Physics.Linecast(this.transform.position, collider.transform.position, obstructionMask);
        }
        
    }
}
