using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Application.DTOs.Common
{
    public abstract class BaseDto<T>
    {
        public int Id { get; set; }

    }
}
