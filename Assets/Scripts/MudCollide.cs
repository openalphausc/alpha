using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudCollide : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float radius;

    private WiperControl wiperScript;
    private Transform wiperTransform;

    void Start() {
      maxHealth = 50f;
      health = maxHealth;
      radius = 1.5f;
      wiperScript = GameObject.Find("Wiper").GetComponent<WiperControl>();
      wiperTransform = GameObject.Find("Wiper").transform;
    }

    void Update() {
      float distanceFromWiper = Mathf.Abs(Vector3.Distance(wiperTransform.position, transform.position));
      // wiper is on top of mud
      if(distanceFromWiper < radius) {
        float speedInst = Mathf.Abs(Vector3.Distance(wiperTransform.position, wiperScript.targetPosition));
        // check if fluid particles are on top
        bool wet = false;
        var fluids = GameObject.FindGameObjectsWithTag("Fluid");
        foreach (var fluid in fluids) {
            float fluidDist = Mathf.Abs(Vector3.Distance(fluid.transform.position, transform.position));
            if(fluidDist < radius) {
              wet = true;
              break;
            }
        }
        // do things
        if(wet) {
          health -= speedInst;
        }
        else {
          health += speedInst;
        }
        transform.localScale = new Vector3(1.5f * health/maxHealth, 1.5f * health/maxHealth, transform.localScale.z);
      }

      if(health <= 0) Destroy(gameObject);
    }
}
