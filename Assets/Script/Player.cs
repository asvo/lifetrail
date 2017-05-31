using UnityEngine;
using System.Collections;

/// <summary>
/// v0.1 玩家角色脚本
/// </summary>
public class Player : MonoBehaviour {
    
	public enum PlayerStatus
    {
        Spwan,
        Idle,
        Follow,
        Eat,
        Die
    }

    public PlayerStatus _Status;
    private Nurish mTarget;

    private bool mIsInLerpMove = false;
    private Vector2 mLerpStartPos;
    private Vector2 mLerpEndPos;
    public float mMoveSpeed = 20f;
    private float mNeedTotalMoveTime = 5f;
    private float mCurPassedLerpTime;

    void Update()
    {
        if (mIsInLerpMove)
        {
            UpdateLerpMove();
        }
        if (_Status == PlayerStatus.Eat)
        {
            UpdateEating();
        }
    }
    
    public float EatingAngleSpeed = 30.0f;
    private void UpdateEating()
    {
        if (!mTarget.IsAlive)
        {
            OnIdle();
        }
        transform.RotateAround(mTarget.Position, Vector3.back, EatingAngleSpeed * Time.deltaTime);
    }

    private void UpdateLerpMove()
    {
        mCurPassedLerpTime += Time.deltaTime;
        float t = mCurPassedLerpTime / mNeedTotalMoveTime;
        bool isLerpMoveEnd = t >= 1f;
        t = Mathf.Clamp(t, 0f, 1f);
        Vector2 lerpPos = Vector2.Lerp(mLerpStartPos, mLerpEndPos, t);
        if (!isLerpMoveEnd)
        {
            transform.position = lerpPos;
        }
        else
        {
            transform.position = mLerpEndPos;
            OnLerpMoveEnd();
        }
    }

    private void OnLerpMoveEnd()
    {
        mIsInLerpMove = false;
        if (_Status == PlayerStatus.Follow)
        {
            //change state to eat
            OnEat();
        }
    }
    public void OnSpwan()
    {
        _Status = PlayerStatus.Spwan;
        //play spawn audio
        //...
        OnIdle();
    }

    private void OnIdle()
    {
        _Status = PlayerStatus.Idle;
        GameObject targetGobj = FindNearestTarget();
        if (null != targetGobj)
            OnFollow(targetGobj);
    }

    public void OnFollow(GameObject gobj)
    {
        mTarget = gobj.GetComponent<Nurish>();
        //todo, 这里找到了目标还可以加一个找到目标的提示动画. 或者hint什么的
        //...
        Vector3 startPos = CalculateEatingStartPos(mTarget.Position, mTarget.EatingRaidus);
        mLerpStartPos = new Vector2(transform.position.x, transform.position.y);
        mLerpEndPos = new Vector2(startPos.x, startPos.y);
        Debug.LogError("startLerpPos = " + mLerpStartPos);
        Debug.LogError("endLerpPos = " + mLerpEndPos);
        float dist = Vector2.Distance(mLerpStartPos, mLerpEndPos);
        mNeedTotalMoveTime = dist / mMoveSpeed;
        _Status = PlayerStatus.Follow;
        mIsInLerpMove = true;
        mCurPassedLerpTime = 0f;
    }

    private Vector2 CalculateEatingStartPos(Vector2 nurishPos, float nurishRadius)
    {
        return nurishPos + Vector2.up * nurishRadius;
    }

    //todo,asvo . 这个目前就一个，指定
    private GameObject FindNearestTarget()
    {
        Transform rootTrans = GameCtrl.Instance.SpawnRoot;
        for (int i = 0; i < rootTrans.childCount; ++i)
        {
            Transform childTrans = rootTrans.GetChild(i);
            Nurish nurish = childTrans.GetComponent<Nurish>();
            if (null != nurish)
            {
                return nurish.gameObject;
            }
        }
        return null;
    }

    private void OnEat()
    {
        _Status = PlayerStatus.Eat;
        InvokeRepeating("InvokeOnEating", 1f, 1f);
    }

    const int EatingPetSec = 10;

    private void InvokeOnEating()
    {
        if (_Status != PlayerStatus.Eat)
        {
            CancelInvoke("InvokeOnEating");
            return;
        }
        if (null != mTarget && mTarget.IsAlive)
        {
            mTarget.OnDamage(EatingPetSec);
        }
    }
}
