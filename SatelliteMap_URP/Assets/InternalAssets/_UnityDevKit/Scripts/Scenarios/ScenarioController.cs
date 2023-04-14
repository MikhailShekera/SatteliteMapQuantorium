using MyBox;
using UnityDevKit.Events;
using UnityEngine;

namespace UnityDevKit.Scenarios
{
    public abstract class ScenarioController : MonoBehaviour
    {
        [Separator("Start settings")] 
        [SerializeField] [InitializationField] 
        private bool activateFromStart;
        [SerializeField] [InitializationField] [ConditionalField(nameof(activateFromStart))] 
        private Scenario startScenario;
        
        [Separator("Events")]
        [SerializeField] private EventFlow onScenarioComplete;
        
        public Scenario CurrentScenario { get; protected set; }

        protected virtual void Awake()
        {
            if (activateFromStart)
            {
                CurrentScenario = startScenario;
                ActivateCurrentScenario();
            }
        }

        protected void ActivateCurrentScenario()
        {
            CurrentScenario.OnComplete.AddListener(OnCurrentScenarioComplete);
        }
        
        protected void DeactivateCurrentScenario()
        {
            if (CurrentScenario != null)
            {
                CurrentScenario.OnComplete.RemoveListener(OnCurrentScenarioComplete);
            }
        }

        protected virtual void OnCurrentScenarioComplete()
        {
            onScenarioComplete.Invoke();
        }
    }
}