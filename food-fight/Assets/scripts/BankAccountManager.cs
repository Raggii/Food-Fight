using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankAccountManager : MonoBehaviour
{

    public int maxWalletBalance = 1500;
    private int walletBalance = 0;


    public void Withdraw(int val)
    {
        if (CanWithdraw(val))
        {
            walletBalance -= val;
        }
    }


    public bool CanWithdraw(int val)
    {
        return walletBalance - val >= 0;
    }


    public int getBalance()
    {
        return walletBalance;
    }


    public int getMaxBalance()
    {
        return maxWalletBalance;
    }


    public void Deposit(int val)
    {
        if (CanDeposit(val)) {
            walletBalance = Mathf.Min(walletBalance + val, maxWalletBalance);
        }
    }

    public bool CanDeposit(int value)
    {
        return value + walletBalance <= maxWalletBalance;
    }
}
