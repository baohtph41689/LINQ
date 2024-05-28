using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Channels;

class  Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string[] Color { get; set; }
    public int Brand { get; set; }

    public Product(int id , string name , double price , string[] color , int brand) 
    {
        this.Id = id;
        this.Name = name;
        this.Price = price;
        this.Color = color;
        this.Brand = brand;
    }
    override public string ToString() => $"{Id}, {Name}, {Price}, {Brand}, {string.Join(",",Color)}  ";
  



}

public class BrandPro
{
    public int Id { get; set; }
    public string name { get; set; }
}
internal class Program
{
    private static void Main(string[] args)
    {
        var brands = new List<BrandPro>()
        {
            new BrandPro{Id = 1 , name = "Cong ty ABC"},
            new BrandPro{Id = 2 , name = "Cong ty CDE"},
            new BrandPro{Id = 3 , name = "Cong ty EGH"},
            new BrandPro{Id = 4 , name = "Cong ty HFE"}
        };

        var products = new List<Product>()
{
    new Product(1, "Bàn trà",    400, new string[] {"Xám", "Xanh"},         2),
    new Product(2, "Tranh treo", 400, new string[] {"Vàng", "Xanh"},        1),
    new Product(3, "Đèn trùm",   500, new string[] {"Trắng"},               3),
    new Product(4, "Bàn học",    200, new string[] {"Trắng", "Xanh"},       1),
    new Product(5, "Túi da",     300, new string[] {"Đỏ", "Đen", "Vàng"},   2),
    new Product(6, "Giường ngủ", 500, new string[] {"Trắng"},               2),
    new Product(7, "Tủ áo",      600, new string[] {"Trắng"},               3),
};
        //foreach (var item in products)
        //{
        //    Console.WriteLine(item);
        //}
        // min max sum avg
        //int[] numbers= {1,2,3,4,5,6,7,8,9};
        //Console.WriteLine(numbers.Where(n => n%2 == 0).Max());
        //Console.WriteLine(numbers.Where(n => n % 2 == 0).Min());
        //Console.WriteLine(numbers.Sum());
        //Console.WriteLine(products.Max(p=>p.Price));

        // cu phap truy van bang LINQ 
        //var query = from p in products  // from xac dinh nguon data ma truy van thuc hien
        //            where p.Price == 400  // dieu kien truy van 
        //            select p; // chi ra cac du lieu dc lay ra cua cau lenh LINQ
        //foreach (var  pr in query)
        //{
        //    Console.WriteLine(pr);
        //}

        // lay ra sp gia < 500 co mau la mau xnah
        var qr = from pr in products
                     from color in pr.Color

                     where pr.Price <= 500 && color == "Xanh"
                     orderby pr.Price descending
                     select new{
                         NamePro = pr.Name,
                         PricePro = pr.Price,
                         ColorPro = pr.Color
                     };

        qr.ToList().ForEach(info => {
            Console.WriteLine($"Name:{info.NamePro} - Price:{info.PricePro}");
            Console.WriteLine(string.Join(",",info.ColorPro));
        });
       
        // trong doi tuong product co method select() select nhan tham so la 1 delegate 
        // tra ve 1 tap hop ma 1 phan tu trong tap hop la 1 mang chuoi
        //var kq = products.Select(
        //     (p) =>
        //     {
        //         return p.Name;

        //     }
        //     );
        //where nhan tham so la delegate 
        //var w = products.Where(
        //    (p) =>
        //    {
        //        //return p.Name.Contains("B");
        //        return p.Brand == 2;
        //    }
        //    );

        // select many tra ve 1 tap hop tung phan tu 1
        //var kq = products.SelectMany(
        // (p) =>
        // {
        //     return p.Color;

        // }
        // );

        // join 
        //var kq = products.Join(brands, p => p.Brand, b => b.Id, (p, b) =>
        //{
        //    return new
        //    {
        //        ten = p.Name,
        //        thuongHieu = b.name
        //    };
        //});
        //foreach (var item in kq)
        //{
        //    Console.WriteLine(item);
        //}

        // groupjoin
        //var kq = brands.GroupJoin(products, b => b.Id, p => p.Brand,
        //    (b, p) =>
        //{
        //    return new
        //    {
        //        brands = b.name,
        //        ProducsNew = p
        //    };
        //});

        //foreach (var item in kq)
        //{
        //    Console.WriteLine(item.brands);
        //    //Console.WriteLine(item.ProducsNew);
        //    foreach (var pro in item.ProducsNew)
        //    {
        //        Console.WriteLine(pro);
        //    }
        //}
        //Console.WriteLine("===============");
        //// TAKE (lay ra 1 so san pham dau tien)
        //products.Take(2).ToList().ForEach(p => Console.WriteLine(p));

        //Console.WriteLine("===============");
        //// Skip loai bo phan tu dau tien lay phan tu con lai
        //products.Skip(2).ToList().ForEach(p => Console.WriteLine(p));

        //// orderby (tang dan ) , orderbyDescending(giam dan)
        //Console.WriteLine("===============");
        //products.OrderBy(p=>p.Price).ToList().ForEach(p => Console.WriteLine(p));
        //Console.WriteLine("===============");
        //products.OrderByDescending(p => p.Id).ToList().ForEach(p => Console.WriteLine(p));

        ////Reverse
        //int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        //numbers.Reverse().ToList().ForEach(p => Console.WriteLine(p));

        ////groupBy tra ve 1 tap hop theo 1 nhom nao do co cx gia tri
        //Console.WriteLine("===============");
        //var Result = products.GroupBy(p => p.Price);
        //foreach (var item in Result)
        //{
        //    Console.WriteLine(item.Key);
        //    foreach (var p in item)
        //    {
        //        Console.WriteLine(p);
        //    }
        //}
        //Console.WriteLine("===============");
        ////distinct ( loai bo nhung phan tu co cung gia tri lay 1 gia tri duy nhat
        //products.SelectMany(p => p.Color).Distinct().ToList().ForEach(p => Console.WriteLine(p));

        // Singer or singerordefault (kiem tra ca phan tu thoa man 1 dieu kien logic nao do thi tra 1 gia tri duy nhat)
        //Console.WriteLine("===============");
        //var result = products.Single(p => p.Price == 600);
        //Console.WriteLine(result);
        // singerordefault nhu singer khac o cho khi ko tim thay phan tu nao thoa man dk thi tra ve null

        //any tra ve true khi thoa man dk logic nao do
        //Console.WriteLine("===============");
        //var result = products.Any(p => p.Price == 600);
        //Console.WriteLine(result);

        // all cx tra ve true hoac fale tat ca cac phan tu thao man dk logic
        //Console.WriteLine("===============");
        //var result = products.All(p => p.Price >=200);
        //Console.WriteLine(result);

        // count dem so luong phan tu
        //Console.WriteLine("===============");
        //var result = products.Count(p => p.Price > 300);
        //Console.WriteLine(result);

        // in ra ten sp ten thuong hieu trong khoang (300-400) 
        // gia giam dan
        //products.Where(p => p.Price >= 300 && p.Price <= 400)
        //        .OrderByDescending(p => p.Price)
        //        .Join(brands, p => p.Brand, b => b.Id, (p, b) =>
        //        {
        //            return new
        //            {
        //                namePro = p.Name,
        //                nameBr = b.name,
        //                pricePro = p.Price
        //            };
        //        }).ToList().ForEach(info => Console.WriteLine($"Ten SP:{info.namePro} - Ten Br:{info.nameBr} - Gia:{info.pricePro}") );
    }
}