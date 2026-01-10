using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHor = 9.0f; // Horizontal
    public float sensitivityVert = 9.0f; // Vertical
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotationX = 0;

    void Start()
    {
        //Aggancia il mouse al centro della finestra di gioco e nascondilo
        Cursor.lockState = CursorLockMode.Locked;
        
        // Bloccare la rotazione da fisica (se c’è Rigidbody)
        // In FPS reali non vuoi che la fisica faccia ruotare il player (tumble, rimbalzi ecc.).
        // Quindi congeli la rotazione del Rigidbody se presente.
        Rigidbody rig = GetComponent<Rigidbody>();
        if (rig != null)
        {
            rig.freezeRotation = true;
        }
    }

    void Update()
    {
        if (axes == RotationAxes.MouseX) 
        {
            // Horizontal Rotation Here
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
            // Input.GetAxis("Mouse X") restituisce un valore positivo/negativo in base allo spostamento del mouse.
        }
        else if (axes == RotationAxes.MouseY)
        {
            // Vertical Rotation Here
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);

            // _rotationX memorizza l’angolo verticale (pitch X).
            // Mathf.Clamp() blocca l’angolo tra min e max
            // transform.localEulerAngles richiede un Vector3 nuovo (non puoi modificare “un pezzo” del vettore perché quei valori sono read-only)
            // localRotation è un Quaternion (più adatto per interpolazioni fluide).
            // localEulerAngles è la versione “umana” XYZ.
            // Unity converte automaticamente tra i due.
        } 

        else
        {
            // Both 
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);

            // delta = “quanto cambia” (il termine matematico standard).
            // rotationY = angolo attuale + delta.
        }
    }
}

/*
SPIEGAZIONE GENERALE DELLO SCRIPT (COSA FA)

Questo script implementa il classico Mouse Look da FPS.

- Permette di ruotare un oggetto in base al movimento del mouse.
- Supporta tre modalità:
  1) Solo rotazione orizzontale (Mouse X → yaw)
  2) Solo rotazione verticale (Mouse Y → pitch, con limiti)
  3) Rotazione completa orizzontale + verticale

La rotazione verticale:
- viene accumulata manualmente (_rotationX)
- è limitata tramite Mathf.Clamp per evitare di guardare oltre un certo angolo
- viene applicata usando localEulerAngles perché Rotate() non consente limiti

La rotazione orizzontale:
- può usare Rotate() (quando è solo X)
- oppure viene calcolata manualmente quando X e Y sono combinati

Il Rigidbody (se presente) ha la rotazione congelata:
- per evitare che la fisica interferisca con il controllo del mouse
- comportamento tipico degli FPS moderni

In sintesi:
- Update() legge l’input del mouse
- converte l’input in variazioni di angolo
- applica le rotazioni in modo controllato
- separa chiaramente yaw (Y) e pitch (X)

Risultato finale:
- controllo visuale fluido
- limiti verticali corretti
- comportamento da FPS standard
*/
