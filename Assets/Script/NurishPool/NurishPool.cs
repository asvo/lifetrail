using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NurishPool : ISingleton<NurishPool> {

    public class NurishUnit
    {
        public bool IsActive { get; private set; }
        public GameObject NurishGobj { get; private set; }

        public void Init()
        {
            Object obj = Resources.Load("Prefab/Nurish");
            NurishGobj = GameObject.Instantiate(obj) as GameObject;
        }

        public GameObject ReActive()
        {
            IsActive = true;
            NurishGobj.SetActive(true);
            return NurishGobj;
        }

        public void DeActive()
        {
            NurishGobj.SetActive(false);
            IsActive = false;
        }
    }
    public List<NurishUnit> mNurishGobjCache = new List<NurishUnit>();

    public GameObject SpawnNurish()
    {
        NurishUnit inActiveUnit = FindFirstInActiveNurishUnit();
        if (null == inActiveUnit)
        {
            inActiveUnit = AddNewNurish();            
        }
        return inActiveUnit.ReActive();
    }

    private NurishUnit AddNewNurish()
    {
        NurishUnit nu = new NurishUnit();
        nu.Init();
        return nu;
    }

    private NurishUnit FindFirstInActiveNurishUnit()
    {
        NurishUnit findUnit = null;
        for(int i = 0; i < mNurishGobjCache.Count; ++i)
        {
            if (!mNurishGobjCache[i].IsActive)
            {
                findUnit = mNurishGobjCache[i];
            }
        }
        return findUnit;
    }

    public void DisspawnNurish(GameObject gobj)
    {
        for(int i = 0; i < mNurishGobjCache.Count; ++i)
        {
            if (mNurishGobjCache[i].NurishGobj == gobj)
            {
                mNurishGobjCache[i].DeActive();
            }
        }
    }
}
