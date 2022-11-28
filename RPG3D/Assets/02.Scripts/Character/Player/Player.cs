using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : CharacterBase, IDamage
{
    public GameObject Hitter { get; set; }
    public GameObject Hittee { get; set; }

    public TargetCaster TargetCasterLeftHand;
    public TargetCaster TargetCasterRightHand;

    public void StartTargetCastLeftHand() => TargetCasterLeftHand.On = true;
    public void EndTargetCastLeftHand() => TargetCasterLeftHand.On = false;
    public void StartTargetCastRightHand() => TargetCasterRightHand.On = true;
    public void EndTargetCastRightHand() => TargetCasterRightHand.On = true;
    public IEnumerable<GameObject> GetTargetsCasted()
    {
        // Enumerable.Concat<> 
        // IEnumerable 끼리 concatenate 시키는 함수
        return TargetCasterLeftHand.GetTargets().Concat(TargetCasterRightHand.GetTargets());
    }

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
