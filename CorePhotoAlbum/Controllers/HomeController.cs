using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CorePhotoAlbum.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CorePhotoAlbum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PhotosharingdbContext _context;
        private readonly IHostingEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, PhotosharingdbContext context, IHostingEnvironment environment)
        {
            _logger = logger;
            this._context = context;
            this._environment = environment;
        }

        public IActionResult Index()
        {
            return View(_context.Photos.ToList());
        }

        public IActionResult GetImage(int photoId)
        {
            Photos requestedPhoto = _context.Photos.Find(photoId);


            if (requestedPhoto != null)
            {   //                                                                                    ****************** MIME Type
                return File(requestedPhoto.PhotoFile, requestedPhoto.ImageMimeType);
            }

            else
            {
                return BadRequest();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
