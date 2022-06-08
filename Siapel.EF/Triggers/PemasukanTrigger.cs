using EntityFrameworkCore.Triggered;
using Siapel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Siapel.EF.Triggers
{
    public class PemasukanTrigger : IBeforeSaveTrigger<Pemasukan>
    {
        readonly SiapelDbContext _siapelDbContext;

        public PemasukanTrigger(SiapelDbContext siapelDbContext)
        {
            _siapelDbContext = siapelDbContext;
        }

        public Task BeforeSave(ITriggerContext<Pemasukan> context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
