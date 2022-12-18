using DalApi;
using DalList;


/// <summary>
/// the file implements all interfaces of all entities 
/// </summary>

namespace Dal
{
    sealed internal class DalList : IDal
    {   public IProduct Product => new DalProduct() { };
        public IOrder Order => new DalOrder() { };
        public IOrderItem OrderItem => new DalOrderItem() { };
        public static IDal instance { get; } = new DalList();
        private DalList() {}
    }
}
