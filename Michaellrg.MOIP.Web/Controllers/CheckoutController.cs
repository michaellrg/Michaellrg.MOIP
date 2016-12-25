using Michaellrg.MOIP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moip.Net.V2.Model;
using Slack.Webhooks;
using Moip.Net.V2;
using System.Threading.Tasks;

namespace Michaellrg.MOIP.Web.Controllers
{
    public class CheckoutController : AsyncController
    {
        //Create Chart
        public static List<Checkout> checklist = new List<Checkout>();
        //Create static MOIP
        public static Cliente cliente = new Cliente();
        public static Telefone telefone = new Telefone();
        public static Endereco endereco = new Endereco();
        Documento documento = new Documento();
        Pedido pedido = new Pedido();

        public static bool coupon = false;


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

        [HttpPost]
        public ActionResult Index(string Coupon)
        {

            //If Coupon = Moip Have Discount and go to Checkout/Customer
            if (Coupon == "MOIP")
            {
                coupon = true;
                return RedirectToAction("Customer", "Checkout");
            }
            else
            { // If Coupon Have Characteres return to Checkout/Index
                if (Coupon.Any())
                {
                    return View(Coupon);
                }
                else
                {//Else don't have Discount and go to Checkout/Customer
                    return RedirectToAction("Customer", "Checkout");
                }
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
                //Insert Phone
                telefone.CountryCode = 55;
                telefone.AreaCode = int.Parse(customer.AreaCode);
                telefone.Number = int.Parse(customer.NumberPhone);
                //Insert Phone On Customer
                cliente.Phone = telefone;
                //Create Tax Document
                documento.Type = DocumentType.CPF;
                documento.Number = customer.Number;
                //Insert TaxDocument on Customer
                cliente.TaxDocument = documento;

                //Return Action Checkout/Address
                return RedirectToAction("Address", "Checkout");
            }
            return View(customer);
        }
        public ActionResult Empty()
        {
            //If checklist have any content
            if (checklist.Any())
            {

                //Return Action Checkout/Index
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
                endereco.City = address.City;
                endereco.Complement = address.Complement;
                endereco.Country = "BRA";
                endereco.District = address.District;
                endereco.State = address.State;
                endereco.Street = address.Street;
                endereco.StreetNumber = address.StreetNumber;
                endereco.ZipCode = address.ZipCode;
                //Insert Address on Customer
                cliente.ShippingAddress = endereco;
                return RedirectToAction("Payment", "Checkout");
            }
            return View(address);
        }
        public ActionResult Payment()
        {

            ItemPedido item = new ItemPedido();
            List<ItemPedido> itemList = new List<ItemPedido>();
            int total = 0;
            int discount = 0;


            var v2Client = new V2Client(
            new Uri("https://sandbox.moip.com.br/"),
            "01010101010101010101010101010101",
            "ABABABABABABABABABABABABABABABABABABABAB"
                );


            foreach (var product in checklist)
            {
                item.Product = product.Product;
                item.Price = product.Price;
                item.Detail = product.Detail;
                item.Quantity = product.quantity;
                total += (item.Price * item.Quantity);
                itemList.Add(item);
            }

            if (coupon == true)
            {
                discount = ((total * 5) / 100);
            }


            Random random = new Random();
            pedido = new Pedido()
            {
                OwnId = "ORD-" + random.Next(100000000, 999999999),
                Amount = new Valores()
                {
                    Currency = CurrencyType.BRL,
                    Subtotals = new Subtotal()
                    {
                        Discount = discount,
                        Shipping = 1000
                    }
                },
                Items = itemList,
                Customer = cliente
            };

            pedido.CheckoutPreferences.Installments.Addition = ((total * 25) / 1000);
            Pagamento pagamento = new Pagamento();
            pagamento.FundingInstrument.Method = MethodType.ONLINE_DEBIT;

            //v2Client.CriarPagamento();
            var clienteCriado = v2Client.CriarPedido(pedido);
            //v2Client.CriarPagamento(clienteCriado.Id,);
            return null;
        }

    }

}
