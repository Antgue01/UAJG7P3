using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadosMov : MonoBehaviour {

    MoveEnemy enemyMov;
    int punto = 0;

	void Start () {
        enemyMov = GetComponent<MoveEnemy>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (enemyMov.EstadoActual())
        {
            case Estados.persecucion:
                enemyMov.Persecucion();
                break;

            case Estados.patrulla:
                enemyMov.Patrulla(ref punto);
                break;

            case Estados.volviendo:
                enemyMov.Volviendo(ref punto);
                break;

            case Estados.finTrayecto:
                break;
        }
	}
}
