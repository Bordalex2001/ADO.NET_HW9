using ADO.NET_HW9.Models;
using Spectre.Console;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_HW9
{
    internal class Program
    {
        static void Main()
        {
            LoadMenu();
        }

        static void LoadMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Склад");
                Console.WriteLine("\n1. Показати всі товари");
                Console.WriteLine("2. Показати всі типи товарів");
                Console.WriteLine("3. Показати всіх постачальників");
                Console.WriteLine("4. Показати товар з найбільшою кількістю");
                Console.WriteLine("5. Показати товар з найменшою кількістю");
                Console.WriteLine("6. Показати товар з найбільшою собівартістю");
                Console.WriteLine("7. Показати товар з найменшою собівартістю");
                Console.WriteLine("8. Показати товари заданого типу");
                Console.WriteLine("9. Показати товари заданого постачальника");
                Console.WriteLine("10. Показати найзастарілий товар");
                Console.WriteLine("11. Показати середню кількість товарів за кожним типом");
                Console.WriteLine("0. Вийти");
                
                Console.Write("\nОберіть функцію чи запит: ");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        ShowAllProducts();
                        break;
                    case 2:
                        ShowProductTypes();
                        break;
                    case 3:
                        ShowSuppliers();
                        break;
                    case 4:
                        ShowMaxQuantityProduct();
                        break;
                    case 5:
                        ShowMinQuantityProduct();
                        break;
                    case 6:
                        ShowMaxCostPriceProduct();
                        break;
                    case 7:
                        ShowMinCostPriceProduct();
                        break;
                    case 8:
                        ShowProductsByType();
                        break;
                    case 9:
                        ShowProductsBySupplier();
                        break;
                    case 10:
                        ShowOldestProduct();
                        break;
                    case 11:
                        ShowAverageQuantityForEachProductType();
                        break;
                    case 0:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("На жаль, функції з таким номером не існує. Будь ласка, спробуйте обрати ще раз");
                        break;
                }
            }
        }

        static void ShowAllProducts()
        {
            using (WarehouseDbContext db = new WarehouseDbContext())
            {
                List<Product> products = db.Products
                    .Include(p => p.ProductType)
                    .Include(p => p.Supplier)
                    .ToList();

                Console.WriteLine("\nУсі товари:");
                var table = new Table()
                {
                    Border = TableBorder.Square,
                    ShowRowSeparators = true,
                };
                
                table.AddColumns("Id", "Name", "ProductTypeId", "SupplierId", "Quantity", "CostPrice", "SupplyDate");
                foreach (Product product in products)
                {
                    table.AddRow($"{product.Id}", $"{product.Name}", $"{product.ProductTypeId}", $"{product.SupplierId}", $"{product.Quantity}", $"{product.СostPrice}", $"{product.SupplyDate}");
                }
                AnsiConsole.Write(table);
            }

            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void ShowProductTypes()
        {
            using (WarehouseDbContext db = new WarehouseDbContext())
            {
                List<ProductType> productTypes = db.ProductTypes.Distinct().ToList();

                Console.Write("\nВсі типи товарів: ");
                foreach (ProductType type in productTypes)
                {
                    Console.Write(type.Name + ", ");
                }
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void ShowSuppliers()
        {
            using (WarehouseDbContext db = new WarehouseDbContext())
            {
                List<Supplier> suppliers = db.Suppliers.Distinct().ToList();

                Console.Write("\nВсі постачальники: ");
                foreach (Supplier supplier in suppliers)
                {
                    Console.Write(supplier.Name + ", ");
                }
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void ShowMaxQuantityProduct()
        {
            using (WarehouseDbContext db = new WarehouseDbContext())
            {
                string maxQuantity = db.Products
                    .OrderByDescending(p => p.Quantity)
                    .First().Name;
                Console.Write($"\nТовар з найбільшою кількістю: {maxQuantity}");
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void ShowMinQuantityProduct()
        {
            using (WarehouseDbContext db = new WarehouseDbContext())
            {
                string minQuantity = db.Products
                    .OrderBy(p => p.Quantity)
                    .First().Name;
                Console.Write($"\nТовар з найменшою кількістю: {minQuantity}");
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void ShowMaxCostPriceProduct()
        {
            using (WarehouseDbContext db = new WarehouseDbContext())
            {
                string maxCostPrice = db.Products
                    .OrderByDescending(p => p.СostPrice)
                    .First().Name;
                Console.Write($"\nТовар з найбільшою собівартістю: {maxCostPrice}");
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void ShowMinCostPriceProduct()
        {
            using (WarehouseDbContext db = new WarehouseDbContext())
            {
                string minCostPrice = db.Products
                    .OrderBy(p => p.СostPrice)
                    .First().Name;
                Console.Write($"\nТовар з найменшою собівартістю: {minCostPrice}");
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void ShowProductsByType()
        {
            Console.Write("\nУведіть тип товару: ");
            string typeInput = Console.ReadLine();

            using (WarehouseDbContext db = new WarehouseDbContext())
            {
                ProductType productType = db.ProductTypes
                    .Include(pt => pt.Products)
                    .SingleOrDefault(pt => pt.Name == typeInput);

                if (productType == null)
                {
                    Console.WriteLine($"Товарів типу \"{typeInput}\" не знайдено.");
                    return;
                }

                Console.Write($"Товари типу \"{typeInput}\": ");
                foreach (Product product in productType.Products)
                {
                    Console.Write(product.Name + ", ");
                }
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void ShowProductsBySupplier()
        {
            Console.Write("\nУведіть постачальника: ");
            string supplierInput = Console.ReadLine();

            using (WarehouseDbContext db = new WarehouseDbContext())
            {
                Supplier supplier = db.Suppliers
                    .Include(s => s.Products)
                    .SingleOrDefault(s => s.Name == supplierInput);

                if (supplier == null)
                {
                    Console.WriteLine($"Постачальника \"{supplierInput}\" не знайдено.");
                    return;
                }

                Console.Write($"Товари постачальника \"{supplierInput}\": ");
                foreach (Product product in supplier.Products)
                {
                    Console.Write(product.Name + ", ");
                }
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void ShowOldestProduct()
        {
            using (WarehouseDbContext db = new WarehouseDbContext())
            {
                string oldest = db.Products
                    .OrderBy(p => p.SupplyDate)
                    .First().Name;
                Console.Write($"\nНайзастарілий товар: {oldest}");
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void ShowAverageQuantityForEachProductType()
        {
            using (WarehouseDbContext db = new WarehouseDbContext())
            {
                var averages = db.ProductTypes
                    .Select(pt => new
                    {
                        ProductType = pt.Name,
                        AverageQuantity = pt.Products.Any() 
                        ? pt.Products.Average(p => p.Quantity) : 0
                    })
                    .ToList();

                Console.Write("Середня кількість товарів за кожним типом: ");
                foreach (var avg in averages)
                {
                    Console.Write($"{avg.ProductType} – {avg.AverageQuantity}, ");
                }
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }
    }
}