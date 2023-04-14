using System;
using System.Collections;
using MyBox;
using UnityEngine;

namespace UnityDevKit.Interactables.Buttons
{
    [Serializable]
    public abstract class TransitionVector
    {
        [InitializationField] public Vector3 moveDelta = new Vector3(0f, -0.5f, 0f);

        public Transform movablePart { get; private set; }
        public Vector3 startPosition { get; private set; }
        public Vector3 movedPosition { get; private set; }
        public float moveSpeed { get; private set; }
        public float moveEpsilon { get; private set; }

        public Coroutine currentCoroutine { get; set; }

        public void Setup(Transform newMovablePart, Vector3 newStartPosition, float transitionTime,
            float epsilonPercentage)
        {
            movablePart = newMovablePart;
            startPosition = newStartPosition;
            movedPosition = startPosition + moveDelta;

            var moveDistance = Vector3.Distance(startPosition, movedPosition);
            moveSpeed = moveDistance / transitionTime;

            moveEpsilon = moveDistance * epsilonPercentage;
        }

        public abstract IEnumerator MoveProcess(Vector3 target);
    }

    [Serializable]
    public class TransitionMoveVector : TransitionVector
    {
        public override IEnumerator MoveProcess(Vector3 target)
        {
            while (Vector3.Distance(movablePart.localPosition, target) > moveEpsilon)
            {
                movablePart.localPosition = Vector3.MoveTowards(movablePart.localPosition, target,
                    moveSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            movablePart.localPosition = target;
        }
    }

    [Serializable]
    public class TransitionRotationVector : TransitionVector
    {
        public override IEnumerator MoveProcess(Vector3 target)
        {
            while (Vector3.Distance(movablePart.localEulerAngles, target) > moveEpsilon)
            {
                movablePart.localEulerAngles = Vector3.MoveTowards(movablePart.localEulerAngles, target,
                    moveSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            movablePart.localEulerAngles = target;
        }
    }
}