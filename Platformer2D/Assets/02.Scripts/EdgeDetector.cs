using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetector : MonoBehaviour
{
    public float topX, topY, bottomX, bottomY;
    public bool topOn, bottomOn;
    private bool detectingFallingEdge;
    private bool detectingRisingEdge;
    private StateMachineManager _machineManager;
    private Rigidbody2D _rb;
    [SerializeField] private LayerMask _groundLayer;
    private void Awake()
    {
        _machineManager = GetComponent<StateMachineManager>();
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        topOn = Physics2D.OverlapCircle(new Vector2(transform.position.x + topX * _machineManager.direction,
                                                    transform.position.y + topY),
                                                    0.01f,
                                                    _groundLayer);
        bottomOn = Physics2D.OverlapCircle(new Vector2(transform.position.x + bottomX * _machineManager.direction,
                                                       transform.position.y + bottomY),
                                                       0.01f,
                                                       _groundLayer);

        // ¶³¾îÁú¶§ »ó½Â¿§Áö
        if (bottomOn &&
            topOn == false &&
            _rb.velocity.y < 0)
        {
            detectingRisingEdge = true;
        }
        else
        {
            detectingRisingEdge = false;
        }   

        // ¿Ã¶ó°¥¶§ ÇÏ°­¿§Áö
        if (bottomOn == false &&
            topOn &&
            _rb.velocity.y >0)
        {
            detectingFallingEdge = true;
        }
        else
        {
            detectingFallingEdge = false;
        }
    }
}
