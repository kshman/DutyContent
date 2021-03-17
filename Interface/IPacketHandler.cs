using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyContent.Interface
{
	interface IPacketHandler
	{
		void PacketHandler(string pid, byte[] message);
		void ZoneChanged(uint zone_id, string zone_name);
	}
}
