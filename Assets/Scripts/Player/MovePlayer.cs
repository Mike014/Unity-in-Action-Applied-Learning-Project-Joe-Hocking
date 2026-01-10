using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/MovePlayer")]

// RequireComponent → obbliga Unity ad avere quel componente sul GameObject
// AddComponentMenu → mette lo script nel menu Add Component

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    public float speed = 6.0f;
    public float gravity = -9.8f;

    private CharacterController _charController;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(0, speed, 0); // Translate cambia posizione
        //                  (x, y, z)

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement = transform.TransformDirection(movement); // converte il vettore orizzontale in world space

        movement.y = gravity;           // gravità in world (giù)
        movement *= Time.deltaTime;

        _charController.Move(movement);

        // GetComponent<CharacterController>() prende il componente già attaccato al player
        // Vector3.ClampMagnitude evita che la diagonale (W+D) sia più veloce (ipotenusa)
        // TransformDirection converte il vettore da local space a world space (perché “avanti” del player dipende da dove guarda)

        // transform.Translate(deltaX * Time.deltaTime, 0, deltaZ * Time.deltaTime);

        // A/D e frecce sinistra/destra → Horizontal
        // W/S e frecce su/giù → Vertical
        // usi X e Z perché:
        // X = laterale
        // Z = avanti/indietro
        // Time.deltaTime = tempo trascorso tra un frame e l’altro.
    }
}

// MouseLook → guardi intorno (yaw/pitch)
// FPSInput + CharacterController → ti muovi con WASD e non attraversi i muri
// deltaTime → stessa velocità su ogni PC
// gravity → cammini invece di volare (se setti correttamente yaw su Player e pitch su Camera)
