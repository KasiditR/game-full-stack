using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private float powerTorque;
    [SerializeField] private float powerFaceup;
    private Rigidbody rb;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    [ContextMenu("Roll")]
    public void Roll()
    {
        this.transform.position = Vector3.zero;
        this.transform.rotation = Quaternion.identity;

        float torqueX = Random.Range(1f, powerTorque);
        float torqueY = Random.Range(1f, powerTorque);
        float torqueZ = Random.Range(1f, powerTorque);

        rb.AddForce(this.transform.up * powerFaceup);
        rb.AddTorque(new Vector3(torqueX, torqueY, torqueZ), ForceMode.Impulse);
    }
}
