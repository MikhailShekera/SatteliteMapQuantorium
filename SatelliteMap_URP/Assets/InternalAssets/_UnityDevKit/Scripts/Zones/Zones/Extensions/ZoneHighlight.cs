using UnityDevKit.Effects.Highlight;
using UnityEngine;

namespace UnityDevKit.Zones.Zones.Extensions
{
    public class ZoneHighlight : ZoneExtension
    {
        [SerializeField] private HighlightEffect highlightEffect;
        
        protected override void OnZoneEnter()
        {
            highlightEffect.Remove();
        }

        protected override void OnZoneExit()
        {
           highlightEffect.Apply();
        }
    }
}