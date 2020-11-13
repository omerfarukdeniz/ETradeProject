using Bogus.DataSets;
using DAL.Context;
using MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.StrategyPattern
{
    public class MyInit:CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                AppUser ap = new AppUser()
                {
                    UserName = new Internet("tr").UserName(),
                    Password = new Internet("tr").Password(),
                    Email=new Internet("tr").Email()
                };
                context.AppUsers.Add(ap);
            }
            context.SaveChanges();

            for (int i = 1; i < 11; i++)
            {
                AppUserDetail apd = new AppUserDetail()
                {
                    ID = i,
                    FirstName = new Name("tr").FirstName(),
                    LastName = new Name("tr").LastName(),
                    Phone = new PhoneNumbers("tr").PhoneNumber(),
                    Adress1 = new Address("tr").Locale
                };
                context.AppUserDetails.Add(apd);
            }
            context.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                Category c = new Category()
                {
                    CategoryName = new Commerce("tr").Categories(1)[0],
                    Description = new Lorem("tr").Sentence(10)
                };
                for (int j = 0; j < 10; j++)
                {
                    Product p = new Product()
                    {
                        ProductName = new Commerce("tr").ProductName(),
                        UnitPrice = Convert.ToDecimal(new Commerce("tr").Price()),
                        UnitsInStock = 100,
                        ImagePath = new Images().Nightlife(),
                    };
                    c.Products.Add(p);
                }
                context.Categories.Add(c);
                context.SaveChanges();
            }
        }
    }
}
