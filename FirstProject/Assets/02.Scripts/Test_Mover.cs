using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Mover : MonoBehaviour
{
    public Transform other;
    private Transform _transform;
    private Camera _camera;
    // Initialization 함수들 
    // 본 클래스를 컴포넌트로 가지는 게임오브젝트가 활성화되어있을때만 호출됨

    // 한번 호출
    // GameObject 가 활성화 되어있고, 해당 컴포넌트가 비활성화 되어있어도 호출됨.
    // 기존에 클래스에서 생성자로 사용해서 초기화해야하는 경우 , 
    // Monobehavior 는 Awake 에서 초기화를 함. (생성자 대용)
    private void Awake()
    {
        // 인스턴스 멤버변수 초기화 
        _transform = transform;

        // Test_Mover 컴포넌트를 가지는 게임오브젝트가 가지고있는 
        // Camera 컴포넌트를 반환하는 함수
        _camera = GetComponent<Camera>();
        _camera = this.gameObject.GetComponent<Camera>();
    }

    // 게임오브젝트가 활성화 될 때마다 호출
    private void OnEnable()
    {
        
    }

    // 한번 호출
    void Start()
    {
        // 다른 GameObject 의 컴포넌트 인스턴스 등 외부참조를 해서 초기화해야할 때 
        // Awake() 에서 다 초기화 된 후에 실행 되어야 할때.
    }

    // 물리연산시마다 호출되는 함수
    // Update()보다 많이 호출될 수도 있음. 
    // Update() 에서 물리연산을 하면 안되는 이유는 
    // 기기 성능마다 다르게 호출되기때문에 시간당 이동거리/ 속도 변화량에 영향을 끼친다.
    private void FixedUpdate()
    {
        transform.position += Vector3.up * Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 카메라이동 등에 사용
    private void LateUpdate()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + Vector3.up, Vector3.one);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.up, Vector3.one);
    }

    private void OnApplicationFocus(bool focus)
    {
        
    }
    private void OnApplicationPause(bool pause)
    {
        
    }

    private void OnApplicationQuit()
    {
        
    }

    // 게임오브젝트가 비활성화 될때마다 호출
    private void OnDisable()
    {
        
    }

    // 게임오브젝트가 파괴될때 호출
    private void OnDestroy()
    {
        
    }
}
