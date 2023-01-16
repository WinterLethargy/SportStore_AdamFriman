using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository orderRepository;
        private Cart cart;
        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            orderRepository = repoService;
            cart = cartService;
        }
        public ViewResult Checkout() => View(new Order());
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (cart.Lines.Count == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty");
            }
            if (ModelState.IsValid) {
                order.Lines = cart.Lines.ToArray();
                orderRepository.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/Completed", new { orderID = order.OrderId });
            }
            else
            {
                return View();
            }
        }
    }
}