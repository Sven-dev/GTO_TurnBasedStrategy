using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

    public float Speed;

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.W))
        {
            Move(Vector3.forward);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Move(Vector3.left);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Move(Vector3.back);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Move(Vector3.right);
        }
    }
    void Move(Vector3 dir)
    {
        transform.Translate(dir * Speed * Time.deltaTime);
    }
}
