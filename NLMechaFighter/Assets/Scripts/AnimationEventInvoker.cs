using UnityEngine;
using UnityEngine.Events;

public class AnimationEventInvoker : MonoBehaviour
{
    public UnityEvent OnStep = new UnityEvent();
    
    private void Step()
    {
        OnStep.Invoke();
    }
}
