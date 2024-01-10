    using Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Entities.Concretes
    {
        public class UserOperationClaim:Entity<int>
        {
            public int UserId { get; set; }
            public int OperationClaimId { get; set; }
            public User User { get; set; }
            public OperationClaim OperationClaim { get; set; }
        }
    }
