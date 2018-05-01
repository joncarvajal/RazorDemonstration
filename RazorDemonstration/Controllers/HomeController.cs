using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using RazorDemonstration.Models;

namespace RazorDemonstration.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<ComicTitle> comicTitles;

            using (IDbConnection source = new SqlConnection(Database.ConnectionString()))
            {
                source.Open();

                // get all comics
                comicTitles = source.Query<ComicTitle>(
                    ComicTitle.Select())
                    .Distinct()
                    .OrderBy(ct => ct.Name)
                    .ToList();
            }
            
            ViewBag.ComicTitles = comicTitles;
            
            return View();
        }

        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var comicTitle = GetComicTitle(id);

            if (comicTitle != null)
            {
                ViewBag.ComicTitle = comicTitle;
                ViewBag.Title = id.ToString();
                ViewBag.IsFormReadOnly = false;
            }
            return View("View");
        }

        [Route("View/{id}")]
        public IActionResult View(int id)
        {
            var comicTitle = GetComicTitle(id);

            if (comicTitle != null)
            {
                ViewBag.ComicTitle = comicTitle;
                ViewBag.Title = id.ToString();
                ViewBag.IsFormReadOnly = true;
            }

            return View("View");
        }
        

        [HttpPost]
        [Route("View/{id}")]
        public IActionResult Save(int id, string name, string url, string iconUrl)
        {
            var comicTitle = new ComicTitle
            {
                ComicTitleId = id,
                Name = name,
                Url = url,
                IconUrl = iconUrl
            };

            using (IDbConnection target = new SqlConnection(Database.ConnectionString()))
            {
                target.Open();
                
                comicTitle.Update(target);
            }

            ViewBag.ComicTitle = comicTitle;
            ViewBag.Title = id.ToString();
            ViewBag.IsFormReadOnly = true;

            return View("View");
        }

        private ComicTitle GetComicTitle(int id)
        {
            ComicTitle comicTitle;

            using (IDbConnection source = new SqlConnection(Database.ConnectionString()))
            {
                source.Open();

                // get all comics
                comicTitle = source.Query<ComicTitle>(
                    ComicTitle.Select())
                    .FirstOrDefault(c => c.ComicTitleId.Equals(id));
            }

            return comicTitle;
        }
    }
}
