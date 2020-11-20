using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Domain.Interfaces;

namespace Warehouse.Infrastructure.Repositories
{
    public class CheckInOutRepository : ICheckInOutRepository
    {
        //TODO: Implement CheckInOutRepository
        private readonly Context _context;
        public CheckInOutRepository(Context context)
        {
            _context = context;
        }
        public int CheckInItem()
        {
            throw new NotImplementedException();
        }

        public IList<int> CheckOutByStructure()
        {
            throw new NotImplementedException();
        }

        public int CheckOutItem()
        {
            throw new NotImplementedException();
        }
    }
}
