using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace selfDPC.Android.Provision
{
    internal class ProvisioningSuccessActivity : Activity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var pputil = new PostProvisioningUtils(this);
            pputil.CompleteProvisioning();

            Finish();
        }
    }
}
