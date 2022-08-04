using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHitter : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        // Bad 판정 범위
        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(transform.position,
                            new Vector3(transform.lossyScale.x / 2,
                                        Constants.HIT_JUDGE_RANGE_BAD,
                                        0));

        // Miss 판정 범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position,
                            new Vector3(transform.lossyScale.x / 2,
                                        Constants.HIT_JUDGE_RANGE_MISS,
                                        0));

        // Good 판정 범위
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position,
                            new Vector3(transform.lossyScale.x / 2,
                                        Constants.HIT_JUDGE_RANGE_GOOD,
                                        0));

        // Great 판정 범위
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,
                            new Vector3(transform.lossyScale.x / 2,
                                        Constants.HIT_JUDGE_RANGE_GREAT,
                                        0));

        // Cool 판정 범위
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position,
                            new Vector3(transform.lossyScale.x / 2,
                                        Constants.HIT_JUDGE_RANGE_COOL,
                                        0));
    }
}
