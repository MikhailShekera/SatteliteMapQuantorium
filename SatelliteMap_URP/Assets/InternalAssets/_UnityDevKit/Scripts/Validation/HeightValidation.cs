using MyBox;
using UnityEngine;

namespace UnityDevKit.Validations
{
    public class HeightValidation : TransformValidation
    {
        [SerializeField] private RangedFloat validBounds = new(1.2f, 1.7f);

        public override bool IsValid(Transform t)
        {
            var yPosition = t.position.y;
            return yPosition >= validBounds.Min && yPosition <= validBounds.Max;
        }
    }
}