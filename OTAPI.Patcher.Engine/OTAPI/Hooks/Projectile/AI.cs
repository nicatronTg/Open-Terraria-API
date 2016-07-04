﻿using NDesk.Options;
using OTAPI.Patcher.Engine.Extensions;
using OTAPI.Patcher.Engine.Modification;
using System.Linq;

namespace OTAPI.Patcher.Engine.Modifications.Hooks.Projectile
{
	public class AI : ModificationBase
	{
		public override string Description => "Hooking Projectile.AI()...";
		public override void Run(OptionSet options)
		{
			var vanilla = SourceDefinition.Type("Terraria.Projectile").Methods.Single(
				x => x.Name == "AI"
				&& x.Parameters.Count() == 0
			);

			var cbkBegin = ModificationDefinition.Type("OTAPI.Core.Callbacks.Terraria.Projectile").Method("AIBegin", parameters: vanilla.Parameters);
			var cbkEnd = ModificationDefinition.Type("OTAPI.Core.Callbacks.Terraria.Projectile").Method("AIEnd", parameters: vanilla.Parameters);

			vanilla.Wrap(cbkBegin, cbkEnd, true);
		}
	}
}