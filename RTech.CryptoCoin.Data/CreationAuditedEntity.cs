using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTech.CryptoCoin.Data;

public class CreationAuditedEntity<TKey> : IHasCreationTime where TKey : struct
{
    public TKey Id { get; set; }
    public DateTime CreationTime { get; set; }
}
