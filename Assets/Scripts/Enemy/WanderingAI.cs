using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;

    void Update()
    {
        //Muovi sempre in avanti
        transform.Translate(0, 0, speed * Time.deltaTime);

        // Ray che parte dal nemico e guarda avanti
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //SphereCast per "vedere" ostacolo con volume
        if(Physics.SphereCast(ray, 0.75f, out hit))
        {
            if(hit.distance < obstacleRange)
            {
                if(hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }
}


/*Questo script implementa un comportamento AI semplice per un nemico che vaga nell'ambiente. 
Si muove costantemente in avanti e usa SphereCast per rilevare ostacoli. Quando rileva un ostacolo entro una certa distanza, 
ruota casualmente per evitarlo, simulando un movimento di "vagabondaggio" con evitamento di collisioni.
*/

