using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudCollide : MonoBehaviour
{
    public float health;
    public float radius;

    private WiperControl wiperScript;
    private Transform wiperTransform;

    void Start() {
      health = 50f;
      radius = 1.5f;
      wiperScript = GameObject.Find("Wiper").GetComponent<WiperControl>();
      wiperTransform = GameObject.Find("Wiper").transform;
    }

    void Update() {
      float distanceFromWiper = Mathf.Abs(Vector3.Distance(wiperTransform.position, transform.position));
      if(distanceFromWiper < radius) {
        float speedInst = Mathf.Abs(Vector3.Distance(wiperTransform.position, wiperScript.targetPosition));
        health -= speedInst;
      }

      if(health <= 0) Destroy(gameObject);
    }
}
