using ORM_MiniApp.Contexts;
using ORM_MiniApp.Models;
using ORM_MiniApp.Repositories.Abstractions;
using ORM_MiniApp.Repositories.Implementations.Generic;

namespace ORM_MiniApp.Repositories.Implementations
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
