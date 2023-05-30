using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.API.Events
{
    public class Constants
    {
        public const string EventLogPath = @"C:\Git\Pets\Pet.Bus\";
        public const string EventLogName = @"PetEvent.txt";
        public static readonly string EventLogFullPath = $"{EventLogPath}{EventLogName}";
    }
}
