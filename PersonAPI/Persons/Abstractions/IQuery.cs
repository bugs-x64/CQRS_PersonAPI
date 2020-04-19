using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons.Abstractions
{
    public interface IQuery<out TResult>
    {
        TResult Execute();
    }
}
