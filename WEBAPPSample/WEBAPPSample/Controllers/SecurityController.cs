﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEBAPPSample.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace WEBAPPSample.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel inputModel)
        {
            if (!IsAuthentic(inputModel.Username, inputModel.Password))
                return View();

            // create claims
            var claims = GetClaims(inputModel.Username);

            // create identity
            var identity = new ClaimsIdentity(claims, "cookie");

            // create principal
            var principal = new ClaimsPrincipal(identity);

            // sign-in
            await HttpContext.SignInAsync(
                    scheme: "WEBAPPSample",
                    principal: principal);

            return RedirectToAction("About", "Home");
        }

        public async Task<IActionResult> Logout(string requestPath)
        {
            await HttpContext.SignOutAsync(
                    scheme: "WEBAPPSample");

            return RedirectToAction("Login");
        }

        public IActionResult Access()
        {
            return View();
        }

        #region " Private "

        private bool IsAuthentic(string username, string password)
        {
            return !string.IsNullOrEmpty(username);
        }

        private List<Claim> GetClaims(string username)
        {
            if (username.ToLower() == "free")
            {
                return new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Free Member"),
                    new Claim("MembershipId", "111"),
                };
            }

            if (username.ToLower() == "paid")
            {
                return new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Paid Member"),
                    new Claim("MembershipId", "222"),
                    new Claim("HasCreditCard", "Y"),
                    new Claim(ClaimTypes.DateOfBirth, "01/01/2000"),
                    new Claim("AllowNewReleases", "true")
                };
            }

            if (username.ToLower() == "over18")
            {
                return new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Over 18"),
                    new Claim("MembershipId", "333"),
                    new Claim("HasCreditCard", "Y"),
                    new Claim(ClaimTypes.DateOfBirth, "01/01/1980"),
                    new Claim("AllowNewReleases", "false")
                };
            }

            return new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Guest")
            };
        }

        #endregion
    }
}