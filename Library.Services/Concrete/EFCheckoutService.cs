using Library.Domain;
using Library.Domain.Model;
using Library.Services.Abstract;
using Microsoft.EntityFrameworkCore;
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
        

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int id)
        {
            return _context.CheckoutHistories
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .Where(h => h.Id == id);
        }
      public Checkout GetLatestCheckout(int assetId)
        {
            return _context.Checkouts
                .Where(c => c.LibraryAsset.Id == assetId)
                .OrderByDescending(c => c.Since)
                .FirstOrDefault();
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
            return _context.Holds
                .Include(h => h.LibraryAsset)
                .Where(h => h.LibraryAsset.Id == id);
        }

        public void MarkFound(int Assetid)
        {



            UpdateAssetStatus(Assetid, "Available"); 
            RemoveExistingCheckouts(Assetid);
            CloseExistingCheckOutHistory(Assetid);
            // close any existing history
          

            _context.SaveChanges();

        }

        private void UpdateAssetStatus(int assetid, string v)
        {
            var item = _context.LibraryAssets.
                FirstOrDefault(a => a.Id == assetid);
            _context.Update(item);
            item.status = _context.Statuses.FirstOrDefault(s => s.Name == "Available");
        }

        private void CloseExistingCheckOutHistory(int assetid)
        {
            var now = DateTime.Now;
            var history = _context.CheckoutHistories.FirstOrDefault(h => h.LibraryAsset.Id == assetid && h.Checkin == null);
            if (history != null)
            {
                _context.Update(history);
                history.Checkin = now;
            }
        }

        private void RemoveExistingCheckouts(int assetid)
        {
            // removing existing check outs
            var checkout = _context.Checkouts
                .FirstOrDefault(co => co.LibraryAsset.Id == assetid);
            if (checkout != null)
            {
                _context.Remove(checkout);
            }
        }

        public void MarkLost(int Assetid)
        {
            var item = _context.LibraryAssets.
                FirstOrDefault(a => a.Id == Assetid);
            _context.Update(item);
            item.status = _context.Statuses.FirstOrDefault(s => s.Name == "Lost");
        }

        public void PlaceHold(int assetId, int libraryCardId) 
        {
            throw new NotImplementedException();
        }

        public void CheckinItem(int assetId, int libraryCardId)
        {
            var now = DateTime.Now;

            var item = _context.LibraryAssets
                .FirstOrDefault(a => a.Id == assetId);
            _context.Update(item);
            // remove any existing checks on item
            RemoveExistingCheckouts(assetId);
            // close any existing check out history
            CloseExistingCheckOutHistory(assetId);
            // look for existing holds on the item
            var currentHolds = _context.Holds
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .Where(x => x.LibraryAsset.Id == assetId);
            // if there are holds check out the item to
            if (currentHolds.Any())
            {
                CheckoutToEarliestHold(assetId, currentHolds);
            }
            // library card with the latest hold
            // otherwise update item status
            UpdateAssetStatus(assetId, "Available");

            _context.SaveChanges();
        }

        private void CheckoutToEarliestHold(int assetId, IQueryable<Hold> currentHolds)
        {
            var earliestHold = _context.Holds
                 .OrderBy(hold => hold.HoldPlaced)
                 .FirstOrDefault();
            var card = earliestHold.LibraryCard;

            _context.Remove(earliestHold);
            _context.SaveChanges();
            CheckoutItem(assetId, card.Id);

        }

        public void CheckoutItem(int assetId, int libraryCardId)
        {
            if (IsCheckout(assetId))
            {
                return;
            }
            UpdateAssetStatus(assetId, "Check Out");
            var item = _context.LibraryAssets
              .FirstOrDefault(a => a.Id == assetId);
            var libraryCard = _context.LibraryCards
                .Include(cards => cards.Checkouts)
                .FirstOrDefault(c => c.Id == libraryCardId);
        }

        private bool IsCheckout(int assetId)
        {
            return _context.Checkouts
                .Where(c => c.LibraryAsset.Id == assetId).Any();
        }
    }
}
