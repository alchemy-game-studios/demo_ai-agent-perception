using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Alchemy.Core;

namespace Alchemy.AI {
    public abstract class Perceptor : MonoBehaviour
    {
        public float perceptionRange = 1f;
        public float sampleInterval = 0.1f;

        public LayerMask sampleLayers;

        public List<PerceptionRecord> perceptionRecords = new List<PerceptionRecord>();

       

        public float latestSampleTimestamp = 0f;

        public UnityEvent<PerceptionRecord> onPerception;
        protected void Update() {
            StartCoroutine(SampleSceneRoutine());
        }

        private IEnumerator SampleSceneRoutine() {
            SampleScene();

            yield return new WaitForSeconds(sampleInterval);
        }

        private void SampleScene() {
            foreach(Collider collider in Probe()) {
                this.RegisterPerceptionEvent(CreatePerceptionRecord(collider));
            }

            latestSampleTimestamp = Time.time;
        }

        protected abstract List<Collider> Probe();

        protected PerceptionRecord CreatePerceptionRecord(Collider collider, string actionId = "Idle") {
            PerceptionRecord record = new PerceptionRecord();
           
            record.Initialize(collider.gameObject, this);
            record.perceivedActionId = actionId;

            return record;
        }

        public void RecordAgentAction(BehaviorTreeContext context) {
            this.RegisterPerceptionEvent(CreatePerceptionRecord(this.GetComponentInParent<AlchemyAgent>().GetComponentInChildren<Collider>(), context.signalId));
        }

        public void RecordPlayerAction(string action) {
            this.RegisterPerceptionEvent(CreatePerceptionRecord(AlchemyPlayer.instance.GetComponentInChildren<Collider>(), action));
        }

        public void RegisterPerceptionEvent(PerceptionRecord perceptionRecord) {
            AIGroup group = GetComponentInParent<AIGroup>();

            if(group != null) {
                group.SynchronizeGroup(perceptionRecord);
            } else {
                AddRecord(perceptionRecord);
            }

           
        }

        public void AddRecord(PerceptionRecord perceptionRecord) {
            int index = FindMatchingPerceptionRecordIndex(perceptionRecord);
            
            if(index > -1) {
                perceptionRecords[index] = perceptionRecord;
            } else {
                onPerception.Invoke(perceptionRecord);
                perceptionRecords.Add(perceptionRecord);
            }

           
        }

        public List<PerceptionRecord> FindPerceivedObjectsWithComponent<T>() {
            List<PerceptionRecord> matchingRecords = new List<PerceptionRecord>();

            foreach(PerceptionRecord record in perceptionRecords) {
                if(record.perceivedObject.GetComponentInParent<T>() != null) {
                    matchingRecords.Add(record);
                }
            }

            return matchingRecords;
        }

        public List<PerceptionRecord> FindPerceivedObjectsWithAction(string actionId) {
            return perceptionRecords.FindAll((x) => x.perceivedActionId == actionId);
        }

        private int FindMatchingPerceptionRecordIndex(PerceptionRecord perceptionRecord) {
            return perceptionRecords.FindIndex((x) => x.perceivedObject.GetInstanceID() == perceptionRecord.perceivedObject.GetInstanceID());
        }

        
    }
}
