using System;
using System.Linq;
using MyBox;
using UnityDevKit.Events;
using UnityDevKit.Scenarios.Tasks;
using UnityEngine;

namespace UnityDevKit.Scenarios
{
    public class Scenario : MonoBehaviour
    {
        [Separator("Data")] 
        [SerializeField] private DataHolder data;
        [SerializeField] [InitializationField] private Task[] tasks;

        [Separator("Settings")] 
        [SerializeField] private bool isResettable = true;
        
        [SerializeField] [ReadOnly] private bool _isCompleted;
        [SerializeField] [ReadOnly] private bool _wasCompleted;
        
        public EventHolderBase OnComplete { get; } = new EventHolderBase();
        public EventHolderBase OnReset { get; } = new EventHolderBase();
        
        [Serializable]
        public struct DataHolder
        {
            [TextArea] public string Name;
            [TextArea] public string Description;
        }
        
        public DataHolder Data => data;
        public Task[] Tasks => tasks;
        public bool IsCompleted => _isCompleted;
        public bool WasCompleted => _wasCompleted;

        private void Awake()
        {
            foreach (var innerTask in tasks)
            {
                innerTask.OnComplete.AddListener(Complete);
            }
        }
        
        private void Complete()
        {
            if (_isCompleted)
            {
                return;
            }
            _isCompleted = tasks.All(task => task.IsCompleted);
            
            if (_isCompleted)
            {
                _wasCompleted = true;
                OnComplete.Invoke();

                if (isResettable)
                {
                    Reset();
                }
            }
        }

        public void Reset()
        {
            _isCompleted = false;
            foreach (var task in tasks)
            {
                task.Reset();
            }
            OnReset.Invoke();
        }
    }
}