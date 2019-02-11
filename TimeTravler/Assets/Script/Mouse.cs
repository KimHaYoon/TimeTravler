using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouse : MonoBehaviour
{
    public Text storeTitleText;
    public StoreUI storeUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(touchPosition, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if(hit.collider != null)
            {
                Debug.Log(hit.collider.name);

                if (hit.collider.name == "EquipmentStore")
                {
                    storeTitleText.text = "장비";
                    storeTitleText.name = "장비";
                    storeUI.gameObject.SetActive(true);

                }

                else if (hit.collider.name == "AccessoriesStore")
                {
                    storeTitleText.text = "잡화";
                    storeTitleText.name = "잡화";
                    storeUI.gameObject.SetActive(true);
                }
            }
        }
        
    }
}
