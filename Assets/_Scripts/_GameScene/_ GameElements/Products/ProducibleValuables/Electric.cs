using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric : IProducibleValuables
{
   private int amount;

   public Electric(int createAmount)
   {
      amount = createAmount;
   }
}
