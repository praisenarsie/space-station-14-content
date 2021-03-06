﻿using Content.Server.GameObjects.EntitySystems;
using Content.Shared.Maps;
using SS14.Server.GameObjects.EntitySystems;
using SS14.Shared.Interfaces.GameObjects;
using SS14.Shared.Interfaces.Map;
using SS14.Shared.IoC;
using SS14.Shared.Map;

namespace Content.Server.GameObjects.Components.Interactable.Tools
{
    public class CrowbarComponent : ToolComponent, IAfterAttack
    {
#pragma warning disable 649
        [Dependency] private readonly ITileDefinitionManager _tileDefinitionManager;
        [Dependency] private readonly IEntitySystemManager _entitySystemManager;
#pragma warning restore 649

        /// <summary>
        /// Tool that can be used to crowbar things apart, such as deconstructing
        /// </summary>
        public override string Name => "Crowbar";

        public CrowbarComponent()
        {
            IoCManager.InjectDependencies(this);
        }

        public void AfterAttack(AfterAttackEventArgs eventArgs)
        {
            var tile = eventArgs.ClickLocation.Grid.GetTile(eventArgs.ClickLocation);
            var tileDef = (ContentTileDefinition) tile.TileDef;
            if (tileDef.CanCrowbar)
            {
                var underplating = _tileDefinitionManager["underplating"];
                eventArgs.ClickLocation.Grid.SetTile(eventArgs.ClickLocation, underplating.TileId);
               _entitySystemManager.GetEntitySystem<AudioSystem>().Play("/Audio/items/crowbar.ogg", Owner);
            }
        }
    }
}
