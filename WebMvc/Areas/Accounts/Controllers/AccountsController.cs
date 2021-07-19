using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities.Identities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    [Area("Accounts")]
    public class AccountsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountsController> _logger;
        private IAppUserService _appUserService;

        public AccountsController(IUnitOfWork unitOfWork, 
            SignInManager<AppUser> singInManager, 
            IMapper mapper, 
            ILogger<AccountsController> logger)
        {
            _unitOfWork = unitOfWork;
            _signInManager = singInManager;
            _mapper = mapper;
            _logger = logger;
        }

        protected IAppUserService AppUserService
        {
            get
            {
                if (_appUserService == null)
                {
                    _appUserService = _unitOfWork.GetService(typeof(IAppUserService)) as IAppUserService;
                }
                return _appUserService;
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<UserTokenDto>> Login(LoginDto model, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    return Redirect(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterDto());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid) return View(model);

            var existsEmail = await AppUserService.CheckEmailExistsAsync(model.Email);
            if (existsEmail)
            {
                return new BadRequestObjectResult($"Email address, {model.Email} is in use");
            }

            var user = new AppUser
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await AppUserService.CreateCustomerAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest();

            return RedirectToAction("login");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Accounts", new { area = "Accounts" });
        }
    }
}
