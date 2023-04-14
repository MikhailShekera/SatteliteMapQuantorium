using UnityDevKit.Scenarios.Tasks;
using MyBox;
using UnityEngine;

namespace UnityDevKit.Scenarios
{
    public class ScenarioDisplaySubscription : MonoBehaviour
    {
        [Separator("Поле вывода")] [SerializeField]
        private Transform contentWindow;

        [Separator("Префабы")] [SerializeField]
        private GameObject taskDisplayPrefab;

        [SerializeField] private ScenarioDisplay scenarioDisplay;

        [Separator("Элементы управления")] [SerializeField]
        private GameObject scrollView;

        public void SetScenario(Scenario scenario)
        {
            scenarioDisplay.Init(scenario);

            scenario.OnComplete.AddListener(() => ScenarioStatusUpdate(scenario, scenarioDisplay));
            scenario.OnReset.AddListener(() => ScenarioStatusUpdate(scenario, scenarioDisplay));

            foreach (var singleTask in scenario.Tasks)
            {
                var taskDisplayObject = Instantiate(taskDisplayPrefab, contentWindow);

                taskDisplayObject.GetComponent<RectTransform>().sizeDelta =
                    new Vector2(scrollView.GetComponent<RectTransform>().rect.width - 20, 80);

                var taskDisplay = taskDisplayObject.GetComponentInChildren<TaskDisplay>();

                taskDisplay.Init(singleTask);

                singleTask.OnComplete.AddListener(() => TaskStatusUpdate(singleTask, taskDisplay));
                scenario.OnReset.AddListener(() => TaskStatusUpdate(singleTask, taskDisplay));
            }
        }

        #region Обновление сценариев и задач

        private void ScenarioStatusUpdate(Scenario scenarioHolder, ScenarioDisplay scenarioDisplay)
        {
            scenarioDisplay.StatusUpdate(scenarioHolder);
        }

        private void TaskStatusUpdate(Task taskHolder, TaskDisplay taskDisplay)
        {
            taskDisplay.StatusUpdate(taskHolder);
        }

        #endregion
    }
}