using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;

    private bool _alive;

    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;

    [SerializeField] private float fireInterval = 1.0f; // seconds between shots
    private float _lastFireTime = 0f;
    [SerializeField] private float fireSpeed = 10f;

    void Start()
    {
        _alive = true;
    }

    void Update()
    {
        if (_alive)
        {
            //Muovi sempre in avanti
            transform.Translate(0, 0, speed * Time.deltaTime);

            // Ray che parte dal nemico e guarda avanti
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            //SphereCast per "vedere" ostacolo con volume
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitobject = hit.transform.gameObject;
                if (hitobject.GetComponent<PlayerCharacter>())
                {
                    // Fire at fixed intervals while the player is in sight
                    if (Time.time - _lastFireTime >= fireInterval)
                    {
                        GameObject fireball = Instantiate(fireballPrefab) as GameObject;
                        fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        fireball.transform.rotation = transform.rotation;

                        // If the prefab has a Rigidbody, give it forward velocity
                        Rigidbody rb = fireball.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.velocity = transform.forward * fireSpeed;
                        }

                        _lastFireTime = Time.time;
                        _fireball = fireball; // keep a reference if needed elsewhere
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}

/*Questo script implementa un comportamento AI semplice per un nemico che vaga nell'ambiente. 
Si muove costantemente in avanti e usa SphereCast per rilevare ostacoli. Quando rileva un ostacolo entro una certa distanza, 
ruota casualmente per evitarlo, simulando un movimento di "vagabondaggio" con evitamento di collisioni.
*/

