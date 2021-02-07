using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Domain.Interfaces
{
    public interface IReservationRepository
    {
        void GetAllReservations();
        void GetAllReservationByUserId();
        void GetReservationByItemId(int itemId);
        int AddReservation();
        int EditReservation(int reservationId);
        void DeleteReservation(int reservationId);
    }
}
