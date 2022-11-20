using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;

namespace BlImplementation
{
    sealed public class Bl : IBL
    {
        public ICart cart => new BlCart();
        public IOrder order => new BlOrder();
        public IOrderForList orderForList => new BlOrderForList();
        public IOrderItem orderItem => new BlOrderItem();
        public IOrderTracking orderTracking => new BlOrderTracking();
        public IProduct product => new BlProduct();
        public IProductItem productItem => new BlProductItem();
        public IProductForList productForList => new BlProductForList();
    }
}
