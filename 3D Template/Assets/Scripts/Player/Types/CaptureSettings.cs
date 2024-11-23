using System;
using UnityEngine;

[Serializable]
public struct CaptureSettings
{
    [SerializeField] private string _parameterName;

    public readonly void Evaluate(Animator animator, bool isCapturing)
    {
        animator.SetBool(_parameterName, isCapturing);
    }
}
