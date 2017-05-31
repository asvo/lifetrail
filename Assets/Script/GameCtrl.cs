using UnityEngine;
using System.Collections;

public class GameCtrl : MonoBehaviour {

    public Transform SpawnRoot;

    private static GameCtrl _Instance;
    public static GameCtrl Instance
    {
        get
        {
            if (null == _Instance)
            {
                GameObject gobj = GameObject.Find("Canvas");
                _Instance = gobj.GetComponent<GameCtrl>();
                if (null == _Instance)
                {
                    _Instance = gobj.AddComponent<GameCtrl>();
                }
            }
            return _Instance;
        }
    }

	void OnGUI()
    {
        if (GUILayout.Button("Game Start"))
        {            
            GameObject gobj = SpawnGobj("Prefab/Nurish", SpawnRoot, Vector3.zero);
            Nurish nurish = gobj.GetComponent<Nurish>();
            nurish.OnSpwan();

            gobj = SpawnGobj("Prefab/Player", SpawnRoot, new Vector3(-151.3f, 5f, 0f));
            Player p = gobj.GetComponent<Player>();
            p.OnSpwan();
        }
    }

    private GameObject SpawnGobj(string path, Transform parent, Vector3 pos)
    {
        Object obj = Resources.Load(path);
        GameObject inst = GameObject.Instantiate(obj) as GameObject;
        inst.transform.SetParent(parent);
        inst.transform.localScale = Vector2.one;
        inst.transform.localPosition = pos;
        return inst;
    }
}
