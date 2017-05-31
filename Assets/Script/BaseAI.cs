
public enum AIStatus
{
    Idle,
    Spawn,
    Follow,
    Attack,
    Damage,
    Die
}

public class BaseAI
{

    public AIStatus _Status;

    public virtual void UpdateAI()
    {
    }

    public void ChangeAIStatus(AIStatus toStatus)
    {
        switch (toStatus)
        {
            case AIStatus.Spawn:
                OnSpwan();
                break;
            case AIStatus.Idle:
                OnIdle();
                break;
            case AIStatus.Follow:
                OnFellow();
                break;
            case AIStatus.Attack:
                OnAttack();
                break;
            case AIStatus.Damage:
                OnDamage();
                break;
            case AIStatus.Die:
                OnDie();
                break;
        }
    }

    public virtual void OnSpwan() { }
    public virtual void OnIdle() { }
    public virtual void OnFellow() { }
    public virtual void OnAttack() { }
    public virtual void OnDamage() { }
    public virtual void OnDie() { }
}
