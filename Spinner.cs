using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float speedOfSpin = 0.1f;
 
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 0, speedOfSpin * Time.deltaTime);
    }
}
