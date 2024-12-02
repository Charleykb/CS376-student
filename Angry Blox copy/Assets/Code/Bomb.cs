using UnityEngine;

public class Bomb : MonoBehaviour {
    public float ThresholdImpulse = 5;
    public GameObject ExplosionPrefab;


void Destruct() {
    Destroy(gameObject);
}

void Boom() {
    GetComponent<PointEffector2D>().enabled = true;
    GetComponent<SpriteRenderer>().enabled = false;
    Instantiate(ExplosionPrefab, transform.position, Quaternion.identity, transform.parent);
    Invoke("Destruct", 0.1f);
}

void OnCollisionEnter2D(Collision2D c) {
    foreach (ContactPoint2D cp in c.contacts) {
        if (cp.normalImpulse >= ThresholdImpulse) {
            Boom();
            break;
        }
    }
}

}