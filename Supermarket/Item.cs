using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    public enum Category
    { BEVERAGE = 1, FRUTS, VEGETABLES, BREAD, MILK_AND_DERIVATIVES, GARDEN, MEAT, SWEETS, SAUCES, FROUZEN, FISH, OTHER };

    public enum Packaging { Unit, Kg, Package };
    public class Item : IComparable<Item>
    {
        private char currency = '\u20AC';
        private int code;
        private string description;
        private bool onSale;
        private double price;
        private Category category;
        private Packaging packaging;
        private double stock;
        private int minStock;

        public Item(int code, string description, bool onSale, double price, Category category, Packaging packaging, double stock, int minStock)
        {
            if (code is default(int)) throw new ArgumentNullException("INVALID CODE");
            if (code <= 0) throw new ArgumentOutOfRangeException("CODE CAN'T BE NEGATIVE OR ZERO");
            if (description is null || description == "") throw new ArgumentNullException("DESCRIPTION CAN'T BE NULL");
            if (price <= 0) throw new ArgumentOutOfRangeException("PRICE CAN'T BE NEGATIVE OR ZERO");
            if (stock < minStock) throw new ArgumentOutOfRangeException("STOCK CAN'T BE LOWER THAN THE MINIMUM STOCK");
            if (minStock <= 0) throw new ArgumentOutOfRangeException("MINIMUM STOCK CAN'T BE LOWER THAN ZERO OR ZERO");

            this.code = code;
            this.description = description;
            this.onSale = onSale;
            this.price = Math.Round(price, 2);
            this.category = category;
            this.packaging = packaging;
            this.stock = stock;
            this.minStock = minStock;
        }

        public double Stock { get { return stock; } }
        public int MinStock { get { return minStock; } }
        public Packaging PackagingType { get { return packaging; } }
        public string Description { get { return description; } }
        public Category GetCategory { get { return category; } }
        public bool OnSale { get { return onSale; } }

        public double Price 
        {
            get 
            {
                double finalPrice = price;
                if (onSale) finalPrice -= (price * 10) / 100;
                return Math.Round(finalPrice);
            }
        }
        public static void UpdateStock(Item item, double qty)
        {
            if (qty < 0)//caso de compra
            {
                if ((qty * -1) > item.stock) throw new Exception("STACKUNDERFLOW. NOT ENOUGH STOCK");

                if (item.stock - (qty * -1) <= item.minStock) item.onSale = false;

                item.stock += qty;
            }
            else //caso restock
            {
                item.stock += qty;
                if (!item.onSale) item.onSale = true;

            }    
        }

        public int CompareTo(Item? other)
        {
            if (other is null) return 1;
            else return other.stock.CompareTo(this.stock);
        }

        public override string ToString()
        {
            StringBuilder item = new StringBuilder();
            item.Append($"CODE -> {this.code} DESCRIPTION -> {this.description}     CATEGORY -> {this.category}     STOCK -> {this.stock} MIN_STOCK -> {this.minStock}  PRICE -> {this.price} {this.currency}  ");
            if (onSale) item.Append($"ON SALE -> Y({this.Price}){this.currency}");
            else item.Append("ON SALE -> N");
            return item.ToString();
        }
    }
}
