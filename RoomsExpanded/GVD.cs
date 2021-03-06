using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomsExpanded
{
    class GVD // = Game Version Dependencies
    {
        private static readonly bool UsesExpansion = true;

        public static void VersionAlert(bool expectBaseGame, string details = "")
        {
            if (expectBaseGame != UsesExpansion)
            {
                string message = "Resolve Vanilla/DLC version differences";
                if (!string.IsNullOrEmpty(details))
                    message += $" : {details}";
                throw new NotSupportedException(message);
            }
        }
    }
}
