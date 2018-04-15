using App_Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EF_ContactsManager.Models
{
    public enum SexType {male, female, other }
    public class Contact
    {
        #region Members
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SexType Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public string AvatarId { get; set; }
        #endregion

        public Contact() { }
        public static Contact FromView(ContactView contactView)
        {
            Contact contact = new Contact();
            contact.Id = contactView.Id;
            contact.FirstName = contactView.FirstName;
            contact.LastName = contactView.LastName;
            contact.Sex = (SexType)contactView.Sex;
            contact.BirthDate = contactView.BirthDate;
            contact.Phone = contactView.Phone;
            contact.EMail = contactView.EMail;
            contact.AvatarId = contactView.AvatarId;
            return contact;
        }
        public ContactView ToView()
        {
            ContactView contactView = new ContactView();
            contactView.Id = Id;
            contactView.FirstName = FirstName;
            contactView.LastName = LastName;
            contactView.Sex = (SexTypeView)Sex;
            contactView.BirthDate = BirthDate;
            contactView.Phone = Phone;
            contactView.EMail = EMail;
            contactView.EMailVerify = EMail;
            contactView.AvatarId = AvatarId;
            return contactView;
        }
        public void Update(Contact contact)
        {
            FirstName = contact.FirstName;
            LastName = contact.LastName;
            Sex = contact.Sex;
            BirthDate = contact.BirthDate;
            Phone = contact.Phone;
            EMail = contact.EMail;
            AvatarId = contact.AvatarId;
        }
    }
    public enum SexTypeView {[Display(Name = "masculin")] male, [Display(Name = "féminin")] female, [Display(Name = "autre")] other }
    public class ContactView
    {
        #region Members
        public int Id { get; set; }

        [Required(ErrorMessage = "Obligatoire")]
        [StringLength(50, ErrorMessage = "Maximum 50 charactères")]
        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Obligatoire")]
        [StringLength(50, ErrorMessage = "Maximum 50 charactères")]
        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Obligatoire")]
        [Display(Name = "Genre")]
        public SexTypeView Sex { get; set; }

        [Required(ErrorMessage = "Obligatoire")]
        [Display(Name = "Naissance")]
        [DataType(DataType.Date, ErrorMessage = "Invalide")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Obligatoire")]
        [Display(Name = "Téléphone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Obligatoire")]
        [Display(Name = "Courriel")]
        [EmailAddress(ErrorMessage = "Invalide")]
        public string EMail { get; set; }

        [Display(Name = "Vérification")]
        [Compare("EMail", ErrorMessage = "Le courriel et sa vérification ne correspondent pas.")]
        public string EMailVerify { get; set; }

        [Display(Name = "Avatar")]
        public string AvatarId { get; set; }

        public ImageGUIDReference PhotoReference { get; set; }
        #endregion

        public ContactView()
        {
            PhotoReference = new ImageGUIDReference(@"/ContactAvatars/", @"Anonymous.png");
            FirstName = "";
            LastName = "";
            BirthDate = DateTime.Now;
        }
        public String GetPhotoURL()
        {
            return PhotoReference.GetURL(AvatarId);
        }
        public void UpLoadPhoto(HttpRequestBase Request)
        {
            AvatarId = PhotoReference.UpLoadImage(Request, AvatarId);
        }
        public void RemovePhoto()
        {
            PhotoReference.Remove(AvatarId);
        }
    }
}