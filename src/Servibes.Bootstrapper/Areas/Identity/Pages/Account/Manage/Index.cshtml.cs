﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Servibes.Bootstrapper.Models;
using Servibes.Shared.Communication.Brokers;

namespace Servibes.Bootstrapper.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMessageBroker _messageBroker;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMessageBroker messageBroker)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _messageBroker = messageBroker;
        }

        //public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            //[Phone]
            //[Display(Name = "Phone number")]
            //public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            //var userName = await _userManager.GetUserNameAsync(user);
            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //Username = userName;

            var email = await _userManager.GetEmailAsync(user);

            Input = new InputModel
            {
                //PhoneNumber = phoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = email
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //if (Input.PhoneNumber != phoneNumber)
            //{
            //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            //    if (!setPhoneResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set phone number.";
            //        return RedirectToPage();
            //    }
            //}

            var email = await _userManager.GetEmailAsync(user);

            if (Input.Email != email)
            {
                var result = await _userManager.SetEmailAsync(user, Input.Email);
                if (!result.Succeeded)
                {
                    StatusMessage = "Your email cannot be changed.";
                    await LoadAsync(user);
                    return Page();
                }
            }
            
            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";

            if (await _userManager.IsInRoleAsync(user, "Client"))
            {
                var @event = new ClientInformationUpdatedEvent(user.Id, user.FirstName, user.LastName, user.Email);
                await _messageBroker.PublishAsync(@event);
            }

            return RedirectToPage();
        }
    }
}
