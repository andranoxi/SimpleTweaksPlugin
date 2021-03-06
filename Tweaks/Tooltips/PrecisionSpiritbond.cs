﻿using System.Collections.Generic;
using Dalamud.Game.Chat.SeStringHandling;
using Dalamud.Game.Chat.SeStringHandling.Payloads;
using ImGuiNET;
using static SimpleTweaksPlugin.Tweaks.TooltipTweaks;
using static SimpleTweaksPlugin.Tweaks.TooltipTweaks.ItemTooltip.TooltipField;

namespace SimpleTweaksPlugin {
    public partial class TooltipTweakConfig {
        public bool PrecisionSpiritbondTrailingZeros = true;
    }
}

namespace SimpleTweaksPlugin.Tweaks.UiAdjustment {
    public class PrecisionSpiritbond : SubTweak {
        public override string Name => "Precise Spiritbond";

        public override void OnItemTooltip(ItemTooltip tooltip, ItemInfo itemInfo) {
            var c = tooltip[SpiritbondPercent];
            if (c != null && !(c.Payloads[0] is TextPayload tp && tp.Text.StartsWith("?"))) {
                tooltip[SpiritbondPercent] = new SeString(new List<Payload>() { new TextPayload((itemInfo.SpiritBond / 100f).ToString(PluginConfig.TooltipTweaks.PrecisionSpiritbondTrailingZeros ? "F2" : "0.##") + "%") });
            }
        }

        public override void DrawConfig(ref bool hasChanged) {
            base.DrawConfig(ref hasChanged);
            if (!Enabled) return;
            ImGui.SameLine();
            hasChanged |= ImGui.Checkbox($"Trailing Zeros###{GetType().Name}TrailingZeros", ref PluginConfig.TooltipTweaks.PrecisionSpiritbondTrailingZeros);
        }
    }

}

