using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElsaApp.Data
{
    public class TenantInfo
    {
        private static readonly AsyncLocal<TenantInfo> _current = new AsyncLocal<TenantInfo>();

        public Guid Id { get; set; }
        public string Name { get; set; }

        public static TenantInfo Current
        {
            get => _current.Value;
            set => _current.Value = value;
        }




    }
}
