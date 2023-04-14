using System;
using UnityEngine;
using UnityDevKit.Events;
using UnityDevKit.Patterns;

namespace UnityDevKit.Utils.TimeHandlers
{
	public class TimeManager : Singleton<TimeManager>, IClock
	{
		[SerializeField] private TimeMode startMode = TimeMode.X1;
		
		public TimeMode Mode { get; private set; }

		public EventHolder<TimeMode> OnTimeModeChanged { get; private set; } = new EventHolder<TimeMode>();

		private TimeMode lastMode;

		private Clock clock = new Clock();
		
		public enum TimeMode
		{
			Pause,
			X05,
			X1,
			X2,
			X4
		}

		public override void Awake()
		{
			base.Awake();
			SetTimeMode(startMode);
			lastMode = Mode;
		}

		public void SetTimeMode(TimeMode mode)
		{
			switch (mode)
			{
				case TimeMode.Pause:
					Time.timeScale = 0f;
					break;
				case TimeMode.X05:
					Time.timeScale = 0.5f;
					break;
				case TimeMode.X1:
					Time.timeScale = 1f;
					break;
				case TimeMode.X2:
					Time.timeScale = 2f;
					break;
				case TimeMode.X4:
					Time.timeScale = 4f;
					break;
			}
			Debug.Log("Time scale changed to " + Time.timeScale);
			Mode = mode;
			OnTimeModeChanged.Invoke(Mode);
		}

		public void SpeedUp()
		{
			switch (Mode)
			{
				case TimeMode.X05:
					SetTimeMode(TimeMode.X1);
					break;
				case TimeMode.X1:
					SetTimeMode(TimeMode.X2);
					break;
				case TimeMode.X2:
					SetTimeMode(TimeMode.X4);
					break;
				case TimeMode.X4:
					break;
				case TimeMode.Pause:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void SpeedUpLoop()
		{
			switch (Mode)
			{
				case TimeMode.X05:
					SetTimeMode(TimeMode.X1);
					break;
				case TimeMode.X1:
					SetTimeMode(TimeMode.X2);
					break;
				case TimeMode.X2:
					SetTimeMode(TimeMode.X4);
					break;
				case TimeMode.X4:
					SetTimeMode(TimeMode.X05);
					break;
				case TimeMode.Pause:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void Launch()
		{
			clock.Launch();
		}

		public Clock.Data Stop()
		{
			return clock.Stop();
		}

		public void Pause()
		{
			lastMode = Mode;
			SetTimeMode(TimeMode.Pause);
			clock.Pause();
		}

		public void Resume()
		{
			SetTimeMode(lastMode);
			clock.Resume();
		}
	}
}