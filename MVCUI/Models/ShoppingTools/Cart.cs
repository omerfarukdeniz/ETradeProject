using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCUI.Models.ShoppingTools
{
    public class Cart
    {
        Dictionary<int, CartItem> _myCart;
        public Cart()
        {
            _myCart = new Dictionary<int, CartItem>();
        }

        public List<CartItem> MyCart
        {
            get
            {
                return _myCart.Values.ToList();
            }
        }

        public void AddToCart(CartItem item)
        {
            if (_myCart.ContainsKey(item.ID))
            {
                _myCart[item.ID].Amount += 1;
                return;
            }
            _myCart.Add(item.ID, item);
        }

        public void DeleteToCart(int id)
        {
            if (_myCart[id].Amount>1)
            {
                _myCart[id].Amount--;
                return;
            }
            _myCart.Remove(id);
        }

        public decimal? TotalPrice
        {
            get
            {
                return _myCart.Sum(x => x.Value.SubTotal);
            }
        }
    }
}