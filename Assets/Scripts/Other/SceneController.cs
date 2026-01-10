using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // visibile nell’Inspector, non modificabile da altri script
    private GameObject _enemy;

    // Update is called once per frame
    void Update()
    {
        // Se non esiste un nemico nella scena creane uno
        if(_enemy == null) //Quando il prefab non è in scena
        {
            _enemy = Instantiate(enemyPrefab) as GameObject;
            
            float spawnX = Random.Range(-10f, 10f);
            float spawnY = 29;
            float spawnYZ = Random.Range(-10f, 10f);
            _enemy.transform.position = new Vector3(spawnX, spawnY, spawnYZ);

            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);
        }
    }
}

/*
1. _enemy è null
2. Instantiate(enemyPrefab)
3. _enemy ora punta all’istanza
4. Il nemico muore → Destroy()
5. _enemy diventa null
6. SceneController spawna un nuovo nemico
*/
