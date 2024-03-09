using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CS107L_MP.App.Products;

namespace CS107L_MP
{
    public partial class Order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var products = AllProducts();

                // Bind the repeater to the list of products
                ProductRepeater.DataSource = products;
                ProductRepeater.DataBind();
            }
        }

        // Sample data for demonstration
        public IEnumerable<Product> AllProducts()
        {

            return new List<Product>()
            {
                new Product() { Name = "Hatdog", Price = 69.00, Stock = 100 },
                new Product() { Name = "Hatdog din", Price = 20.00, Stock = 50 },
                new Product() { Name = "Jumbo hatdog", Price = 56.00, Stock = 200 },
                new Product() { Name = "Jumbo hatdog", Price = 56.00, Stock = 200 },
                new Product() { Name = "Jumbo hatdog", Price = 56.00, Stock = 200 },
                new Product() { Name = "Jumbo hatdog", Price = 56.00, Stock = 200 },
                new Product() { Name = "Jumbo hatdog", Price = 56.00, Stock = 200 },
                new Product() { Name = "Jumbo hatdog", Price = 56.00, Stock = 200 },
                new Product() { Name = "Jumbo hatdog", Price = 56.00, Stock = 200 },
                new Product() { Name = "Jumbo hatdog", Price = 56.00, Stock = 200 },
                new Product() { Name = "Jumbo hatdog", Price = 56.00, Stock = 200 },
                new Product() { Name = "Jumbo hatdog", Price = 56.00, Stock = 200 },
                new Product() { Name = "Jumbo hatdog", Price = 56.00, Stock = 200 },
                new Product() { Name = "Jumbo hatdog", Price = 56.00, Stock = 200 },
                new Product() { Name = "Jumbo hatdog", Price = 56.00, Stock = 200 },
                new Product() { Name = "Jumbo hatdog", Price = 56.00, Stock = 200 }
            };
        }
    }
}
