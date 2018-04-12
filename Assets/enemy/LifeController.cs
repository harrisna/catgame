using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour {

    [SerializeField] private DeathController dc;

    public void Kill() {
        dc.Kill();
    }

    public void SetDC(DeathController dc) {
        this.dc = dc;
    }
}
