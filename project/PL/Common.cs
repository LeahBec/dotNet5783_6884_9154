using BO;
using DalFacade.DO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public static class Common
    {
        public static PO.Cart ConvertToPoCart(BO.Cart Bc, PO.Cart Pc)
        {

            Pc.CustomerAddress = Bc.CustomerAddress;
            Pc.CustomerEmail = Bc.CustomerEmail;
            Pc.CustomerName = Bc.CustomerName;
            //Items = Pp.items.ForEach(i => ConvertToPoItem(i)).ToList(),
            Pc.Items = convertItemsToPOOI(Bc.items);
            Pc.TotalPrice = Bc.TotalPrice;

            return Pc;
        }
        public static List<PO.OrderItem> convertItemsToPOOI(List<BO.OrderItem> oil)
        {
            List<PO.OrderItem> returnlist = new();
            oil.ForEach(item =>
            {
                PO.OrderItem item2 = new()
                {
                    ID = item.ID,
                    Amount = item.Amount,

                    Price = item.Price,
                    ProductID = item.ProductID,
                    ProductName = item.ProductName,
                    TotalPrice = item.TotalPrice
                };
                returnlist.Add(item2);
            });
            return returnlist;
        }
        public static BO.Cart ConvertToBoCart(PO.Cart Bp)
        {
            BO.Cart item = new()
            {
                CustomerAddress = Bp.CustomerAddress,
                CustomerEmail = Bp.CustomerEmail,
                CustomerName = Bp.CustomerName,
                //Items = Pp.items.ForEach(i => ConvertToPoItem(i)).ToList(),
                items = convertItemsToBOOI(Bp.Items),
                TotalPrice = Bp.TotalPrice,
            };
            return item;
        }

        public static List<BO.OrderItem> convertItemsToBOOI(List<PO.OrderItem> oil)
        {
            List<BO.OrderItem> returnlist = new();
            oil.ForEach(item =>
            {
                BO.OrderItem item2 = new()
                {
                    ID = item.ID,
                    Amount = item.Amount,

                    Price = item.Price,
                    ProductID = item.ProductID,
                    ProductName = item.ProductName,
                    TotalPrice = item.TotalPrice
                };
                returnlist.Add(item2);
            });
            return returnlist;
        }
        public static PO.OrderItem ConvertToPoItem(BO.OrderItem Pp)
        {
            PO.OrderItem item = new()
            {
                ID = Pp.ID,
                ProductID = Pp.ProductID,
                ProductName = Pp.ProductName,
                Price = Pp.Price,
                TotalPrice = Pp.TotalPrice,
                Amount = Pp.Amount
            };
            return item;
        }
        public static PO.Product ConvertToPoPro(BO.Product Pp)
        {
            PO.Product item = new()
            {
                ID = Pp.ID,
                Name = Pp.Name,
                Price = Pp.Price,
                Category = (BO.Category)(eCategory)Pp.Category,
                inStock = Pp.inStock
            };
            return item;
        } 

        public static PO.Order ConvertToPoOrder(BO.Order Bo)
        {
            PO.Order item = new()
            {
                ID = Bo.ID,
                CustomerName = Bo.CustomerName,
                CustomerAddress = Bo.CustomerAddress,
                CustomerEmail = Bo.CustomerEmail,
                DeiveryDate = (DateTime?)Bo?.DeiveryDate,
                ShipDate = (DateTime?)Bo.ShipDate,
                OrderDate = (DateTime?)Bo?.OrderDate
            };
            return item;
        }

        public static ObservableCollection<PO.OrderForList> convertListOrder(IEnumerable<BO.OrderForList> list2, ObservableCollection<PO.OrderForList> List_o)
        {
            /*list2.ForEach(item =>
            {
                List_o.Add(ConvertToPoOrder(item));
            });*/
            PO.OrderForList i = new PO.OrderForList();
            foreach (BO.OrderForList tmp in list2)
            {
                i = ConvertToPoOrderForList(tmp);
                List_o.Add(i);
            }
            return List_o;
        }
        public static PO.OrderForList ConvertToPoOrderForList(BO.OrderForList boo)
        {
            PO.OrderForList returnOrder = new()
            {
                ID = boo.ID,
                CustomerName = boo.CustomerName,
                TotalPrice = boo.TotalPrice,
                AmountOfItems = boo.AmountOfItems,
                Status = boo.Status,
            };
            return returnOrder;
        }
        public static PO.OrderItem converToPoOi(BO.OrderItem oi)
        {
            PO.OrderItem item = new()
            {
                Amount = oi.Amount,
                ID = oi.ID,
                ProductID = oi.ProductID,
                Price = oi.Price,
                ProductName = oi.ProductName
            };
            return item;
        }
        public static List<PO.OrderItem> convertToPoOiList(List<BO.OrderItem> loi)
        {
            List<PO.OrderItem> returnList = new();
            foreach (BO.OrderItem oi in loi)
            {
                returnList.Add(converToPoOi(oi));
            }
            return returnList;
        }

        public static BO.OrderItem converToBoOi(PO.OrderItem oi)
        {
            BO.OrderItem item = new()
            {
                Amount = oi.Amount,
                ID = oi.ID,
                ProductID = oi.ProductID,
                Price = oi.Price,
                ProductName = oi.ProductName,
                TotalPrice = oi.TotalPrice,
            };
            return item;
        }
        public static List<BO.OrderItem> convertToBoOiList(List<PO.OrderItem> loi)
        {
            List<BO.OrderItem> returnList = new();
            foreach (PO.OrderItem oi in loi)
            {
                returnList.Add(converToBoOi(oi));
            }
            return returnList;
        }


        /*        public static PO.Cart ConvertToPoCart(BO.Cart ca)
                {
                    PO.Cart item = new()
                    {

                        CustomerAddress = ca.CustomerAddress,
                        CustomerEmail = ca.CustomerEmail,
                        CustomerName = ca.CustomerName,
                        TotalPrice = ca.TotalPrice,
                    };
                    item.Items = convertToPoOiList(ca.items);
                    return item;
                }*/
        public static ObservableCollection<PO.ProductForList> convertList(ObservableCollection<PO.ProductForList> List_p, IEnumerable<BO.ProductForList> list1)
        {
            PO.ProductForList i = new PO.ProductForList();
            foreach (BO.ProductForList tmp in list1)
            {
                i = ConvertToPo(tmp);
                List_p.Add(i);
            }
            return List_p;
        }
        public static PO.ProductForList ConvertToPo(BO.ProductForList Bp)
        {
            PO.ProductForList item = new()
            {
                ID = Bp.ID,
                Name = Bp.Name,
                Price = Bp.Price,
                Category = (eCategory)Bp.Category
            };
            return item;
        }

        /*public static PO.Product ConvertToPoPro(BO.Product Pp)
        {
            PO.Product item = new()
            {
                ID = Pp.ID,
                Name = Pp.Name,
                Price = Pp.Price,
                Category = (BO.Category)(eCategory)Pp.Category,
                inStock = Pp.inStock
            };
            return item;
        }*/
        public static BO.Order ConvertToBo(PO.Order Op)
        {
            BO.Order item = new()
            {
                ID = Op.ID,
                CustomerName = Op.CustomerName,
                CustomerEmail = Op.CustomerEmail,
                CustomerAddress = Op.CustomerAddress,
                OrderDate = (DateTime?)Op.OrderDate,
                ShipDate = (DateTime?)Op.ShipDate,
                DeiveryDate = (DateTime?)Op.DeiveryDate
            };
            return item;
        }

        public static PO.OrderForList ConvertPFLToP(PO.Order Pp)
        {
            PO.OrderForList item = new()
            {
                ID = Pp.ID,
                CustomerName = Pp.CustomerName,
                Status = Pp.Status,
                AmountOfItems = Pp.Items.Count,
                TotalPrice = Pp.TotalPrice,
            };
            return item;
        }
        public static BO.Product ConvertToBo(PO.Product Pp)
        {
            BO.Product item = new()
            {
                ID = Pp.ID,
                Name = Pp.Name,
                Price = Pp.Price,
                Category = (BO.Category)(eCategory)Pp.Category,
                inStock = Pp.inStock
            };
            return item;
        }

        public static PO.ProductForList ConvertPFLToP(PO.Product p)
        {
            PO.ProductForList item = new();
            item.ID = p.ID;
            item.Name = p.Name;
            item.Price = p.Price;
            item.Category = (DalFacade.DO.eCategory)p.Category;
            return item;
        }

        public static PO.Product ConvertPToPFL(PO.ProductForList p)
        {
            PO.Product item = new();
            item.ID = p.ID;
            item.Name = p.Name;
            item.Price = p.Price;
            item.Category = (Category)p.Category;

            return item;
        }
        public static BO.ProductForList ConvertPFLToB(BO.Product p)
        {
            BO.ProductForList item = new();
            item.ID = p.ID;
            item.Name = p.Name;
            item.Price = p.Price;
            item.Category = p.Category;
            return item;
        }


    }
}
