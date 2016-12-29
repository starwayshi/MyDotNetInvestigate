using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetInvestigate
{
    class RootObject
    {
        public int RootIntElement { get; set; }

        public string RootStringElement { get; set; }

        public List<ChildObject> Childs { get; set; }

        public List<ChildObject> Boys { get; set; }
    }

    class ChildObject
    {
        public int ChildIntElement { get; set; }

        public string ChildStringElement { get; set; }

        public List<ChildObject> Childs { get; set; }

    }

    class LinqInvestigate
    {
        public void Execute()
        {
            var actions = new List<Action> { foo1, foo2, foo3, foo4, foo5, foo6 };
            var tasks = new List<Task>();
            actions.ForEach(o => tasks.Add(Task.Run(o)));
            Task.WaitAll(tasks.ToArray());
        }

        private void foo1()
        {
            RootObject testObject = new RootObject()
            {
                RootIntElement = 1,
                RootStringElement = "test",
                Childs = new List<ChildObject>()
            };

            testObject.Childs.Add(new ChildObject() { ChildIntElement = 11, ChildStringElement = "Child1" });
            testObject.Childs.Add(new ChildObject() { ChildIntElement = 12, ChildStringElement = "Child2" });
            testObject.Childs.Add(new ChildObject() { ChildIntElement = 13, ChildStringElement = "Child3" });
            testObject.Childs.Add(new ChildObject() { ChildIntElement = 14, ChildStringElement = "Child4" });
            testObject.Childs.Add(new ChildObject() { ChildIntElement = 15, ChildStringElement = "Child5" });

            List<ChildObject> targets = new List<ChildObject>();
            targets.Add(new ChildObject() { ChildIntElement = 11, ChildStringElement = "TargetChild1" });
            targets.Add(new ChildObject() { ChildIntElement = 11, ChildStringElement = "TargetChild2" });
            targets.Add(new ChildObject() { ChildIntElement = 11, ChildStringElement = "TargetChild3" });
            targets.Add(new ChildObject() { ChildIntElement = 14, ChildStringElement = "TargetChild4" });
            targets.Add(new ChildObject() { ChildIntElement = 16, ChildStringElement = "TargetChild5" });

            testObject.Childs.ForEach(l =>
            {
                l.Childs = targets.Where(p => p.ChildIntElement.ToString() == l.ChildIntElement.ToString()).ToList();
            });

            var result1 = targets.Find(u => u.ChildIntElement == 10);
            var result2 = targets.First(u => u.ChildIntElement == 110);  //throw exception!!
            var result3 = targets.FirstOrDefault(u => u.ChildIntElement == 10);
            var result4 = targets.FindAll(u => u.ChildIntElement == 11);
            var result5 = targets.Any(u => u.ChildIntElement == 15);
            int pageindex = 10;
            int pagesize = 2;
            var result6 = targets.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            var result7 = targets.Select(o => o.ChildStringElement).Distinct()
                .Select((o, i) => string.Format("{0}. {1}", i + 1, o)).ToList();

            ChildObject FirstOrDefaultResult = targets.FirstOrDefault(u => u.ChildIntElement == 24);


        }

        private void foo2()
        {
            List<ChildObject> targets = new List<ChildObject>();
            targets.Add(new ChildObject() { ChildIntElement = 1, ChildStringElement = "TargetChild1" });

            ChildObject result3 = targets.First(n => n.ChildIntElement == 1);
            ChildObject result1 = targets.FirstOrDefault(n => n.ChildIntElement == 0);
            ChildObject result2 = targets.First();
        }

        private class CategroyPropery
        {
            public string ItemCategoryId { get; set; }
            public string PropertyId { get; set; }
            public string ServiceItemCount { get; set; }
        }

        private class ServiceItemInfoResponse
        {
            public List<CategoryInfoResponse> CategoryList { get; set; }
            public int ServiceItemId { get; set; }
        }

        private class CategoryInfoResponse
        {
            public int CategoryId { get; set; }
            public int PropertyId { get; set; }
        }

        private void foo3()
        {
            List<CategroyPropery> request = new List<CategroyPropery>
            {
                new CategroyPropery {ItemCategoryId = "a1", PropertyId = "b1", ServiceItemCount = "c1" },
                new CategroyPropery {ItemCategoryId = "a2", PropertyId = "b2", ServiceItemCount = "c2" },
                new CategroyPropery {ItemCategoryId = "a2", PropertyId = "b2", ServiceItemCount = "c3" },
            };

            var result = request.Distinct(o => new { a = o.ItemCategoryId, b = o.PropertyId }).ToList();
        }

        private void foo4()
        {
            List<ServiceItemInfoResponse> request = new List<ServiceItemInfoResponse>
            {
                new ServiceItemInfoResponse {ServiceItemId = 1, CategoryList = new List<CategoryInfoResponse>
                    {
                        new CategoryInfoResponse {CategoryId = 100, PropertyId = 101 },
                    },
                },
                new ServiceItemInfoResponse {ServiceItemId = 2, CategoryList = new List<CategoryInfoResponse>
                    {
                        new CategoryInfoResponse {CategoryId = 100, PropertyId = 102 },
                    },
                },
                new ServiceItemInfoResponse {ServiceItemId = 2 },
            };

            var a1 = JsonConvert.SerializeObject(request[2].CategoryList);
            var result = request.Distinct(o => JsonConvert.SerializeObject(o.CategoryList)).ToList();
        }

        private void foo5()
        {
            List<RootObject> RootObjectList = new List<RootObject>
            {
                //root1: 对象完整
                new RootObject() {
                    RootIntElement = 1,
                    RootStringElement = "root1",
                    Childs = new List<ChildObject>{
                        new ChildObject() { ChildIntElement = 11, ChildStringElement = "Child11" },
                        new ChildObject() { ChildIntElement = 12, ChildStringElement = "Child12" },
                        new ChildObject() { ChildIntElement = 13, ChildStringElement = "Child13" },
                    }
                },

                //root2: 对象完整
                new RootObject() {
                    RootIntElement = 2,
                    RootStringElement = "root2",
                    Childs = new List<ChildObject>{
                        new ChildObject() { ChildIntElement = 21, ChildStringElement = "Child21" },
                        new ChildObject() { ChildIntElement = 22, ChildStringElement = "Child22" },
                        new ChildObject() { ChildIntElement = 23, ChildStringElement = "Child23" }
                    }
                },

                //root3: 对象内String为null
                new RootObject() {
                    RootIntElement = 3,
                    RootStringElement = null,
                    Childs = new List<ChildObject>()
                },

                //root4: 对象内ojbect为null
                new RootObject() {
                    RootIntElement = 4,
                    RootStringElement = "root4",
                    Childs = null
                },
            };

            var result1 = RootObjectList.Select(l => l.RootIntElement);
            var result2 = RootObjectList.Select(l => l.RootStringElement);
            var result3 = RootObjectList.Where(l => l.RootIntElement == 21);
            var result4 = RootObjectList.FindAll(l => l.RootIntElement == 21);
            var result5 = RootObjectList.SelectMany(l => l.Childs ?? new List<ChildObject>()).ToList();

            RootObject result6 = RootObjectList.First();
            //RootObject result6 = RootObjectList.First(a => a.RootIntElement == 20);  查不到将报错
            RootObject result7 = RootObjectList.FirstOrDefault(a => a.RootIntElement == 99);

            var result8 = RootObjectList.Sum(l => l.RootIntElement);
            var result9 = RootObjectList.Where(m => m.Childs != null).Sum(l => l.Childs.Sum(m => m.ChildIntElement));

            var result10 = RootObjectList.OrderByDescending(o => o.RootIntElement);
            result10.First().RootStringElement = "test!!!";

            List<ChildObject> joinTargets = new List<ChildObject>()
            {
                new ChildObject() { ChildIntElement = 11, ChildStringElement = "TargetChild11" },
                new ChildObject() { ChildIntElement = 22, ChildStringElement = "TargetChild22" },
                new ChildObject() { ChildIntElement = 34, ChildStringElement = "TargetChild34" },
                new ChildObject() { ChildIntElement = 36, ChildStringElement = "TargetChild36" }
            };

            RootObjectList.ForEach(l =>
            {
                if (l.Childs != null)
                {
                    l.Boys = (from c in joinTargets
                              join u in l.Childs
                              on c.ChildIntElement equals u.ChildIntElement
                              select c).ToList();
                    l.Boys.ForEach(a => a.ChildStringElement = "test!!!");
                }
            });
        }


        class Customer
        {
            public int CustomerId { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }

        class Product
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public string Origin { get; set; }
        }
        class Order
        {
            public int OrderId { get; set; }
            public int CustomerId { get; set; }
            public List<Product> Products { get; set; }
        }

        static List<Customer> customers;
        static List<Product> products;
        static List<Order> orders;
        public static void CreateEntities()
        {
            customers = new List<Customer>{
                new Customer(){ CustomerId = 1, Name = "CA", Age=13},
                new Customer(){ CustomerId = 2, Name = "CB", Age=13},
                new Customer(){ CustomerId = 3, Name = "CC", Age=13},
                new Customer(){ CustomerId = 4, Name = "CD", Age=13}
            };

            products = new List<Product>{
                new Product(){ ProductId = 1, Name = "PA", Origin="P1" },
                new Product(){ ProductId = 2, Name = "PB", Origin="P2" },
                new Product(){ ProductId = 3, Name = "PC", Origin="P1" },
                new Product(){ ProductId = 4, Name = "PD", Origin="P3" }
            };

            orders = new List<Order>{
                new Order{
                    OrderId = 1 ,
                    CustomerId =1,
                    Products = new List<Product>{
                        new Product(){ ProductId = 2, Name = "PB", Origin="P2" },
                        new Product(){ ProductId = 3, Name = "PC", Origin="P1" }
                    }},
                new Order{
                    OrderId = 2 ,
                    CustomerId =1,
                    Products = new List<Product>{
                        new Product(){ ProductId = 3, Name = "PC", Origin="P1" },
                        new Product(){ ProductId = 4, Name = "PD", Origin="P3" }
                    }},
                new Order{
                    OrderId = 3 ,
                    CustomerId =3,
                    Products = new List<Product>{
                        new Product(){ ProductId = 4, Name = "PD", Origin="P3" }
                    }},
                new Order{
                    OrderId = 4 ,
                    CustomerId =2,
                    Products = new List<Product>{
                        new Product(){ ProductId = 1, Name = "PA", Origin="P1" },
                        new Product(){ ProductId = 4, Name = "PD", Origin="P3" }
                    }}
            };
        }

        private void foo6()
        {
            CreateEntities();
            //inner join
            var result1 = (from c in customers
                           join o in orders on c.CustomerId equals o.CustomerId
                           select c).ToList();

            //group join
            var result2 = (from c in customers
                           join o in orders on c.CustomerId equals o.CustomerId into os
                           select new { c, os }).ToList();

            //left join
            var result3 = (from c in customers
                           join o in orders on c.CustomerId equals o.CustomerId into os
                           from o2 in os.DefaultIfEmpty()
                           select new { c, o2 }).ToList();
        }
    }
}
