using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_7_No_Space_Left_On_Device
{
    public class FileInDirectory
    {
        public string Name { get;  }
        public int Size { get;  }
        public FileInDirectory(string[] input)
        {
            Name = input[1];
            Size = int.Parse(input[0]);
        }
    }
}
