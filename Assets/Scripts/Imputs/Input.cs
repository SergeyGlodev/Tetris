
public abstract class Input
{
    public event System.Action rotateLeft;
    public event System.Action rotateRight;
    public event System.Action stepLeft;
    public event System.Action stepRight;
    public event System.Action stepDown;
    public event System.Action fall;
    public event System.Action OnPPressed;
    public event System.Action escape;

    public abstract void CheckInputsInUpdate();

    protected void OnRotateLeft()
    {
        rotateLeft?.Invoke();
    }
    protected void OnRotateRight()
    {
        rotateRight?.Invoke();
    }
    protected void OnStepLeft()
    {
        stepLeft?.Invoke();
    }
    protected void OnStepRight()
    {
        stepRight?.Invoke();
    }
    protected void OnStepDown()
    {
        stepDown?.Invoke();
    }
    protected void OnFall()
    {
        fall?.Invoke();
    }
    protected void OnP()
    {
        OnPPressed?.Invoke();
    }
    protected void OnEscape()
    {
        escape?.Invoke();
    }
}
