using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdvancedProgramming.Models
{
    public class AuditLog
    {
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string User { get; set; }

        public void LogAction(string action, string user)
        {
            AuditLog log = new AuditLog()
            {
                Action = action,
                Timestamp = DateTime.Now,
                User = user
            };

            using (StreamWriter writer = File.AppendText("auditlog.txt"))
            {
                writer.WriteLine($"{DateTime.Now} - Action: {action}, User: {user}");
            }
        }
    }

}
