using System.Collections;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        // recupera il componente Camera (come già visto col CharacterController)
        _camera = GetComponent<Camera>();
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    void Update()
    {
        /*
        controlla continuamente l’input
        spara solo quando il mouse viene cliccato
        Centro dello schermo:
        - pixelWidth / 2
        - pixelHeight / 2
        Physics.Raycast:
        - True se colpisce qualcosa
        out permette alla funzione di riempire una struttura esterna, senza out, i dati andrebbero persi, è il modo standard in C# per restituire più informazioni
        */
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject hitObject = hit.transform.gameObject;
                    ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                    if (target != null)
                    {
                        target.ReactToHit();
                    }
                    else
                    {
                        StartCoroutine(SphereIndicator(hit.point));
                        Debug.Log("Hit " + hit.point);
                    }
                }
                // StartCoroutine(SphereIndicator(hit.point));
                // Debug.Log("Hit " + hit.point);
                /*
                Cosa fa questa Coroutine: GUARDA SphereIndicator
                1. Crea una sfera nel punto colpito
                2. Aspetta 1 secondo
                3. Distrugge la sfera
                Il yield:
                - sospende la funzione
                - restituisce il controllo al motore
                - riprende dal punto esatto dopo il tempo indicato
                */

            }
        }
    }

    void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.color = Color.red;
        GUI.Label(new Rect(posX, posY, size, size), "*"); // Questo comando mostra text al centro dello schermo
        GUI.color = Color.white; // reset (buona pratica)
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere =
          GameObject.CreatePrimitive(PrimitiveType.Sphere);

        sphere.transform.position = pos;
        sphere.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }

}
