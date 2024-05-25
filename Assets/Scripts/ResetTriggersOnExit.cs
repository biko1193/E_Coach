using UnityEngine;

public class ResetTriggersOnExit : StateMachineBehaviour
{
    public string[] triggerNames; // �缳���� Ʈ���� �̸���
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var triggerName in triggerNames)
        {
            animator.ResetTrigger(triggerName);
        }
    }
}
