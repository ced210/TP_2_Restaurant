using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EF_ContactsManager.Models
{
    public class ContactsDBEntities : DbContext
    {
        public ContactsDBEntities() : base("name=ContactsDBConnectionString") { }
        public DbSet<Contact> Contacts { get; set; }
    }

    public static class ContactsDBEntities_DAL
    {
        #region Contacts CRUD functions
        public static Contact Create(this ContactsDBEntities DB, Contact contact)
        {
            Contact newContact = DB.Contacts.Add(contact);
            DB.SaveChanges();
            return newContact;
        }
        public static Contact Update(this ContactsDBEntities DB, Contact contact)
        {
            if (contact != null)
            {
                Contact contactToUpdate = DB.Contacts.Find(contact.Id);
                if (contactToUpdate != null)
                {
                    contactToUpdate.Update(contact);
                    DB.Entry(contactToUpdate).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();
                    return contactToUpdate;
                }
            }
            return null;
        }
        public static Contact DeleteContact(this ContactsDBEntities DB, int contactId)
        {
            Contact contactToDelete = DB.Contacts.Find(contactId);
            if (contactToDelete != null)
            {
                DB.Entry(contactToDelete).State = System.Data.Entity.EntityState.Deleted;
                DB.SaveChanges();
                return contactToDelete;
            }
            return null;
        }
        public static List<ContactView> SortedContactList(this ContactsDBEntities DB, string orderBy, bool ascending)
        {
            List<ContactView> items = new List<ContactView>();
            List<Contact> contacts = null;
            switch (orderBy)
            {
                case "Name":
                    if (ascending)
                        contacts = DB.Contacts.OrderBy(c => c.FirstName).ThenBy(c => c.LastName).ToList();
                    else
                        contacts = DB.Contacts.OrderByDescending(c => c.FirstName).ThenByDescending(c => c.LastName).ToList();
                    break;
                case "Sex":
                    if (ascending)
                        contacts = DB.Contacts.OrderBy(c => c.Sex).ToList();
                    else
                        contacts = DB.Contacts.OrderByDescending(c => c.Sex).ToList();
                    break;
                case "BirthDate":
                    if (ascending)
                        contacts = DB.Contacts.OrderBy(c => c.BirthDate).ToList();
                    else
                        contacts = DB.Contacts.OrderByDescending(c => c.BirthDate).ToList();
                    break;
            }
            foreach (Contact contact in contacts)
            {
                items.Add(contact.ToView());
            }
            return items;
        }
        #endregion
    }
}