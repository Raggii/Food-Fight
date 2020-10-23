using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankAccountManager : MonoBehaviour
{

    public int maxWalletSize = 1500;

    private int walletCurrency = 0;

    public void Deposit(int val)
    {
        walletCurrency = Mathf.Min(walletCurrency + val, maxWalletSize);
    }

    public bool CanDeposit(int value)
    {
        return value + walletCurrency <= maxWalletSize;
    }
}
