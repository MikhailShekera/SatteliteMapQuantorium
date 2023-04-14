using System.Collections.Generic;
using UnityDevKit.Events;
using UnityEngine;

namespace UnityDevKit.Interactables.Spinnable.Discrete
{
    [RequireComponent(typeof(IDiscreteSpinnable))]
    public class DiscreteValueSetter : MonoBehaviour
    {
        public EventHolder<List<float>> OnValuesLoad { get; private set; }= new EventHolder<List<float>>();

        private IDiscreteSpinnable discreteSpinnable;

        private void Awake()
        {
            discreteSpinnable = GetComponent<IDiscreteSpinnable>();
        }

        public void SetDiscreteValues(List<float> values)
        {
            discreteSpinnable.SetDiscreteValues(values);
            OnValuesLoad.Invoke(values);
        }
    }
}