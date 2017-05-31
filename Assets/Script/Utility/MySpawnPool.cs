using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 自定义spawnpool
/// </summary>
public class MySpawnPool : ISingleton<MySpawnPool> {

    private Dictionary<string, SpawnData> mSpawnpool;

    public MySpawnPool()
    {
        mSpawnpool = new Dictionary<string, SpawnData>();
    }

    public GameObject Spawn(string spawnName)
    {
        SpawnData sp = GetSpawnData(spawnName);
        if (null == sp)
        {
            sp = new SpawnData(spawnName);
            sp.Spawn();
        }
        else
        {
            sp.Spawn();
        }
        return null;
    }

    private SpawnData GetSpawnData(string spawnName)
    {
        SpawnData findSd = null;
        mSpawnpool.TryGetValue(spawnName, out findSd);
        return findSd;
    }
}

public class SpawnData
{
    private string name;
    public List<SpawnUnit> spawnedList;

    public SpawnData(string name)
    {
    }

    public virtual GameObject Spawn()
    {
        return null;
    }
}

public class SpawnUnit
{
    public bool IsAlive;
    public GameObject SpawnInstance;
}
