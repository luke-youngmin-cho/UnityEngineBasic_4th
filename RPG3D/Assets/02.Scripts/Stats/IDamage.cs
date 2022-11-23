using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    public GameObject Hitter { get; set; }
    public GameObject Hittee { get; set; }

    public void GetDamage(GameObject hitter, GameObject hittee, int damage, bool isCritical);
}
