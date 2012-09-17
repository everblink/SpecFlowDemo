using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace SpecFlowDemo.Models
{

    #region Models
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    #endregion

    #region Services
    // The FormsAuthentication type is sealed and contains static members, so it is difficult to
    // unit test code that calls its members. The interface and helper class below demonstrate
    // how to create an abstract wrapper around such a type in order to make the AccountController
    // code unit testable.

    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    //public class AccountMembershipService : IMembershipService
    //{
    //    private readonly MembershipProvider _provider;

    //    public AccountMembershipService()
    //        : this(null)
    //    {
    //    }

    //    public AccountMembershipService(MembershipProvider provider)
    //    {
    //        _provider = provider ?? Membership.Provider;
    //    }

    //    public int MinPasswordLength
    //    {
    //        get
    //        {
    //            return _provider.MinRequiredPasswordLength;
    //        }
    //    }

    //    public bool ValidateUser(string userName, string password)
    //    {
    //        if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
    //        if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");

    //        return _provider.ValidateUser(userName, password);
    //    }

    //    public MembershipCreateStatus CreateUser(string userName, string password, string email)
    //    {
    //        if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
    //        if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
    //        if (String.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", "email");

    //        MembershipCreateStatus status;
    //        _provider.CreateUser(userName, password, email, null, null, true, null, out status);
    //        return status;
    //    }

    //    public bool ChangePassword(string userName, string oldPassword, string newPassword)
    //    {
    //        if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
    //        if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
    //        if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

    //        // The underlying ChangePassword() will throw an exception rather
    //        // than return false in certain failure scenarios.
    //        try
    //        {
    //            MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
    //            return currentUser.ChangePassword(oldPassword, newPassword);
    //        }
    //        catch (ArgumentException)
    //        {
    //            return false;
    //        }
    //        catch (MembershipPasswordException)
    //        {
    //            return false;
    //        }
    //    }
    //}

    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }
    #endregion


}
