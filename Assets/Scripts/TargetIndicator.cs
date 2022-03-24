using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour {

    public Transform ascensor;
    public Transform tarjeta;
    public GameObject flecha;
	
	
	// Update is called once per frame
	void Update () {
        //var dir = tarjeta.position - transform.position;
        //var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (GameManager.instance.CaosActual() >= 100 && !GameManager.instance.HasGanado())
        {
            flecha.SetActive(true);
            var dir1 = tarjeta.position - transform.position;
            var angle1 = Mathf.Atan2(dir1.y, dir1.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle1, Vector3.forward);
        }
        else if(GameManager.instance.CaosActual() >= 100 && GameManager.instance.HasGanado())
        {
            var dir2 = ascensor.position - transform.position;
            var angle2 = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle2, Vector3.forward);
        }
	}
}
