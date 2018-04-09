using EF_ContactsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EF_ContactsManager.Controllers
{
    public class ContactsController : Controller
    {
        #region Sort setup
        private void InitSessionSort()
        {
            if (Session["ContactSortBy"] == null)
            {
                Session["ContactSortBy"] = "Name";
                Session["ContactSortAscendant"] = true;
            }
        }
        public ActionResult Sort(string by)
        {
            if (by == (string)Session["ContactSortBy"])
                Session["ContactSortAscendant"] = !(bool)Session["ContactSortAscendant"];
            else
                Session["ContactSortAscendant"] = true;

            Session["ContactSortBy"] = by;
            return RedirectToAction("Index");
        }
        #endregion
        
        #region List
        public ActionResult Index()
        {
            InitSessionSort();
            using (var DB = new ContactsDBEntities())
            {
                return View(DB.SortedContactList((string)Session["ContactSortBy"], (bool)Session["ContactSortAscendant"]));
            }
        }
        #endregion
        
        #region Create
        public ActionResult Create()
        {
            ContactView contactView = new ContactView();
            return View(contactView);
        }
        [HttpPost]
        public ActionResult Create(ContactView contactView)
        {
            contactView.UpLoadPhoto(Request);
            using (var DB = new ContactsDBEntities()) { DB.Create(Contact.FromView(contactView)); }
            return RedirectToAction("Index");
        }
        #endregion
        
        #region Edit
        public ActionResult Edit(int id)
        {
            Contact contact = null;
            using (var DB = new ContactsDBEntities()) { contact = DB.Contacts.Find(id); }
            if (contact != null)
            {
                return View(contact.ToView());
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(ContactView contactView)
        {
            contactView.UpLoadPhoto(Request);
            using (var db = new ContactsDBEntities()) { db.Update(Contact.FromView(contactView)); }
            return RedirectToAction("Index");
        }
        #endregion
        
        #region Delete
        public ActionResult Delete(int id)
        {
            using (var DB = new ContactsDBEntities())
            {
                Contact contact = DB.Contacts.Find(id);  
                if (contact != null)
                {
                    contact.ToView().RemovePhoto();
                    DB.DeleteContact(id);
                }
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}