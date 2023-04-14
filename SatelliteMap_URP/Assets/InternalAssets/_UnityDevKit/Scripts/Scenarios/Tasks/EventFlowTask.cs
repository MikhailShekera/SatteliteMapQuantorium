using MyBox;
using UnityDevKit.Events;
using UnityEngine;

namespace UnityDevKit.Scenarios.Tasks
{
    public class EventFlowTask : Task
    {
        [Separator("Trigger")]
        [SerializeField] private EventFlow eventFlow;

        private void Start()
        {
            eventFlow.AddListener(Complete);
        }
    }
}