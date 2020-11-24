using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Servibes.Shared.Communication
{
    internal sealed class ModuleRequestRegistration
    {
        public Type ReceiverType { get; set; }

        public Func<IServiceProvider, object, Task<object>> Action { get; set; }
    }
}
