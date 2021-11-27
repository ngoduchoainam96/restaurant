using System;
using System.Collections.Generic;
using System.Linq;

namespace razor04.codebehide.Models {
  // Lớp Product
  public class Product {
    public int ID {set; get;}
    public String Name {set; get;}
    public String Desciption {set; get;}
    public Decimal Price {set; get;} = 0;
  }

  // Lớp tĩnh giả định DbContext
  public static class ProductContext {
    public static List<Product> products;
    static ProductContext() {
      // Khởi tạo một danh sách các sản phẩm mẫu
      products = new List<Product>() {
        new Product {
          ID=1,
          Name = "Cá kho chua cay",
          Price = 50000,
          Desciption = "Cá kho có màu đẹp và thịt chắc, thơm ngon đậm đà"
        },
        new Product {
          ID = 2,
          Name = "Bạch tuộc xào cay",
          Price = 220000,
          Desciption = "Bạch tuộc xào tươi ngon, được chế biến từ nguyên liệu sống và sạch 100%"
        },
        new Product {
          ID = 3,
          Name = "Cua rang me",
          Price = 100000,
          Desciption = "Cua được nuôi trong điều kiện lý tưởng, được chế biến theo cách tuyệt vời nhất"
        }
      };
    }

    // Tìm sản phẩm theo ID
    public static Product FindProductByID(int ID) {
      var p = from product in products 
              where product.ID == ID
              select product; 
      return p.FirstOrDefault();        
    }

  }

}