using Library.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Abstract
{
  public  interface ILibraryAsset
    {
        IEnumerable<LibraryAsset> GetAllLibraryAssets();
        LibraryAsset GetLibraryAssetById(int Id);
        void AddLibraryAssest(LibraryAsset libraryAsset);
        string GetAuthororDirector(int Id);
        string GetType(int Id); 
        string GetDeweyIndex(int Id);
        string GetTitle(int Id);
        string GetIsbn(int Id);
        LibraryBranch GetLibraryBranchLocation(int Id);
    }
}
