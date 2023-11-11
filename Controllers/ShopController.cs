using Shop1.Entities;
using Shop1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop1.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop

        public ActionResult ListOfBooks()
        {
            List<Books_Catalog> books = new List<Books_Catalog>();
            using (var db = new Shop_JVEntities())
            {
                books = db.Books_Catalog.ToList();
            }
            return View(books);
        }

        List<string> GetAuthorsList()
        {
            List<string> authors = new List<string>();
            using (var db = new Shop_JVEntities())
            {
                foreach (Author author in db.Authors)
                {
                    authors.Add(author.Author_Name);
                }
            }
            return authors;
        }

        List<string> GetPubHouseList()
        {
            List<string> Pub_house = new List<string>();
            using (var db = new Shop_JVEntities())
            {
                foreach (Pub_houses pub_house in db.Pub_houses)
                {
                    Pub_house.Add(pub_house.Pub_house_name);
                }
            }
            return Pub_house;
        }

        List<string> GetGenreList()
        {
            List<string> Genres = new List<string>();
            using (var db = new Shop_JVEntities())
            {
                foreach (Genre genre in db.Genres)
                {
                    Genres.Add(genre.Genre_name);
                }
            }
            return Genres;
        }

        [HttpGet]
        public ActionResult BookDetails(Guid Book_id)
        {
            BookDetails model;
            using (var context = new Shop_JVEntities())
            {
                Books_Catalog book = context.Books_Catalog.Find(Book_id);
                int genre_code = book.Genre_code;
                int author_code = book.Author_code;
                int pub_house_code = book.Book_pub_house_code;
                model = new BookDetails()
                {
                    Book_id = Guid.NewGuid(),
                    Book_name = book.Book_name,
                    Genres = GetGenreList()[genre_code - 1],
                    Authors = GetAuthorsList()[author_code - 1],
                    Pub_house = GetPubHouseList()[pub_house_code - 1],
                    Book_age_categ = book.Book_age_categ,
                    Book_count = book.Book_count,
                    Book_price = book.Book_price
                };
            }
                return View(model);
        }

        [HttpGet]
        public ActionResult CreateBook()
        {
            ViewBag.Authors = new SelectList(GetAuthorsList(), "Item1");
            ViewBag.Genres = new SelectList(GetGenreList(), "Item1");
            ViewBag.PubHouse = new SelectList(GetPubHouseList(), "Iten1");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult CreateBook(Books_CatalogVM newBook)
        {
            
            if (ModelState.IsValid)
            {
                using (var context = new Shop_JVEntities())
                {
                    //int count = context.Authors;
                    Books_Catalog book = new Books_Catalog
                    {
                        Book_id = Guid.NewGuid(),
                        Book_name = newBook.Book_name,
                        Genre_code = context.Genres.Where(i => i.Genre_name == newBook.Genre).FirstOrDefault().Genre_code,
                        Author_code = context.Authors.Where(i => i.Author_Name == newBook.Author).FirstOrDefault().Author_code,
                        Book_pub_house_code = context.Pub_houses.Where(i => i.Pub_house_name == newBook.Book_pub_house).FirstOrDefault().Pub_house_code,
                        Book_age_categ = newBook.Book_age_categ,
                        Book_count = newBook.Book_count,
                        Book_price = newBook.Book_price,
                    };
                    context.Books_Catalog.Add(book);
                    context.SaveChanges();
                }
                return RedirectToAction("ListOfBooks");
            }
            ViewBag.Authors = new SelectList(GetAuthorsList(), "authors");
            ViewBag.Genres = new SelectList(GetGenreList(), "Genres");
            ViewBag.PubHouse = new SelectList(GetPubHouseList(), "Pub_house");
            return View(newBook);            
        }

        [HttpGet]
        public ActionResult EditBook(Guid Book_id)
        {
            BookDetails model;
            using (var context = new Shop_JVEntities())
            {
                Books_Catalog book = context.Books_Catalog.Find(Book_id);
                int genre_code = book.Genre_code;
                int author_code = book.Author_code;
                int pub_house_code = book.Book_pub_house_code;
                model = new BookDetails()
                {
                    Book_id = Guid.NewGuid(),
                    Book_name = book.Book_name,
                    Genres = GetGenreList()[genre_code - 1],
                    Authors = GetAuthorsList()[author_code - 1],
                    Pub_house = GetPubHouseList()[pub_house_code - 1],
                    Book_age_categ = book.Book_age_categ,
                    Book_count = book.Book_count,
                    Book_price = book.Book_price
                };

                //ViewBag.Genres = new SelectList(GetGenreList(), "Genres");
                //ViewBag.Authors = new SelectList(GetAuthorsList(), "authors");
                //ViewBag.PubHouse = new SelectList(GetPubHouseList(), "Pub_house");
            }       
            return View(model);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult EditBook(BookDetails model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new Shop_JVEntities())
                {
                    int genre_code = GetGenreList().IndexOf(model.Genres);
                    int author_code = GetAuthorsList().IndexOf(model.Authors);
                    int pub_house_code = GetPubHouseList().IndexOf(model.Pub_house);
                    Books_Catalog editedBook = new Books_Catalog
                    {
                        Book_id = model.Book_id,
                        Book_name = model.Book_name,
                        Genre_code = genre_code + 1,
                        Author_code = author_code + 1,
                        Book_pub_house_code = pub_house_code + 1,
                        Book_age_categ = model.Book_age_categ,
                        Book_count = model.Book_count,
                        Book_price = model.Book_price
                    };
                    context.Books_Catalog.Attach(editedBook);
                    context.Entry(editedBook).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    //ViewBag.Genres = new SelectList(GetGenreList(), "Genres", model.Genre);
                    //ViewBag.Authors = new SelectList(GetAuthorsList(), "authors", model.Author);
                    //ViewBag.PubHouse = new SelectList(GetPubHouseList(), "Pub_house", model.Book_pub_house);
                }
                return RedirectToAction("ListOfBooks");
            }
            
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteBook(Guid Book_id)
        {
            Books_Catalog bookToDelete;
            using (var context = new Shop_JVEntities())
            {
                bookToDelete = context.Books_Catalog.Find(Book_id);
            }
            return View(bookToDelete);
        }

        [HttpPost, ActionName("DeleteBook")]
        public ActionResult DeleteConfirmed(Guid Book_id)
        {
            using (var context = new Shop_JVEntities())
            {
                Books_Catalog bookToDelete = new Books_Catalog
                {
                    Book_id = Book_id
                };
                context.Entry(bookToDelete).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
            return RedirectToAction("ListOfBooks");
        }
    }
}