using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using DMSapi_v2.Models;
using DMSapi_v2.Services;

namespace DMSapi_v2.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CheckoutController : ControllerBase
    {
        private readonly CheckoutService _checkoutService;
        public CheckoutController(CheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpGet]
        public ActionResult<List<Checkout>> GetAllCheckout() => _checkoutService.GetAllCheckout();

        [HttpGet("{id}")]
        public ActionResult<Checkout> GetCheckoutById(string id)
        {
            var checkout = _checkoutService.GetCheckoutById(id);
            if (checkout == null)
            {
                return NotFound();
            }
            return checkout;
        }

        [HttpGet("{checkoutStatus}")]
        public  ActionResult<List<Checkout>>  GetCheckoutByStatus(string checkoutStatus)
        {
            var checkout = _checkoutService.GetCheckoutByStatus(checkoutStatus);
            if (checkout == null)
            {
                return NotFound();
            }
            return checkout;
        }

        [HttpGet("{datex1}/{datex2}")]
        public ActionResult<List<Checkout>> GetCheckoutByMonth(DateTime datex1, DateTime datex2)
        {
            var filter = _checkoutService.FilterCheckoutByMonth(datex1, datex2);
            if (filter == null)
            {
                return NotFound();
            }
            return filter;
        }

        [HttpPost]
        public Checkout CreateCheckout([FromBody] Checkout checkout)
        {
            var data = _checkoutService.GetAllCheckoutForApi();
            var count = data.Count();
            var id = "C00" + count.ToString();
            checkout.CheckoutId = id;
            checkout.Status = "Open";
            _checkoutService.CreateCheckout(checkout);
            return checkout;
        }

        [HttpPut("{id}")]
        public IActionResult EditCheckout([FromBody] Checkout checkout, string id)
        {
            var checkouts = _checkoutService.GetCheckoutById(id);
            if (checkouts == null)
            {
                return NotFound();
            }
            checkout.CheckoutId = id;
            _checkoutService.EditCheckout(id, checkout);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult DeleteCheckout(string id)
        {
            var checkouts = _checkoutService.GetCheckoutById(id);
            var statusChange = checkouts.Status;
            if (checkouts == null)
            {
                return NotFound();
            }
            if (statusChange == "Open")
            {
                statusChange = "Close";
            }
            checkouts.Status = statusChange;
            _checkoutService.DeleteCheckout(id, checkouts);
            return NoContent();
        }
    }
}