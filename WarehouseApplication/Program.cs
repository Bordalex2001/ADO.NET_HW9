using ADO.NET_HW9.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_HW9
{
    public class Program
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
                Console.WriteLine("12. Показати інформацію про постачальника з найбільшою кількістю товарів");
                Console.WriteLine("13. Показати інформацію про постачальника з найменшою кількістю товарів");
                Console.WriteLine("14. Показати інформацію про тип з найбільшою кількістю товарів");
                Console.WriteLine("15. Показати інформацію про тип з найменшою кількістю товарів");
                Console.WriteLine("16. Додати новий товар");
                Console.WriteLine("17. Додати новий тип товарів");
                Console.WriteLine("18. Додати нового постачальника");
                Console.WriteLine("19. Змінити товар");
                Console.WriteLine("20. Змінити тип товарів");
                Console.WriteLine("21. Змінити постачальника");
                Console.WriteLine("22. Видалити товар");
                Console.WriteLine("23. Видалити тип товарів");
                Console.WriteLine("24. Видалити постачальника");
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
                    case 12:
                        ShowSupplierWithMaxQuantityOfProducts();
                        break;
                    case 13:
                        ShowSupplierWithMinQuantityOfProducts();
                        break;
                    case 14:
                        ShowProductTypeWithMaxQuantityOfProducts();
                        break;
                    case 15:
                        ShowProductTypeWithMinQuantityOfProducts();
                        break;
                    case 16:
                        AddProduct();
                        break;
                    case 17:
                        AddProductType();
                        break;
                    case 18:
                        AddSupplier();
                        break;
                    case 19:
                        UpdateProduct(); 
                        break;
                    case 20:
                        UpdateProductType();
                        break;
                    case 21:
                        UpdateSupplier();
                        break;
                    case 22:
                        DeleteProduct();
                        break;
                    case 23:
                        DeleteProductType();
                        break;
                    case 24:
                        DeleteSupplier();
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
            using (WarehouseDbContext? db = new())
            {
                List<Product> products = db.Products.ToList();

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
            using (WarehouseDbContext? db = new())
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
            using (WarehouseDbContext? db = new())
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
            using (WarehouseDbContext? db = new())
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
            using (WarehouseDbContext? db = new())
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
            using (WarehouseDbContext? db = new())
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
            using (WarehouseDbContext? db = new())
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
            Console.Write("\nУведіть тип товарів: ");
            string? typeInput = Console.ReadLine();

            using (WarehouseDbContext? db = new())
            {
                ProductType? productType = db.ProductTypes
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
            string? supplierInput = Console.ReadLine();

            using (WarehouseDbContext? db = new())
            {
                Supplier? supplier = db.Suppliers
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
            using (WarehouseDbContext? db = new())
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
            using (WarehouseDbContext? db = new())
            {
                var averages = db.ProductTypes
                    .Select(pt => new
                    {
                        ProductType = pt.Name,
                        AverageQuantity = pt.Products.Any()
                        ? pt.Products.Average(p => p.Quantity) : 0
                    })
                    .ToList();

                Console.Write("\nСередня кількість товарів за кожним типом: ");
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

        static void ShowSupplierWithMaxQuantityOfProducts()
        {
            using (WarehouseDbContext? db = new())
            {
                var supplier = db.Suppliers
                    .Select(s => new
                    {
                        Supplier = s,
                        TotalQuantity = s.Products.Sum(p => p.Quantity)
                    })
                    .OrderByDescending(s => s.TotalQuantity)
                    .First();

                Console.WriteLine("\nПостачальник з найбільшою кількістю товарів:");
                Console.WriteLine($"\nНазва: {supplier.Supplier.Name}");
                Console.WriteLine($"Загальна кількість товарів: {supplier.TotalQuantity}");
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void ShowSupplierWithMinQuantityOfProducts()
        {
            using (WarehouseDbContext? db = new())
            {
                var supplier = db.Suppliers
                    .Select(s => new
                    {
                        Supplier = s,
                        TotalQuantity = s.Products.Sum(p => p.Quantity)
                    })
                    .Where(s => s.TotalQuantity > 0)
                    .OrderBy(s => s.TotalQuantity)
                    .First();

                Console.WriteLine("\nПостачальник з найменшою кількістю товарів:");
                Console.WriteLine($"\nНазва: {supplier.Supplier.Name}");
                Console.WriteLine($"Загальна кількість товарів: {supplier.TotalQuantity}");
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void ShowProductTypeWithMaxQuantityOfProducts()
        {
            using (WarehouseDbContext? db = new())
            {
                var productType = db.ProductTypes
                    .Select(pt => new
                    {
                        ProductType = pt,
                        TotalQuantity = pt.Products.Sum(p => p.Quantity)
                    })
                    .OrderByDescending(pt => pt.TotalQuantity)
                    .First();

                Console.WriteLine("\nТип з найбільшою кількістю товарів:");
                Console.WriteLine($"\nНазва: {productType.ProductType.Name}");
                Console.WriteLine($"Загальна кількість товарів: {productType.TotalQuantity}");
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void ShowProductTypeWithMinQuantityOfProducts()
        {
            using (WarehouseDbContext? db = new())
            {
                var productType = db.ProductTypes
                    .Select(pt => new
                    {
                        ProductType = pt,
                        TotalQuantity = pt.Products.Sum(p => p.Quantity)
                    })
                    .Where(pt => pt.TotalQuantity > 0)
                    .OrderBy(pt => pt.TotalQuantity)
                    .First();

                Console.WriteLine("\nТип з найменшою кількістю товарів:");
                Console.WriteLine($"\nНазва: {productType.ProductType.Name}");
                Console.WriteLine($"Загальна кількість товарів: {productType.TotalQuantity}");
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void AddProduct()
        {
            using (WarehouseDbContext? db = new())
            {
                Console.Write("\nУведіть назву товару: ");
                string name = Console.ReadLine();

                Console.Write("Уведіть ID типу товарів: ");
                int productTypeId = int.Parse(Console.ReadLine());

                Console.Write("Уведіть ID постачальника: ");
                int supplierId = int.Parse(Console.ReadLine());

                Console.Write("Уведіть кількість: ");
                int quantity = int.Parse(Console.ReadLine());

                Console.Write("Уведіть собівартість: ");
                decimal costPrice = decimal.Parse(Console.ReadLine());

                Product? product = new()
                {
                    Name = name,
                    ProductTypeId = productTypeId,
                    SupplierId = supplierId,
                    Quantity = quantity,
                    СostPrice = costPrice,
                    SupplyDate = DateTime.Now.Date
                };

                db.Products.Add(product);
                db.SaveChanges();

                Console.WriteLine("\nТовар додано успішно.");
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }
        
        static void AddProductType()
        {
            using (WarehouseDbContext? db = new())
            {
                Console.Write("\nУведіть тип товарів: ");
                string name = Console.ReadLine();

                ProductType? productType = new()
                {
                    Name = name
                };

                db.ProductTypes.Add(productType);
                db.SaveChanges();

                Console.WriteLine("\nТип товарів додано успішно.");
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }
        
        static void AddSupplier()
        {
            using (WarehouseDbContext? db = new())
            {
                Console.Write("\nУведіть постачальника: ");
                string name = Console.ReadLine();

                Supplier? supplier = new()
                {
                    Name = name
                };

                db.Suppliers.Add(supplier);
                db.SaveChanges();

                Console.WriteLine("\nПостачальника додано успішно.");
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void UpdateProduct()
        {
            using (WarehouseDbContext? db = new())
            {
                Console.Write("\nУведіть ID товару для оновлення: ");
                int productId = int.Parse(Console.ReadLine());

                Product? product = db.Products.Find(productId);

                if (product != null)
                {
                    Console.Write("\nУведіть нову назву товару (залишіть поле порожнім, щоб перейти до наступного): ");
                    string name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        product.Name = name;
                    }

                    Console.Write("Уведіть нову кількість (залишіть поле порожнім, щоб перейти до наступного): ");
                    string quantityInput = Console.ReadLine();
                    if (int.TryParse(quantityInput, out int quantity))
                    {
                        product.Quantity = quantity;
                    }

                    Console.Write("Уведіть нову собівартість: ");
                    string costPriceInput = Console.ReadLine();
                    if (decimal.TryParse(costPriceInput, out decimal costPrice))
                    {
                        product.СostPrice = costPrice;
                    }

                    db.SaveChanges();
                    Console.WriteLine("\nТовар оновлено успішно.");
                }
                else
                {
                    Console.WriteLine("\nТовару з таким ID не знайдено.");
                }
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void UpdateProductType()
        {
            using (WarehouseDbContext? db = new())
            {
                Console.Write("\nУведіть ID типу товарів для оновлення: ");
                int productTypeId = int.Parse(Console.ReadLine());

                ProductType? productType = db.ProductTypes.Find(productTypeId);

                if (productType != null)
                {
                    Console.Write("\nУведіть нову назву типу товарів: ");
                    string name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        productType.Name = name;
                    }

                    db.SaveChanges();
                    Console.WriteLine("\nТип товарів оновлено успішно.");
                }
                else
                {
                    Console.WriteLine("\nТипу товарів з таким ID не знайдено.");
                }
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void UpdateSupplier()
        {
            using (WarehouseDbContext? db = new())
            {
                Console.Write("\nУведіть ID постачальника для оновлення: ");
                int supplierId = int.Parse(Console.ReadLine());

                Supplier? supplier = db.Suppliers.Find(supplierId);

                if (supplier != null)
                {
                    Console.Write("\nУведіть нову назву постачальника: ");
                    string name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        supplier.Name = name;
                    }

                    db.SaveChanges();
                    Console.WriteLine("\nПостачальника оновлено успішно.");
                }
                else
                {
                    Console.WriteLine("\nПостачальника з таким ID не знайдено.");
                }
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void DeleteProduct()
        {
            using (WarehouseDbContext? db = new())
            {
                Console.Write("\nУведіть ID товару для видалення: ");
                int productId = int.Parse(Console.ReadLine());

                Product? product = db.Products.Find(productId);

                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                    Console.WriteLine("\nТовар видалено успішно.");
                }
                else
                {
                    Console.WriteLine("\nТовару з таким ID не знайдено.");
                }
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void DeleteProductType()
        {
            using (WarehouseDbContext? db = new())
            {
                Console.Write("\nУведіть ID типу товарів для видалення: ");
                int productTypeId = int.Parse(Console.ReadLine());

                ProductType? productType = db.ProductTypes.Find(productTypeId);

                if (productType != null)
                {
                    db.ProductTypes.Remove(productType);
                    db.SaveChanges();
                    Console.WriteLine("\nТип товарів видалено успішно.");
                }
                else
                {
                    Console.WriteLine("\nТипу товарів з таким ID не знайдено.");
                }
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }

        static void DeleteSupplier()
        {
            using (WarehouseDbContext? db = new())
            {
                Console.Write("\nУведіть ID постачальника для видалення: ");
                int supplierId = int.Parse(Console.ReadLine());

                Supplier? supplier = db.Suppliers.Find(supplierId);

                if (supplier != null)
                {
                    db.Suppliers.Remove(supplier);
                    db.SaveChanges();
                    Console.WriteLine("\nПостачальника видалено успішно.");
                }
                else
                {
                    Console.WriteLine("\nПостачальника з таким ID не знайдено.");
                }
            }

            Console.WriteLine();
            Console.WriteLine("\nЩоб продовжити, натисніть будь-яку клавішу...");
            Console.ReadKey();
            LoadMenu();
        }
    }
}