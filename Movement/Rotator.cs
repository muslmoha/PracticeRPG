using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1.6f;
    private float rotation = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 q = gameObject.transform.rotation.eulerAngles;
        float y = q.y + rotationSpeed;
        q = new Vector3(q.x, y, q.z);

        if (y > 360) y = 0;

        gameObject.transform.rotation = Quaternion.Euler(q);
    }
}
