using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1.Model.Model
{
    /// <summary>
    /// Pohlaví
    /// </summary>
    public enum GenderEnum : int
    {
        /// <summary>
        /// Nespecifikováno
        /// </summary>
        NotSpecified,
        /// <summary>
        /// Muž
        /// </summary>
        Male,
        /// <summary>
        /// Žena
        /// </summary>
        Female,
        /// <summary>
        /// Jiné
        /// </summary>
        Other
    }
}
