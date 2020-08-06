using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Services.Abstract;
using LibraryMS.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMS.Controllers
{
    public class CatalogController : Controller
    {
        private ILibraryAsset _asset;

        public CatalogController(ILibraryAsset asset)
        {
            _asset = asset;
        }
        public IActionResult Index()
        {
            var assetModel = _asset.GetAllLibraryAssets();

            var listingResult = assetModel.Select(result => new AssetListingModel
            {
                Id = result.Id,
                ImageUrl = result.imageUrl,
                AuthorOrDirector = _asset.GetAuthororDirector(result.Id),
                DeweyCallNumbers = _asset.GetDeweyIndex(result.Id),
                Type = _asset.GetType(result.Id),
                Title = result.Title,


            });

            var model = new AssetIndexModel()
            {
                Assets = listingResult
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var asset = _asset.GetLibraryAssetById(id);
            var model = new AssetDetailModel
            {
                AssetId = asset.Id,
                Title = asset.Title,
                Year = asset.Year,
                Cost = asset.Cost,
                status = asset.status.Name,
                imageUrl = asset.imageUrl,
                AuthorOrDirector = _asset.GetAuthororDirector(id),
                CurrentLocation = _asset.GetLibraryBranchLocation(id).Name,
                DeweyCallNumber = _asset.GetDeweyIndex(id),
                ISBN = _asset.GetIsbn(id)
            };
            return View(model);
        }
    }
}
