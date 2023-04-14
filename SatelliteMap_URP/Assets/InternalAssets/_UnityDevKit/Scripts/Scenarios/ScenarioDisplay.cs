using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnityDevKit.Scenarios
{
    public class ScenarioDisplay : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private TMP_Text scenarioName;
        [SerializeField] private TMP_Text scenarioDescription;

        public void Init(Scenario scenario)
        {
            scenarioName.text = scenario.Data.Name;
            scenarioDescription.text = scenario.Data.Description;
            toggle.isOn = scenario.IsCompleted;
        }

        public void StatusUpdate(Scenario scenario)
        {
            toggle.isOn = scenario.IsCompleted;
        }
    }
}