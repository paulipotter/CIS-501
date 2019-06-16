using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class OutputConsole
    {
        Model model;

        public OutputConsole(Model model)
        {
            this.model = model;
        }

        public void HandleModelUpdate(object sender, string update)
        {
            Console.WriteLine("[Observer!] " + update);
        }
    }
}
