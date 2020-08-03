using Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TestCase.Domain
{
    public class Transaction : Entity
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TransactionStatus Status { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
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
