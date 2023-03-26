using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1.Model.Model
{
    /// <summary>
    /// Úroveň vzdělání
    /// </summary>
    public enum EducationEnum : int
    {
        /// <summary>
        /// Nespecifikováno
        /// </summary>
        NotSpecified,
        /// <summary>
        /// Žádné
        /// </summary>
        NoFormal,
        /// <summary>
        /// Základní
        /// </summary>
        Primary,
        /// <summary>
        /// Středoškolské
        /// </summary>
        Secondary,
        /// <summary>
        /// Bakalářské
        /// </summary>
        Bachelors,
        /// <summary>
        /// Magisterské
        /// </summary>
        Masters,
        /// <summary>
        /// Doktorát nebo vyšší
        /// </summary>
        DoctorateOrHigher
    }
}
