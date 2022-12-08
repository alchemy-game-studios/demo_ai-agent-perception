using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Alchemy.AI {
    public class AIGroup : MonoBehaviour
    {
        public void SynchronizeGroup(PerceptionRecord perceptionRecord)  {
            List<Perceptor> perceptors = GetComponentsInChildren<Perceptor>().ToList();

            foreach(Perceptor perceptor in perceptors) {
                perceptor.AddRecord(perceptionRecord);
            }
        }
    }
}
