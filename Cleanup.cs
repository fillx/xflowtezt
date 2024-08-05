using TMPro.SpriteAssetUtilities;
using UnityEngine;

namespace Cleanup
{
    internal class Cleanup
    {
        private const double TargetChangeTime = 1;
        private double _previousTargetSetTime;

        private object _lockedTarget;
        private object _target;
        private object _previousTarget;
        private object _activeTarget;
        private object _targetInRangeContainer;


        public void CleanupTest(TexturePacker_JsonArray.Frame frame)
        {
            if (IsValidTarget(_lockedTarget) == false) _lockedTarget = null;
            
            _activeTarget = TrySetActiveTargetFromQuantum(frame);

            // Проверка текущей цели
            if (!IsValidTarget(_target) || Time.time - _previousTargetSetTime >= TargetChangeTime)
            {
                if (IsValidTarget(_lockedTarget))
                {
                    _target = _lockedTarget;
                }
                else if (IsValidTarget(_activeTarget))
                {
                    _target = _activeTarget;
                }
                else
                {
                    var targetFromContainer = _targetInRangeContainer.GetTarget();
                    if (IsValidTarget(targetFromContainer))
                    {
                        _target = targetFromContainer;
                    }
                    else
                    {
                        _target = null;
                    }
                }
            }
            
            if (_previousTarget != _target)
            {
                _previousTargetSetTime = Time.time;
            }

            _previousTarget = _target;
            TargetableEntity.Selected = _target;
        }

        private bool IsValidTarget(object target)
        {
            return target != null && target.CanBeTarget;
        }
    }
}