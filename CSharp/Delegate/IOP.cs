using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public delegate void OPDel(int a, int b);
namespace Delegate
{    
    internal interface IOP
    {        
        public OPDel Del { get; set; }
        public event OPDel DelEvent;
    }


    public class OP : IOP
    {
        public OPDel Del { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event OPDel DelEvent;
    }


}
