using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E1Practice2
{
    interface IImageForm
    {
        EImage SrcImage { get; set; }
        EImage DestImage { get; set; }
    }
}
