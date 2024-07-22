using JetBrains.Annotations;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle;
using LBoL.Core.Cards;
using LBoL.Core;
using LBoL.EntityLib.EnemyUnits.Character;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using System;
using System.Collections.Generic;
using System.Text;
using LBoL.Core.Units;

namespace SanaeStar
{
    public sealed class SanaeStar_Card_Rain_Def : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(SanaeStar_Rain);
        }

        public override CardImages LoadCardImages()
        {
            CardImages cardImages = new CardImages(BepinexPlugin.embeddedSource);
            cardImages.AutoLoad(this, ".png", "", false);
            return cardImages;
        }

        public override LocalizationOption LoadLocalization()
        {
            return BepinexPlugin.SanaeStarCardsBatchloc.AddEntity(this);
        }

        public override CardConfig MakeConfig()
        {
            var cardConfig = new CardConfig(
                Index: BepinexPlugin.sequenceTable.Next(typeof(CardConfig)),
                Id: "",
                Order: 10,
                AutoPerform: true,
                Perform: new string[0][],
                GunName: "",
                GunNameBurst: "",
                DebugLevel: 0,
                Revealable: false,

                IsPooled: false,
                FindInBattle: false,

                HideMesuem: false,
                IsUpgradable: false,
                Rarity: Rarity.Common,
                Type: CardType.Status,
                TargetType: null,
                Colors: new List<ManaColor> { ManaColor.Blue },
                IsXCost: false,
                Cost: new ManaGroup() { },
                UpgradedCost: null,
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 2,
                UpgradedValue1: null,
                Value2: 5,
                UpgradedValue2: null,
                Mana: null,
                UpgradedMana: null,
                Scry: null,
                UpgradedScry: null,

                ToolPlayableTimes: null,

                Loyalty: null,
                UpgradedLoyalty: null,
                PassiveCost: null,
                UpgradedPassiveCost: null,
                ActiveCost: null,
                UpgradedActiveCost: null,
                UltimateCost: null,
                UpgradedUltimateCost: null,

                Keywords: Keyword.Forbidden | Keyword.Ethereal,
                UpgradedKeywords: Keyword.None,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() { },
                UpgradedRelativeEffects: new List<string>() { },
                RelativeCards: new List<string>() { },
                UpgradedRelativeCards: new List<string>() { },

                Owner: "SanaeStarMod",
                ImageId: "",
                UpgradeImageId: "",

                Unfinished: false,
                Illustrator: null,
                SubIllustrator: new List<string>() { }
            );
            return cardConfig;
        }
    }

    [EntityLogic(typeof(SanaeStar_Card_Rain_Def))]
    [UsedImplicitly]
    public sealed class SanaeStar_Rain : Card
    {
        public override IEnumerable<BattleAction> OnDraw()
        {
            return this.EnterHandReactor();
        }

        public override IEnumerable<BattleAction> OnMove(CardZone srcZone, CardZone dstZone)
        {
            bool flag = dstZone != CardZone.Hand;
            IEnumerable<BattleAction> enumerable;
            if (flag)
            {
                enumerable = null;
            }
            else
            {
                enumerable = this.EnterHandReactor();
            }
            return enumerable;
        }

        protected override void OnEnterBattle(BattleController battle)
        {
            base.ReactBattleEvent<UnitEventArgs>(base.Battle.Player.TurnEnding, new EventSequencedReactor<UnitEventArgs>(this.OnPlayerTurnEnding));
            bool flag = base.Zone == CardZone.Hand;
            if (flag)
            {
                base.React(this.EnterHandReactor());
            }
        }

        private IEnumerable<BattleAction> EnterHandReactor()
        {
            bool flag = base.Zone != CardZone.Hand;
            if (flag)
            {
                yield break;
            }
            base.NotifyActivating();
            yield return base.HealAction(base.Value1);
            yield break;
        }

        private IEnumerable<BattleAction> OnPlayerTurnEnding(UnitEventArgs args)
        {
            bool battleShouldEnd = base.Battle.BattleShouldEnd;
            if (battleShouldEnd)
            {
                yield break;
            }
            bool flag = base.Zone == CardZone.Hand;
            if (flag)
            {
                UnitSelector selector = new UnitSelector(TargetType.AllEnemies);
                foreach (Unit unit in selector.GetUnits(base.Battle))
                {
                    base.NotifyActivating();
                    yield return new HealAction(base.Battle.Player, unit, base.Value2, HealType.Normal, 0.2f);
                }
            }
            yield break;
        }
    }
}
