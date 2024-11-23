using System;
using UnityEngine;

[Serializable]
public struct FightSettings
{
    [SerializeField] private string _parameterName;

    public readonly void Evaluate(Animator animator)
    {
        animator.SetTrigger(_parameterName);
    }
}
