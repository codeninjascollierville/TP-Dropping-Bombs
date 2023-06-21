using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExplosion : MonoBehaviour
{
 [Header("Explosion Parts")]
 public GameObject explosion;

 private void OnCollisionEnter(Collision collision)
 {
    Instantiate(explosion, transform.position, transform.rotation);
 }
}
