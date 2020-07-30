using System;
using System.Collections.Generic;
using System.Text;
using TestCase.Domain.Abstract;

namespace TestCase.Domain
{
    public class Transaction : Entity
    {
        public TransactionStatus Status { get; set; }

        public TransactionType Type { get; set; }

        public string ClientName { get; set; }

        public decimal Amount { get; set; }

        public virtual void ChangeStatus(TransactionStatus status)
        {
            if (Status == status)
                return;

            Status = status;
        }

        public virtual void ChangeType(TransactionType type)
        {
            if (Type == type)
                return;

            Type = type;
        }
    }
}
