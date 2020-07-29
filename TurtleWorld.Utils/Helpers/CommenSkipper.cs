using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleWorld.Utils.Helpers
{
    internal static class CommenSkipper
    {
        /// <summary>
        /// skipp empty lines and lines starting with #
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ReadLine(TextReader stream)
        {
            string line;
            while (null != (line = stream.ReadLine()))
            {
                line = line.Trim();
                if (String.Empty != line
                    && !line.StartsWith("#")//trimmed from spaces already
                    )
                    return line;
            }
            return line;
        }
    }
}
