using System;
using MyBox;
using UnityEngine;

namespace UnityDevKit.Optimization
{
    [Serializable]
    public class DynamicFrameFilter
    {
        [SerializeField] private int minFrameFilter = 1;

        [SerializeField] private int maxFrameFilter = 30;

        [ReadOnly] public int currentFrameFilter = 1;

        private const int DEFAULT_FRAME_STEP = 1;


        public bool IsFilteredFrame() => Time.frameCount % currentFrameFilter == 0;

        public void Increase(int frameStep = DEFAULT_FRAME_STEP)
        {
            currentFrameFilter = Mathf.Min(currentFrameFilter + frameStep, maxFrameFilter);
        }

        public void Decrease(int frameStep = DEFAULT_FRAME_STEP)
        {
            currentFrameFilter = Mathf.Max(currentFrameFilter - frameStep, minFrameFilter);
        }

        public void SetToMax()
        {
            currentFrameFilter = maxFrameFilter;
        }

        public void SetToMin()
        {
            currentFrameFilter = minFrameFilter;
        }

        private enum StartFrameFilterMode // TODO
        {
            MIN,
            AVERAGE,
            MAX
        }
    }
}