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
    public class EfLibraryAsset : ILibraryAsset
    {
        private LibraryContext _context;

        public EfLibraryAsset(LibraryContext context)
        {
            _context = context;
        }
        public void AddLibraryAssest(LibraryAsset libraryAsset)
        {
            _context.Add(libraryAsset);
            _context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAllLibraryAssets()
        {
            return _context.LibraryAssets
                .Include(asset => asset.status)
                .Include(asset => asset.Location);
                                    
        }

        public LibraryAsset GetLibraryAssetById(int Id)
        {
            return GetAllLibraryAssets()
                 .FirstOrDefault(asset => asset.Id == Id);
        }

        public string GetDeweyIndex(int Id)
        {
            if (_context.Books.Any(book => book.Id == Id))
            {
                return _context.Books.FirstOrDefault(book => book.Id == Id).DeweyIndex;
            }
            else return "";
        }

        public string GetIsbn(int Id)
        {
            if (_context.Books.Any(book => book.Id == Id))
            {
                return _context.Books.FirstOrDefault(book => book.Id == Id).ISBN;
            }
            else return "";
        }

        

        public LibraryBranch GetLibraryBranchLocation(int Id)
        {
            //_context.LibraryAssets.FirstOrDefault(asset => asset.Id == Id).Location;
            return GetLibraryAssetById(Id).Location;
        }

        public string GetTitle(int Id)
        {
            return GetLibraryAssetById(Id).Title;
        }

        public string GetType(int Id)
        {
            var book = _context.LibraryAssets.OfType<Book>().Where(b => b.Id == Id);
            return book.Any() ? "Book" : "Video";
        }

        public string GetAuthororDirector(int Id)
        {
            var isBook = _context.LibraryAssets.OfType<Book>()
                             .Where(asset => asset.Id == Id).Any();

            var isVideo = _context.LibraryAssets.OfType<Video>()
                             .Where(asset => asset.Id == Id).Any();
            return isBook ?
                _context.Books.FirstOrDefault(b => b.Id == Id).Author :
                _context.Videos.FirstOrDefault(b => b.Id == Id).Director
            ?? "Unknown";
        }
    }
}
