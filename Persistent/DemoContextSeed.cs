using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistent
{
    public class DemoContextSeed
    {
        public static async Task SeedAsync(DemoContext context, UserManager<AppUser> userManager, ILoggerFactory loggerFactory)
        {
            try
            {
                string filePath = "../Persistent/Data/Seed/";
                await context.Database.OpenConnectionAsync();
                if (!context.ProductBrands.Any())
                {
                    var itemsData = System.IO.File.ReadAllText($"{filePath}brands.json");
                    var items = JsonSerializer.Deserialize<List<ProductBrand>>(itemsData);

                    foreach (var item in items)
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.ProductBrands ON;");
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.ProductBrands OFF;");
                }

                if (!context.ProductTypes.Any())
                {
                    var itemsData = System.IO.File.ReadAllText($"{filePath}types.json");
                    var items = JsonSerializer.Deserialize<List<ProductType>>(itemsData);

                    foreach (var item in items)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.ProductTypes ON;");
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.ProductTypes OFF;");
                }

                if (!context.Products.Any())
                {
                    var itemsData = System.IO.File.ReadAllText($"{filePath}products.json");
                    var items = JsonSerializer.Deserialize<List<Product>>(itemsData);

                    foreach (var item in items)
                    {
                        context.Products.Add(item);
                    }
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Products ON;");
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Products OFF;");
                }

                if (!context.DeliveryMethods.Any())
                {
                    var itemsData = System.IO.File.ReadAllText($"{filePath}delivery.json");
                    var items = JsonSerializer.Deserialize<List<DeliveryMethod>>(itemsData);

                    foreach (var item in items)
                    {
                        context.DeliveryMethods.Add(item);
                    }
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.DeliveryMethods ON");
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.DeliveryMethods OFF");
                }

                if (!context.Roles.Any())
                {
                    var itemsData = System.IO.File.ReadAllText($"{filePath}roles.json");
                    var items = JsonSerializer.Deserialize<List<AppRole>>(itemsData);

                    foreach (var item in items)
                    {
                        context.AppRoles.Add(item);
                    }
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.AppRoles ON;");
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.AppRoles OFF;");
                }

                if (!userManager.Users.Any())
                {
                    var user = new AppUser
                    {
                        DisplayName = "Administrator",
                        Email = "admin@root.com",
                        UserName = "admin@root.com",
                        Address = new Address
                        {
                            FirstName = "Demo",
                            LastName = "Oho",
                            Street = "10 The Strees",
                            City = "New York",
                            State = "NY",
                            Zipcode = "90210"
                        }

                    };

                    await userManager.CreateAsync(user, "P@ssw0rd");

                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            catch (System.Exception ex)
            {
                var logger = loggerFactory.CreateLogger<DemoContextSeed>();
                logger.LogError(ex.Message);
            }
            finally
            {
                await context.Database.CloseConnectionAsync();
            }
        }
    }
}