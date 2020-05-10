using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe_Payment.Data
{
    public class StripeSetting
    {
        public string SecretKey { get; set; }
        public string PublishKey { get; set; }
    }
}
