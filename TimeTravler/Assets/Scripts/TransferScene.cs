using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;  // 이동할 맵의 이름.

    public Transform target;
    public BoxCollider2D targetBound;

    private Player thePlayer;
    private CameraManager theCamera;

    public bool flag;

    // Start is called before the first frame update
    void Start()
    {
        if(!flag)
            theCamera = FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<Player>();
        
        // FindObjectOfType<> -> 하이어라키에 있는 모든 객체의 <> 컴포넌트를 검색해서 리턴 // 다수의 객체
        // GetComponent<> -> 해당 스크립트가 적용된 객체의 <> 컴포넌트를 검색해서 리턴 ( 검색 범위의 차이로 이해하시면 됨.) // 단일의 객체
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            //thePlayer.currentMapName = transferMapName;
            theCamera.SetBound(targetBound);

            if(flag)
                SceneManager.LoadScene(transferMapName);
            else
            {
                thePlayer.transform.position = target.transform.position;
                theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
            }
        }
    }
}
