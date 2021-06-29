
using System;
using Microsoft.MetadirectoryServices;

namespace Mms_Metaverse
{
	/// <summary>
	/// Summary description for MVExtensionObject.
	/// </summary>
    public class MVExtensionObject : IMVSynchronization
    {
        public MVExtensionObject()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        void IMVSynchronization.Initialize ()
        {
            //
            // TODO: Add initialization logic here
            //
        }

        void IMVSynchronization.Terminate ()
        {
            //
            // TODO: Add termination logic here
            //
        }

        void IMVSynchronization.Provision (MVEntry mventry)
        {
            //https://docs.microsoft.com/en-us/previous-versions/windows/desktop/identity-lifecycle-manager/ms696008(v=vs.85)
            //CSEntry - Connector Space entry object
            string management_agent_name = ""; //Name of the management agent where objects from the multiverse are exported into
            string container = "OBJECT=user";
            string rdn = "CN=" + Guid.NewGuid().ToString();

            ConnectedMA HomeTenantMA = mventry.ConnectedMAs[management_agent_name];
            ReferenceValue dn = HomeTenantMA.EscapeDNComponent(rdn).Concat(container);

            int numConnectors = HomeTenantMA.Connectors.Count;

            if (numConnectors == 0)
            {
                CSEntry csentry = HomeTenantMA.Connectors.StartNewConnector("user");
                csentry.DN = dn;
                csentry["id"].StringValue = Guid.NewGuid().ToString();
                csentry.CommitNewConnector();
            }
            else if (numConnectors == 1) { }
            else
            {
                throw (new UnexpectedDataException("multiple connectors:" + numConnectors.ToString()));
            }
        }	

        bool IMVSynchronization.ShouldDeleteFromMV (CSEntry csentry, MVEntry mventry)
        {
            //
            // TODO: Add MV deletion logic here
            //
            throw new EntryPointNotImplementedException();
        }
    }
}
