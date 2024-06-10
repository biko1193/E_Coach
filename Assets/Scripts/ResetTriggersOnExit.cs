using UnityEngine;

public class ResetTriggersOnExit : StateMachineBehaviour
{
    public string[] triggerNames; // 재설정할 트리거 이름들
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var triggerName in triggerNames)
        {
            animator.ResetTrigger(triggerName);
        }
    }
}
