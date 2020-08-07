using Library.Domain;
using Library.Domain.Model;
using Library.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Library.Services.Concrete
{
    public class EFCheckoutService : ICheckout
    {
        private LibraryContext _context;

        public EFCheckoutService(LibraryContext contest)
        {

            _context = contest;

        }
        public void AddCheckout(Checkout checkout)
        {
            _context.Add(checkout);
            _context.SaveChanges();
        }

        public IEnumerable<Checkout> GetAllCheckout()
        {
            return _context.Checkouts;
        }
        public Checkout GetCheckoutById(int Id)
        {
            return GetAllCheckout().FirstOrDefault(a => a.Id == Id);
        }
        public void CheckinItem(int assetId, int libraryCardId)
        {
            throw new NotImplementedException();
        }

        public void CheckoutItem(int assetId, int libraryCardId)
        {
            throw new NotImplementedException();
        }

      

       

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int id)
        {
            throw new NotImplementedException();
        }

        public string GetCurrentHoldPatronName(int id)
        {
            throw new NotImplementedException();
        }

        public DateTime GetCurrentHoldPlaced(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hold> GetCurrentHolds(int id)
        {
            throw new NotImplementedException();
        }

        public void MarkFound(int Assetid)
        {
            throw new NotImplementedException();
        }

        public void MarkLost(int Assetid)
        {
            throw new NotImplementedException();
        }

        public void PlaceHold(int assetId, int libraryCardId)
        {
            throw new NotImplementedException();
        }
    }
}
