using UnityEngine;
using System.Collections;

public class TestRotate : MonoBehaviour {

    public Transform MoveTran;
    public Transform StaticTran;

    public float RotateSpeed = 10.0f;

	// Update is called once per frame
	void Update () {
        MoveTran.RotateAround(StaticTran.position, Vector3.back, RotateSpeed * Time.deltaTime);
	}
}
