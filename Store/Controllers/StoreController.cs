using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Store.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public StoreController(StoreContext context, SignInManager<User> signInManager, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
        }



        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            StoreViewModel viewModel = new StoreViewModel();

            int.TryParse(HttpContext.Request.Cookies["counter"], out int counter);

            var products = _context.Products
                .Where(x => x.Stock.Any(x => x.IsLastElementOrdered == false)) // wyklucza przedmioty których już nie ma w magazynie
                .Where(x => x.Stock.Count > 0) // tzn że są na magazynie jakieś rozmiary jeszcze 
                .Include(p => p.Brand)
                .Include(u => u.Color)
                .Include(u => u.Sex)
                .Include(u => u.Category);


            if (counter != 0) //jeżeli jakieś elementy są już w koszyku
            {
                var listOfEmptyStockIds = GetListOfEmtyStocksIds(counter);
                viewModel.Products = FilterProductList(listOfEmptyStockIds, products).ToList();
            }
            else
            {
                viewModel.Products = products.IncludeFilter(x => x.Stock.Where(x => x.IsLastElementOrdered == false)).ToList();
            }



            return View(viewModel);
        }

        public List<int> GetListOfEmtyStocksIds(int counter)
        {
            var qtyList = new List<int>();


            for (int i = 1; i <= counter; i++)
            {
                var stockId = int.Parse(HttpContext.Request.Cookies["stock-" + i]);

                var qty = int.Parse(HttpContext.Request.Cookies["stock-" + stockId + "-qty"]);

                if (qty == 0)
                {
                    qtyList.Add(stockId);
                }
                else if (qty < 0)
                {
                    throw new Exception("Qty of stockId: " + stockId + " < 0");
                }
            }

            var distinct = qtyList.Distinct().ToList();

            return distinct;
        }

        public IQueryable<Product> FilterProductList(List<int> list, IQueryable<Product> products)
        {
            IQueryable<Product> temp;
            switch (list.Count)
            {
                case 1:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.IsLastElementOrdered == false));
                    break;

                case 2:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1]
                   && x.IsLastElementOrdered == false));
                    break;

                case 3:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                   && x.IsLastElementOrdered == false));
                    break;

                case 4:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                    && x.Id != list[3]
                   && x.IsLastElementOrdered == false));
                    break;

                case 5:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                    && x.Id != list[3] && x.Id != list[4]
                   && x.IsLastElementOrdered == false));
                    break;

                case 6:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                    && x.Id != list[3] && x.Id != list[4] && x.Id != list[5]
                   && x.IsLastElementOrdered == false));
                    break;

                case 7:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                    && x.Id != list[3] && x.Id != list[4] && x.Id != list[5] && x.Id != list[6]
                   && x.IsLastElementOrdered == false));
                    break;

                case 8:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                    && x.Id != list[3] && x.Id != list[4] && x.Id != list[5] && x.Id != list[6] && x.Id != list[7]
                   && x.IsLastElementOrdered == false));
                    break;

                case 9:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                    && x.Id != list[3] && x.Id != list[4] && x.Id != list[5] && x.Id != list[6] && x.Id != list[7] && x.Id != list[8]
                   && x.IsLastElementOrdered == false));
                    break;

                case 10:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                    && x.Id != list[3] && x.Id != list[4] && x.Id != list[5] && x.Id != list[6] && x.Id != list[7] && x.Id != list[8] && x.Id != list[9]

                   && x.IsLastElementOrdered == false));
                    break;

                case 11:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                    && x.Id != list[3] && x.Id != list[4] && x.Id != list[5] && x.Id != list[6] && x.Id != list[7] && x.Id != list[8] && x.Id != list[9]
                    && x.Id != list[10]
                   && x.IsLastElementOrdered == false));
                    break;

                case 12:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                    && x.Id != list[3] && x.Id != list[4] && x.Id != list[5] && x.Id != list[6] && x.Id != list[7] && x.Id != list[8] && x.Id != list[9]
                    && x.Id != list[10] && x.Id != list[11]
                   && x.IsLastElementOrdered == false));
                    break;

                case 13:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                    && x.Id != list[3] && x.Id != list[4] && x.Id != list[5] && x.Id != list[6] && x.Id != list[7] && x.Id != list[8] && x.Id != list[9]
                    && x.Id != list[10] && x.Id != list[11] && x.Id != list[12]
                   && x.IsLastElementOrdered == false));
                    break;

                case 14:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                    && x.Id != list[3] && x.Id != list[4] && x.Id != list[5] && x.Id != list[6] && x.Id != list[7] && x.Id != list[8] && x.Id != list[9]
                    && x.Id != list[10] && x.Id != list[11] && x.Id != list[12] && x.Id != list[13]
                   && x.IsLastElementOrdered == false));
                    break;

                case 15:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                    && x.Id != list[3] && x.Id != list[4] && x.Id != list[5] && x.Id != list[6] && x.Id != list[7] && x.Id != list[8] && x.Id != list[9]
                    && x.Id != list[10] && x.Id != list[11] && x.Id != list[12] && x.Id != list[13] && x.Id != list[14]
                   && x.IsLastElementOrdered == false));
                    break;

                case 16:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                    && x.Id != list[3] && x.Id != list[4] && x.Id != list[5] && x.Id != list[6] && x.Id != list[7] && x.Id != list[8] && x.Id != list[9]
                    && x.Id != list[10] && x.Id != list[11] && x.Id != list[12] && x.Id != list[13] && x.Id != list[14] && x.Id != list[15]

                   && x.IsLastElementOrdered == false));
                    break;

                case 17:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                    && x.Id != list[3] && x.Id != list[4] && x.Id != list[5] && x.Id != list[6] && x.Id != list[7] && x.Id != list[8] && x.Id != list[9]
                    && x.Id != list[10] && x.Id != list[11] && x.Id != list[12] && x.Id != list[13] && x.Id != list[14] && x.Id != list[15]
                    && x.Id != list[16]
                   && x.IsLastElementOrdered == false));
                    break;

                case 18:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                    && x.Id != list[3] && x.Id != list[4] && x.Id != list[5] && x.Id != list[6] && x.Id != list[7] && x.Id != list[8] && x.Id != list[9]
                    && x.Id != list[10] && x.Id != list[11] && x.Id != list[12] && x.Id != list[13] && x.Id != list[14] && x.Id != list[15]
                    && x.Id != list[16] && x.Id != list[17]
                   && x.IsLastElementOrdered == false));
                    break;

                case 19:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                    && x.Id != list[3] && x.Id != list[4] && x.Id != list[5] && x.Id != list[6] && x.Id != list[7] && x.Id != list[8] && x.Id != list[9]
                    && x.Id != list[10] && x.Id != list[11] && x.Id != list[12] && x.Id != list[13] && x.Id != list[14] && x.Id != list[15]
                    && x.Id != list[16] && x.Id != list[17] && x.Id != list[18]
                   && x.IsLastElementOrdered == false));
                    break;

                case 20:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.Id != list[0] && x.Id != list[1] && x.Id != list[2]
                    && x.Id != list[3] && x.Id != list[4] && x.Id != list[5] && x.Id != list[6] && x.Id != list[7] && x.Id != list[8] && x.Id != list[9]
                    && x.Id != list[10] && x.Id != list[11] && x.Id != list[12] && x.Id != list[13] && x.Id != list[14] && x.Id != list[15]
                    && x.Id != list[16] && x.Id != list[17] && x.Id != list[18] && x.Id != list[19]
                   && x.IsLastElementOrdered == false));
                    break;

                default:
                    temp = products.IncludeFilter(x => x.Stock.Where(x => x.IsLastElementOrdered == false));
                    break;
            }

            return temp;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(int id)
        {
            List<Product> model = new List<Product>();

            int.TryParse(HttpContext.Request.Cookies["counter"], out int counter);

            var products = _context.Products
                .Where(x => x.Stock.Any(x => x.IsLastElementOrdered == false)) // wyklucza przedmioty których już nie ma w magazynie
                .Where(x => x.Stock.Count > 0) // tzn że są na magazynie jakieś rozmiary jeszcze 
                .Where(p => p.Id == id)
                .Include(p => p.Brand)
                .Include(u => u.Color)
                .Include(u => u.Sex)
                .Include(u => u.Category);


            if (counter != 0) //jeżeli jakieś elementy są już w koszyku
            {
                var listOfEmptyStockIds = GetListOfEmtyStocksIds(counter);
                model = FilterProductList(listOfEmptyStockIds, products).ToList();
            }
            else
            {
                model = products.IncludeFilter(x => x.Stock.Where(x => x.IsLastElementOrdered == false)).ToList();
            }



            return View(model[0]);
        }

        [HttpPost]
        public IActionResult Cart(int id, int counter)
        {
            string cookieName = "stock-" + counter;

            HttpContext.Response.Cookies.Append(cookieName, id.ToString());
            HttpContext.Response.Cookies.Append("counter", counter.ToString());

            //qty 
            var stock = _context.Stock.FirstOrDefault(x => x.Id == id);
            int qty;
            if (string.IsNullOrEmpty(HttpContext.Request.Cookies["stock-" + id + "-qty"]))
            {
                qty = stock.Qty;
            }
            else
            {
                qty = int.Parse(HttpContext.Request.Cookies["stock-" + id + "-qty"]);
            }

            qty--;
            HttpContext.Response.Cookies.Append("stock-" + id + "-qty", qty.ToString());
            System.Diagnostics.Debug.WriteLine("Poprawne dodnie do koszyka");
            return Ok();
        }

        public int FindCookieByStockId(int stockId, int counter)
        {
            for (int i = 1; i <= counter; i++)
            {
                if (stockId == int.Parse(HttpContext.Request.Cookies["stock-" + i]))
                {
                    return i;
                }
            }

            throw new Exception("Nie znaleziono takiego ciasteczka");
        }


        public IActionResult DeleteFromCart(int stockId)
        {
            int.TryParse(HttpContext.Request.Cookies["counter"], out int counter);
            var id = FindCookieByStockId(stockId, counter);

            Replace(id, counter);
            counter--;

            //HttpContext.Response.Cookies.Delete("stock-" + id); // to już jest w replace
            HttpContext.Response.Cookies.Append("counter", counter.ToString());

            // todo odblokowywanie elementu w sklepie
            int.TryParse(HttpContext.Request.Cookies["stock-" + stockId + "-qty"], out int qty);
            HttpContext.Response.Cookies.Delete("stock-" + stockId + "-qty");
            qty++;
            HttpContext.Response.Cookies.Append("stock-" + stockId + "-qty", qty.ToString());

            //to do jeżeli nie ma więcej produktu o danym id w koszyku to wyczyść wszystkie ciasteczka z nim związane

            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> Cart()
        {

            int.TryParse(HttpContext.Request.Cookies["counter"], out int counter);

            if (counter == 0)
            {
                var model = new List<OrderProduct>();
                return View("Cart", model);
            }
            else
            {
                var list = await GetOrderProductsList(counter);
                foreach (var item in list)
                {
                    item.Product.Brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == item.Product.BrandId);
                }

                return View("Cart", list);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Payment(string stripeEmail, string stripeToken)
        {
            var customers = new Stripe.CustomerService();
            var charges = new Stripe.ChargeService();
            var user = _userManager.GetUserAsync(User).Result;
            int.TryParse(HttpContext.Request.Cookies["counter"], out int counter);
            var orderProducts = await GetOrderProductsList(counter);
            decimal price = 0;

            foreach (var item in orderProducts)
            {
                var stock = await _context.Stock.FirstAsync(x => x.Id == item.StockId);
                stock.Qty -= item.Qty;
                for (int i = 0; i < item.Qty; i++)
                {
                    price += item.Product.Price;
                }

                if (stock.Qty == 0)
                {
                    stock.IsLastElementOrdered = true;
                }

                _context.Update(stock);
                await _context.OrderProducts.AddAsync(item);
            }

            var customer = customers.Create(new Stripe.CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken,
                Name = user.FirstName + " " +user.LastName,
                Address = new Stripe.AddressOptions { City= user.City, Country = "Poland", PostalCode = user.PostCode, Line1 = user.Address1, Line2 = user.Address2},
                Phone = user.PhoneNumber,
            });

            var charge = charges.Create(new Stripe.ChargeCreateOptions
            {
                Amount = Convert.ToInt64(price*100),
                Description = "Sample Charge",
                Currency = "PLN",
                Customer = customer.Id,
                ReceiptEmail = stripeEmail,
            });

            System.Diagnostics.Debug.WriteLine($"Zamówienie użytkownika {user.UserName} o id = {user.   Id}");
            

            //można to zrobić mapperem ale trzeba go dokładnie skonfigurować
            var order = new Order
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address1 = user.Address1,
                Address2 = user.Address2,
                City = user.City,
                PostCode = user.PostCode,
                OrderProducts = orderProducts,
                OrderDate = DateTime.Now,
                UserId = user.Id,
                ChargeId = charge.Id
            };

            await _context.AddAsync(order);

            // czyszczenie koszyka
            HttpContext.Response.Cookies.Delete("counter");

            // todo dodanie zamówienia do tabeli orders i przypisywać je danemu użytkownikowi

            await _context.SaveChangesAsync();
            System.Diagnostics.Debug.WriteLine("Poprawne zamowienie");

            return RedirectToAction("OrdersHistory", "Account");
        }

        public async Task<List<Stock>> GetStockList(int counter)
        {
            var list = new List<Stock>();

            for (int i = 1; i <= counter; i++)
            {
                int stockId;
                int.TryParse(HttpContext.Request.Cookies["stock-" + i], out stockId);

                var stock = await _context.Stock
                    .Include(x => x.Product)
                    .Include(x => x.Product.Brand)
                    .FirstOrDefaultAsync(x => x.Id == stockId);

                list.Add(stock);
            }

            return list;
        }

        public void Replace(int id, int counter)
        {
            var lastElement = HttpContext.Request.Cookies["stock-" + counter];
            HttpContext.Response.Cookies.Append("stock-" + id, lastElement);
            HttpContext.Response.Cookies.Delete("stock-" + counter);
        }



        [HttpGet]
        public IActionResult ShippingInformation()
        {
            var viewModel = new ShippingInformationModel();

            if (_signInManager.IsSignedIn(User))
            {
                var user = _context.Users.Single(x => x.UserName == User.Identity.Name);

                viewModel.User = user;

                return View(viewModel);

            }

            viewModel.User = new User();


            return View(viewModel);
        }


        public async Task<List<OrderProduct>> GetOrderProductsList(int counter)
        {
            var list = new List<OrderProduct>();

            for (int i = 1; i <= counter; i++)
            {
                int stockId;
                int.TryParse(HttpContext.Request.Cookies["stock-" + i], out stockId);

                if (list.Any(x => x.StockId == stockId))
                {
                    var el = list.First(x => x.StockId == stockId).Qty++;
                }
                else
                {
                    var stock = (await _context.Stock.FirstAsync(x => x.Id == stockId));

                    var orderProduct = new OrderProduct
                    {
                        StockId = stockId,
                        ProductId = stock.ProductId,
                        Qty = 1,
                        Stock = stock,
                        Product = await _context.Products.FirstAsync(x => x.Id == stock.ProductId)
                    };

                    list.Add(orderProduct);
                }
            }

            return list;
        }

        [HttpPost]
        public async Task<IActionResult> ShippingInformation(ShippingInformationModel model)
        {

            var user = _userManager.GetUserAsync(User).Result;

            user.Address1 = model.Order.Address1;
            user.Address2 = model.Order.Address2;
            user.City = model.Order.City;
            user.FirstName = model.Order.FirstName;
            user.LastName = model.Order.LastName;
            user.PostCode= model.Order.PostCode;

            await _userManager.UpdateAsync(user);

            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}