using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DatabaseSeed;
public class SeederOptions
{
    public string PrimeEmail { get; set; } = string.Empty;
    public string PrimeUsername { get; set; } = string.Empty;
    public string PrimePassword { get; set; } = string.Empty;

}
