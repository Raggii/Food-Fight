using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Canvas shopCanvas;
    public shopItems[] items;
    public Image[] Images;

    private bool inStore = false;
    private PlayerController playerCol;
    private BankAccountManager account;

    private void OnTriggerStay2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !inStore)
        {
            EnterStore();
            playerCol = otherCollider.GetComponent<PlayerController>();
            account = otherCollider.GetComponent<BankAccountManager>();
            playerCol.DeactivatePlayerControls();
        }
    }


    private void Start()
    {
        for(int i = 0; i<items.Length; i++)
        {
            Images[i].sprite = items[i].spriteAsset;
        }
    }


    public void PurchaseItem(int index)
    {
        if (account.CanWithdraw(items[index].cost))
        {
            account.Withdraw(items[index].cost);
            Debug.Log("Withdrawn " + items[index].cost);
        }else
        {
            Debug.Log("BROKE");
        }
    }


    public void EnterStore()
    {
        inStore = true;
        ShowMenu();
    }


    public void ExitStore()
    {
        inStore = false;
        HideMenu();
        if (playerCol != null)
        {
            playerCol.ActivatePlayerControls();
            playerCol = null;
        }

        if (account != null)
        {
            account = null;
        }

    }


    public void HideMenu()
    {
        shopCanvas.gameObject.SetActive(false);
    }

    public void ShowMenu()
    {
        shopCanvas.gameObject.SetActive(true);
    }

}
