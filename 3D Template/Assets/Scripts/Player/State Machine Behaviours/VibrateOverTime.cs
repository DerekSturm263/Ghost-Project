using UnityEngine;

public class VibrateOverTime : StateMachineBehaviour
{
    [SerializeField] private AnimationCurve _strengthOverTime;
    [SerializeField] private float _curveStrength;
    [SerializeField] private float _curveSpeed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PlayerController>().Player.StartVibrations();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float frequency = _strengthOverTime.Evaluate(Mathf.Repeat(stateInfo.normalizedTime * _curveSpeed, 1)) * _curveStrength;
        animator.GetComponent<PlayerController>().Player.VibrateOverTime(frequency);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PlayerController>().Player.EndVibrations();
    }
}
