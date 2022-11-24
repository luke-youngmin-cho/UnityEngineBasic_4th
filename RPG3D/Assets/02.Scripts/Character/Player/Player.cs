using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Movement))]
public class Player : CharacterBase, IDamage
{
    public GameObject Hitter { get; set; }
    public GameObject Hittee { get; set; }

    public void GetDamage(GameObject hitter, GameObject hittee, int damage, bool isCritical)
    {
        Hitter = hitter;
        Hittee = hittee;
        Debug.Log("Try to damage");
        if (hitter.TryGetComponent(out IStats istatsHitter) &&
            hittee.TryGetComponent(out IStats istatsHittee))
        {
            istatsHittee.stats.StatDictionary[Stat.ID_HP].
                Modify(-istatsHitter.stats[Stat.ID_ATK].Value, StatModType.Flat);

            Debug.Log($"Player got damage {istatsHitter.stats[Stat.ID_ATK].Value}");
        }
    }

    protected override IStateMachine CreateStateMachine()
    {
        return new PlayerStateMachine(gameObject);
    }
}
