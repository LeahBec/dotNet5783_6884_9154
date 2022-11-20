using BLApi;
using BO;
using DalApi;
using Dal;
namespace BlImplementation
{
    internal class BlCart : ICart
    {
        private IDal Dal = new Dal.DalList();
        public void Add(Cart c, int id)
        {
            throw new NotImplementedException();
        }

        public void CartConfirmation(Cart c, string customerName, string customerEmail, string customerAddress)
        {
            throw new NotImplementedException();
        }

        public Cart Update(Cart c, int id, double newAmount)
        {
            throw new NotImplementedException();
        }
    }
}
