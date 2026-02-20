using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{

    public static SelectionManager Instance;
    public GameObject SelectedObject;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void SelectObject(GameObject obj)
    {
        if (obj != null)
        {
            if(SelectedObject != null)
            SelectedObject.GetComponent<Platform>().UnSelected();
            SelectedObject = null;
            SelectedObject = obj;
            SelectedObject.GetComponent<Platform>().Selected();
            UI_Manager.Instance.HideUpgradeUI();
        }
        else
        {
            if(SelectedObject != null)
            SelectedObject.GetComponent<Platform>().UnSelected();
            SelectedObject = null;
        }
    }

    public void BuildButton(int turretID)
    {
        if (SelectedObject != null)
        {
            if (SelectionManager.Instance.SelectedObject.CompareTag("Platform"))
            {
                SelectionManager.Instance.SelectedObject.GetComponent<Platform>().Build(turretID);
            }
            else
            {
                return;
            }
        }
    }

    public void UpgradeButton(int turretID)
    {
        if (SelectedObject != null)
        {
            if (SelectionManager.Instance.SelectedObject.CompareTag("Platform"))
            {             
                SelectionManager.Instance.SelectedObject.GetComponent<Platform>().Upgrade(turretID);
            }
            else
            {
                return;
            }
        }
    }

    public void Dismantle()
    {
        if (SelectedObject != null)
        {
            if (SelectionManager.Instance.SelectedObject.CompareTag("Platform"))
            {
                SelectionManager.Instance.SelectedObject.GetComponent<Platform>().Dismantle();
            }
            else
            {
                return;
            }
        }
    }
}
