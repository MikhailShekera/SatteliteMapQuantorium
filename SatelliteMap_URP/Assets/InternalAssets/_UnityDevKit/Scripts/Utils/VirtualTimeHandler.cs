using System;
using UnityDevKit.Events;
using UnityDevKit.Patterns;

namespace UnityDevKit.Utils
{
    public class VirtualTimeHandler : Singleton<VirtualTimeHandler>
    {
        private DateTime _startDateTime;
        private DateTime _rootDateTime;

        public EventHolderBase OnTimeChange { get; } = new EventHolderBase(); 
        
        public override void Awake()
        {
            base.Awake();
            ResetStartTime();
            _rootDateTime = _startDateTime;
            SetRootTime(new TimeSpan(8, 0, 0));
        }
        
        public DateTime CurrentDateTime => _rootDateTime + (DateTime.Now - _startDateTime);
        
        public TimeSpan CurrentTime => CurrentDateTime.TimeOfDay;

        public void SetRootDateTime(DateTime dateTime)
        {
            ResetStartTime();
            _rootDateTime = dateTime;
            OnTimeChange.Invoke();
        }

        public void SetRootTime(TimeSpan timeSpan)
        {
            ResetStartTime();
            _rootDateTime = new DateTime(
                _rootDateTime.Year,
                _rootDateTime.Month,
                _rootDateTime.Day,
                timeSpan.Hours,
                timeSpan.Minutes,
                timeSpan.Seconds,
                timeSpan.Milliseconds);
            OnTimeChange.Invoke();
        }

        private void ResetStartTime()
        {
            _startDateTime = DateTime.Now;
        }
    }
}