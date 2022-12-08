using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using Alchemy.Core;

namespace Alchemy.AI {
    public class AlchemyAgent : AlchemyAvatar {
        private NavMeshAgent navMeshAgent;
        private List<Perceptor> perceptors;

        void OnDrawGizmos() {
            Gizmos.color = Color.yellow;

            if(navMeshAgent != null) {
                Gizmos.DrawSphere(navMeshAgent.destination, 0.2f);
            }
            
        }

        void Awake() {
            navMeshAgent = GetComponent<NavMeshAgent>();
            perceptors = GetComponentsInChildren<Perceptor>().ToList();
        }

        public void Attack(BehaviorTreeContext behaviorTreeContext) {
            ProjectileLauncher launcher = GetComponentInChildren<ProjectileLauncher>();
            launcher.aimPoint = GetPerceptionRecordWithComponent<AlchemyPlayer>().perceivedObjectPosition;
            launcher.isAimSet = true;
            launcher.GetComponent<PlayableDirector>().Play();
        }

        public List<PerceptionRecord> GetPerceptionRecordsWithComponent<T>() {
            List<PerceptionRecord> allMatches = new List<PerceptionRecord>();

            foreach(Perceptor perceptor in perceptors) {
                allMatches.AddRange(perceptor.FindPerceivedObjectsWithComponent<T>());
            }

            return allMatches;
        }

        public PerceptionRecord GetPerceptionRecordWithComponent<T>() {
            return GetPerceptionRecordsWithComponent<T>().First();
        }

        public List<T> GetPerceivedObjectsWithComponent<T>() {
            return GetPerceptionRecordsWithComponent<T>().Select((x) => x.perceivedObject.GetComponentInParent<T>()).ToList();
        }

        public T GetPerceivedObjectWithComponent<T>() {
            return GetPerceivedObjectsWithComponent<T>().First();
        }

        public NavMeshAgent GetNavMeshAgent() {
            return navMeshAgent;
        }
    }
}