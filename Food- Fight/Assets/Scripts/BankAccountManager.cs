using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankAccountManager : MonoBehaviour
{

    public int maxWalletSize = 1500;
    private int walletCurrency = 0;


    public void Withdraw(int val)
    {
        if (CanWithdraw(val))
        {
            walletCurrency -= val;
        }
    }


    public bool CanWithdraw(int val)
    {
        return walletCurrency - val >= 0;
    }


    public void Deposit(int val)
    {
        if (CanDeposit(val)) {
            walletCurrency = Mathf.Min(walletCurrency + val, maxWalletSize);
        }
    }

    public bool CanDeposit(int value)
    {
        return value + walletCurrency <= maxWalletSize;
    }
}
