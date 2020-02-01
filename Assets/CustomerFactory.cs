using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFactory : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform customerParent;

    public GameObject CreateCustomer()
    {
        return Instantiate(customerPrefab, customerParent);
    }

    public void DeleteCustomer(GameObject customer)
    {
        Destroy(customer);
    }
}
