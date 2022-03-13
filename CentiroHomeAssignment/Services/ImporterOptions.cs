using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Services
{
    // Class mirror of appSettings configuration regarding Importer Files
    public class ImporterOptions
    {
        public string Directory { get; set; } = String.Empty;
        public string Pattern { get; set; } = String.Empty;
    }
}
