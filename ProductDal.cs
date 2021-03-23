using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EntityFrameworkDemo
{
   public class ProductDal
    {
        public List<Product> GetAll()
        {
            using (ETradeContext context = new ETradeContext())
            {
               //List<Product> products = context.Products.ToList();
               //return products;
               return context.Products.ToList();
            }
        }

        public List<Product> GetByName(string key)
        {
            using (ETradeContext context = new ETradeContext())
            {
                List<Product> products = context.Products.Where(p=>p.Name.Contains(key)).ToList();
                return products;
            }
        }

        public List<Product> GetByPrice(decimal min , decimal max)
        {
            using (ETradeContext context = new ETradeContext())
            {
                List<Product> products = context.Products.Where(p => p.UnitPrice >= min && p.UnitPrice<= max ).ToList();
                return products;
            }
        }

        public void Add(Product product)
        {
            using (ETradeContext context = new ETradeContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (ETradeContext context = new ETradeContext())
            {
               Product product = context.Products.FirstOrDefault(x => x.Id == id);
               context.Products.Remove(product);
               context.SaveChanges();
            }
        }

        public void Update(Product product)
        {
           ETradeContext context = new ETradeContext();
            //var entity = context.Entry(product);
            //entity.State = EntityState.Modified;

              Product productUp = context.Products.Find(product.Id);
              productUp.Name = product.Name;
              productUp.StockAmount = product.StockAmount;
              productUp.UnitPrice = product.UnitPrice;

           context.SaveChanges();
        }
    }
}
