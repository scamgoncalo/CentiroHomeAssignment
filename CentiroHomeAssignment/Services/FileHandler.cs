using CentiroHomeAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentiroHomeAssignment.Services
{
    public abstract class FileHandler
    {
        public abstract List<Order> LoadFiles(string directory, string pattern);
    }
}
