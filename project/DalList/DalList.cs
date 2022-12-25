using DalApi;
using DalList;


/// <summary>
/// the file implements all interfaces of all entities 
/// </summary>

namespace Dal
{
    sealed internal  class DalList : IDal
    {
        //private Lazy<DalList> instance { get; } = new Lazy<DalList>();

        //private static readonly Lazy<DalList> lazy =
        //new Lazy<DalList>(() => new DalList());

        //public static DalList Instance { get { return lazy.Value; } }

        //private DalList()
        //{
        //}
        static private Lazy<DalList> instance { get; set; } = null;
        public static IDal Instance { get => GetInstance(); }

        public IProduct Product => new DalProduct() { };
        public IOrder Order => new DalOrder() { };
        public IOrderItem OrderItem => new DalOrderItem() { };
        private static readonly object padlock = new object();

        private DalList() { }
        public static DalList GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                    instance = new Lazy<DalList>();

                return instance.Value;
            }

        }
    }
}
