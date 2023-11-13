using ProductReviewApp.Data;
using ProductReviewApp.Models;

namespace ProductReviewApp
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext dataContext)
        {
            this.dataContext=dataContext;
        }

        public void SeedDataContext()
        {
            if (!dataContext.Products.Any())
            {
                var product1 = new Product()
                {
                    Name = "Bluetooth speaker",
                    Price = 20,
                    Description = "Suitable for both indoor and outdoor use, vibe and enjoy",
                    Manufacturer = new Manufacturer()
                    {
                        Name = "JBL",
                        Description = "No 1 manufacturer of home appliances",
                        Address = "2A, Broad street, Ohio, US"
                    },
                    Reviews = new List<Review>()
                    {
                        new Review()
                        {
                            Title = "Bluetooth Player",
                            Text = "This is undisputedly the best Bluetooth speaker in the US",
                            Rating = 10,
                            ReviewedOn = new DateTime(2023,06,30),
                            Reviewer = new Reviewer()
                            {
                                FirstName = "Jeremy",
                                LastName = "Doku",
                                Country = new Country()
                                {
                                    Name = "USA"
                                }
                            }
                        },
                        new Review()
                        {
                            Title = "JBL Player",
                            Text = "This is the best Bluetooth speaker one can dream of",
                            Rating = 9,
                            ReviewedOn = new DateTime(2023,08,15),
                            Reviewer = new Reviewer()
                            {
                                FirstName = "Pascal",
                                LastName = "Gross",
                                Country = new Country()
                                {
                                    Name = "Scotland"
                                }
                            }
                        },
                         new Review()
                        {
                            Title = "Good Speaker",
                            Text = "The Bluetooth speaker you can trust",
                            Rating = 10,
                            ReviewedOn = new DateTime(2022,03,24),
                            Reviewer = new Reviewer()
                            {
                                FirstName = "Memphis",
                                LastName = "Depay",
                                Country = new Country()
                                {
                                    Name = "Netherlands"
                                }
                            }
                        }
                    },
                    ProductCategories = new List<ProductCategory>()
                    {
                        new ProductCategory()
                        {
                            Category = new Category() 
                            {
                                Name = "Home Appliances"
                            }
                        },
                        new ProductCategory()
                        {
                            Category = new Category()
                            {
                                Name = "Gadgets"
                            }
                        }
                    },
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now

            };

                var product2 = new Product()
                {
                    Name = "G280",
                    Price = 108,
                    Description = "Your laptop for all occasions",
                    Manufacturer = new Manufacturer()
                    {
                        Name = "HP",
                        Description = "No 1 manufacturer of laptops",
                        Address = "33A, Broad street, Lagos, Portugal"
                    },
                    Reviews = new List<Review>()
                    {
                        new Review()
                        {
                            Title = "G280 Laptop",
                            Text = "This is undisputedly the best personal computer in Portugal",
                            Rating = 10,
                            ReviewedOn = new DateTime(2023,10,20),
                            Reviewer = new Reviewer()
                            {
                                FirstName = "Puskas",
                                LastName = "geek",
                                Country = new Country()
                                {
                                    Name = "Latvia"
                                }
                            }
                        },
                        new Review()
                        {
                            Title = "Personal computer",
                            Text = "This is the best laptop device one can dream of",
                            Rating = 8,
                            ReviewedOn = new DateTime(2023,08,27),
                            Reviewer = new Reviewer()
                            {
                                FirstName = "Roman",
                                LastName = "Empire",
                                Country = new Country()
                                {
                                    Name = "Italy"
                                }
                            }
                        },
                         new Review()
                        {
                            Title = "Good specifications",
                            Text = "Specifications you can trust for coding and gaming",
                            Rating = 10,
                            ReviewedOn = new DateTime(2022,12,27),
                            Reviewer = new Reviewer()
                            {
                                FirstName = "Adekunle",
                                LastName = "Ojo",
                                Country = new Country()
                                {
                                    Name = "Nigeria"
                                }
                            }
                        }
                    },
                    ProductCategories = new List<ProductCategory>()
                    {
                        new ProductCategory()
                        {
                            Category = new Category()
                            {
                                Name = "Music and Gaming"
                            }
                        },
                        new ProductCategory()
                        {
                            Category = new Category()
                            {
                                Name = "Gadgets"
                            }
                        }
                    },
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now

                };
                dataContext.Products.AddRange(product1);
                dataContext.Products.AddRange(product2);
                dataContext.SaveChanges();
            };    
        }
    }
}
