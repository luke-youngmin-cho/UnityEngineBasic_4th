using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingPlay : MonoBehaviour
{
    [SerializeField] private List<HorseMove> horses;
    [SerializeField] private Transform goalPoint;

    public void Play()
    {
        foreach (HorseMove horse in horses)
        {
            horse.StartMove(goalPoint.position.z - horse.transform.position.z);
        }
    }
}
