using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PickupManager : MonoBehaviour
{
   PickupController[] pickups;

   void Awake() {
       pickups = GetComponentsInChildren<PickupController>();
       foreach (PickupController pickup in pickups)
       {
           Debug.Log(pickup.name);
       }
   }

   void ResetAllPickups() {
       foreach (PickupController pickup in pickups)
       {
           pickup.Reset();
       }
   }

   void Pickup(GameObject pickup) {
       pickup.SendMessage("PickedUp");
   }
}
