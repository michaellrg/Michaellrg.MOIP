using Michaellrg.MOIP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moip.Net.V2.Model;
using Slack.Webhooks;

namespace Michaellrg.MOIP.Web.Controllers
{
    public class CheckoutController : Controller
    {
        //Create Chart
        public static List<Checkout> checklist = new List<Checkout>();
        //Create static MOIP
        public static Cliente cliente = new Cliente();
        public static Endereco endereco = new Endereco();
        


        // GET: Checkout
        public ActionResult Index()
        {
            //Check Cart List 

            if (!checklist.Any())
                return RedirectToAction("Empty", "Checkout");
            else
            {
                return View(checklist.ToList());

            }



        }


        public ActionResult AddItem(int id)
        {
            //Validate if exist
            bool exist = checklist.Exists(s => s.idItem == id);
            //Create objects ItemView (Productlist) and checkout(Product to add on chart)
            ItemsView itemView = new ItemsView();
            Checkout checkout = new Checkout();

            //If Exists QTY ++;
            if (exist == true)
            {

                checkout = checklist.SingleOrDefault(s => s.idItem == id);

                checkout.quantity = checkout.quantity + 1;

            }
            //Else Insert on list the Product
            else
            {
                List<Item> itemList = itemView.Load();
                Item item = itemList.SingleOrDefault(s => s.Id == id);

                checkout.idItem = id;
                checkout.Product = item.Product;
                checkout.Detail = item.Detail;
                checkout.Price = item.Price;
                checkout.urlImage = item.urlImage;
                checkout.quantity = 1;
                checklist.Add(checkout);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult RemoveItem(int id)
        {
            //Find the item
            Checkout checkout = checklist.SingleOrDefault(s => s.idItem == id);
            //Remove
            checklist.Remove(checkout);



            return RedirectToAction("Index", "Checkout");
        }

        public ActionResult Customer()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Customer(CustomerView customer)
        {
            //If model is valid
            if (ModelState.IsValid)
            {
                Random random = new Random();
                //Insert Data on static variables
                cliente.OwnId = "CUS-" + random.Next(100000000, 999999999);
                Session["customer"] = cliente.OwnId;
                cliente.BirthDate = String.Format("yyyy-MM-dd", customer.BirthDate);
                cliente.Email = customer.Email;
                cliente.CreatedAt = DateTime.Now;
                cliente.Fullname = customer.BirthDate;
                cliente.Phone.CountryCode = 55;
                cliente.Phone.AreaCode = int.Parse(customer.AreaCode);
                cliente.Phone.Number = int.Parse(customer.NumberPhone);
                cliente.TaxDocument.Type = Moip.Net.V2.DocumentType.CPF;
                cliente.TaxDocument.Number = customer.Number;

                return RedirectToAction("Address", "Checkout");
            }
            return View(customer);
        }
        public ActionResult Empty()
        {
            //If checklist have any content
            if (checklist.Any())
            {


                return RedirectToAction("Index", "Checkout");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Address()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Address(AddressView address)
        {
            //If model is valid
            if (ModelState.IsValid)
            {
                //Insert address on static customer
                cliente.ShippingAddress.City = address.City;
                cliente.ShippingAddress.Complement = address.Complement;
                cliente.ShippingAddress.Country = "BRA";
                cliente.ShippingAddress.District = address.District;
                cliente.ShippingAddress.State = address.State;
                cliente.ShippingAddress.Street = address.Street;
                cliente.ShippingAddress.StreetNumber = address.StreetNumber;
                cliente.ShippingAddress.ZipCode = address.ZipCode;

                return RedirectToAction("Payment", "Checkout");
            }
            return View(address);
        }


    }

}
