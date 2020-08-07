using Library.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Abstract
{
   public  interface ICheckout
    {
        IEnumerable<Checkout> GetAllCheckout();
        Checkout GetCheckoutById(int Id);
        void AddCheckout(Checkout checkout);
        void CheckoutItem(int assetId , int libraryCardId);
        void CheckinItem(int assetId, int libraryCardId);
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int id);
        void PlaceHold(int assetId, int libraryCardId);
        string GetCurrentHoldPatronName(int id);
        DateTime GetCurrentHoldPlaced(int id);
        IEnumerable<Hold> GetCurrentHolds(int id);
        void MarkLost(int Assetid);
        void MarkFound(int Assetid);


    }
}
