using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class SimpleADSR
{
    [Serializable]
    public struct PhaseData
    {
        public float duration;
        public float targetValue;
    }

    [Serializable]
    public struct FlatPhaseData
    {
        public float duration;
    }

    public PhaseData attack;
    public PhaseData decay;
    public FlatPhaseData sustain;
    public FlatPhaseData release;

    public float Evaluate(float time)
    {
        float attackStep = Mathf.Clamp01(time / attack.duration);
        float decayStep = Mathf.Clamp01((time -= attack.duration) / decay.duration);
        float sustainStep = Mathf.Clamp01((time -= decay.duration) / sustain.duration);
        float releaseStep = Mathf.Clamp01((time -= sustain.duration) / release.duration);

        DebugManager.Instance.PushDebugText($"attackStep: {attackStep}");
        DebugManager.Instance.PushDebugText($"decayStep: {decayStep}");
        DebugManager.Instance.PushDebugText($"sustainStep: {sustainStep}");
        DebugManager.Instance.PushDebugText($"releaseStep: {releaseStep}");

        if (attackStep < 1)
        {
            DebugManager.Instance.PushDebugText($"attack");
            return Mathf.Lerp(0, attack.targetValue, attackStep);
        }
        else if (decayStep < 1)
        {
            DebugManager.Instance.PushDebugText($"decay");
            return Mathf.Lerp(attack.targetValue, decay.targetValue, decayStep);
        }
        else if (sustainStep < 1)
        {
            DebugManager.Instance.PushDebugText($"sustain");
            return decay.duration == 0 ? attack.targetValue : decay.targetValue;
        }
        else
        {
            DebugManager.Instance.PushDebugText($"release");
            return Mathf.Lerp(decay.duration == 0 ? attack.targetValue : decay.targetValue, 0, releaseStep);
        }
    }
}