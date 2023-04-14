using System;
using System.Linq;
using MyBox;
using UnityDevKit.Events;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityDevKit.Scenarios.Tasks
{
    public class Task : MonoBehaviour
    {
        [Separator("Data")] 
        [SerializeField] private DataHolder data;

        [Separator("Inner tasks")] 
        [SerializeField] private bool anyForCompleted;
        [SerializeField] [InitializationField] private Task[] innerTasks;

        [Separator("Settings")] 
        [SerializeField] private bool isSingle = true;
        
        public EventHolderBase OnComplete { get; } = new EventHolderBase();
        
        private Task _parentTask;
        [SerializeField] [ReadOnly] private bool _isCompleted;

        [Serializable]
        public struct DataHolder
        {
            [TextArea] public string Name;
            [FormerlySerializedAs("Value")] [TextArea] public string Description;
        }

        private bool IsSingleCompleted => isSingle && _isCompleted;
        public DataHolder Data => data;
        public bool HasInnerTasks => innerTasks.Length > 0;
        private bool AreInnerTasksCompleted => !HasInnerTasks || 
                                               (anyForCompleted 
                                                   ? innerTasks.Any(task => task.IsCompleted)
                                                   : innerTasks.All(task => task.IsCompleted));
        public bool IsCompleted => _isCompleted && AreInnerTasksCompleted;

        protected virtual void Awake()
        {
            foreach (var innerTask in innerTasks)
            {
                innerTask.SetParentTask(this);
            }
        }

        private void SetParentTask(Task task)
        {
            _parentTask = task;
            OnComplete.AddListener(_parentTask.Complete);
        }

        public void Complete()
        {
            if (IsSingleCompleted)
            {
                return;
            }
            _isCompleted = AreInnerTasksCompleted;

            if (_isCompleted)
            {
                OnComplete.Invoke();
            }
        }
        
        public void AutoComplete()
        {
            if (IsSingleCompleted)
            {
                return;
            }
            _isCompleted = AreInnerTasksCompleted;
        }

        public virtual void Reset()
        {
            _isCompleted = false;
            foreach (var innerTask in innerTasks)
            {
                innerTask.Reset();
            }
        }
    }
}