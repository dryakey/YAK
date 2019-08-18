using System;
using System.Collections.Generic;
using System.Text;

namespace Autin.Enum
{
    public class PowerSystem
    {
        public enum GeneratorStatus {
            GS_ON, 
            GS_OFF
        }

        public enum TransmitLineStatus {
            TLS_NORMAL,
            TLS_LOOSE,
            TLS_BLOCKED,
            TLS_HIGHLOAD
        }
    }
}
