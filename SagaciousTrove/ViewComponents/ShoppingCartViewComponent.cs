using System;
using System.Security.Claims;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace SagaciousTrove.ViewComponents
{
	public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null) // User is logged in
            {
                // Check if SessionCart key in session exists
                if (HttpContext.Session.GetInt32(SD.SessionCart) != null)
                {
                    return View(HttpContext.Session.GetInt32(SD.SessionCart));
                }
                else // go to the database and retrieve the count
                {
                    HttpContext.Session.SetInt32(SD.SessionCart,
                        _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count
                    );
                    return View(HttpContext.Session.GetInt32(SD.SessionCart));
                }
            }
            else // User is not logged in. Clear the session 
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
	}
}