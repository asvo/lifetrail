using UnityEngine;
using System.Collections;

public class Nurish : MonoBehaviour {

    //属性
    public float EatingRaidus = 64.0f;

    public Vector2 Position = Vector2.zero;
    public int Hp = 100;


    public AIStatus _Status;

	public void OnSpwan()
    {
        _Status = AIStatus.Spawn;
        InitNurish();
    }

    private void InitNurish()
    {
        Hp = MaxHp;
        Position = Vector2.zero;
        EatingRaidus = 64.0f;
    }

    private const int MaxHp = 100;
    private const float SmallestSacle = 0.6f;
    private float mCurrentScale = 1.0f;
    public void OnDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDie();
        }
        else
        {
            ShowOnHurt();
        }
    }

    /// <summary>
    /// 动画。表示受到攻击
    /// todo, 还可以加上受击动画、或者颜色变化之类的.
    /// </summary>
    private void ShowOnHurt()
    {
        mCurrentScale = 1.0f - (1.0f - SmallestSacle) / MaxHp * (MaxHp - Hp);
        Debug.LogError("On hurt. hp= " + Hp + ", scale= " + mCurrentScale);
        transform.localScale = Vector2.one * mCurrentScale;
    }

    public void OnDie()
    {
        _Status = AIStatus.Die;
        Debug.Log("Nurish died.");
    }

    public bool IsAlive
    {
        get
        {
            return _Status != AIStatus.Die;
        }
    }
}
