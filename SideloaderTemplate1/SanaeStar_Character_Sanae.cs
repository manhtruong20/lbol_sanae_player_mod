using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Units;
using LBoL.EntityLib.EnemyUnits.Character;
using LBoL.Presentation;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using LBoLEntitySideloader.Utils;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace SanaeStar
{
    public sealed class SanaeStar_Character_Sanae_Def : PlayerUnitTemplate
    {
        public static DirectorySource dir = new DirectorySource(PInfo.GUID, "");
        public override IdContainer GetId()
        {
            return nameof(SanaeStarMod);
        }

        public override LocalizationOption LoadLocalization()
        {
            return BepinexPlugin.SanaeStarPlayerUnitBatchloc.AddEntity(this);
        }

        public override PlayerImages LoadPlayerImages()
        {
            PlayerImages playerImages = new PlayerImages();
            playerImages.AutoLoad("", (s) => ResourceLoader.LoadSprite(s, dir, ppu: 100, 1, FilterMode.Bilinear, generateMipMaps: true), (s) => ResourceLoader.LoadSpriteAsync(s, BepinexPlugin.directorySource), 0, ".png", "SanaeStarImage");
            return playerImages;
        }

        public override PlayerUnitConfig MakeConfig()
        {
            PlayerUnitConfig reimuConfig = PlayerUnitConfig.FromId("Reimu").Copy();

            PlayerUnitConfig config = new PlayerUnitConfig(
            Id: "",
            ShowOrder: 2147483647,
            Order: 0,
            UnlockLevel: 1,
            ModleName: "",
            NarrativeColor: "#23EC4D",
            IsSelectable: true,
            MaxHp: 75,
            InitialMana: new LBoL.Base.ManaGroup() {
                Green = 1,
                White = 2,
                Blue = 1,
            },
            InitialMoney: 80,
            InitialPower: 0,
            UltimateSkillA: "ReimuUltR",
            UltimateSkillB: "ReimuUltR",
            ExhibitA: "KongbaiKapai",
            ExhibitB: "KongbaiKapai",
            DeckA: new List<string> { "SanaeStar_Rain" },
            DeckB: new List<string> { "SanaeStar_Rain" },
            DifficultyA: 2,
            DifficultyB: 3
            );
            return config;
        }



        [EntityLogic(typeof(SanaeStar_Character_Sanae_Def))]
        public sealed class SanaeStarMod : PlayerUnit
        {
        }
    }

    public sealed class SanaeStar_SanaeModel : UnitModelTemplate
    {
        public string model_name = nameof(Sanae);
        public override IdContainer GetId()
        {
            return new SanaeStar_Character_Sanae_Def().UniqueId;
        }
        public override LocalizationOption LoadLocalization()
        {
            return BepinexPlugin.SanaeStarModelBatchloc.AddEntity(this);
        }

        /*
        public override ModelOption LoadModelOptions()
        {
            return new ModelOption(ResourceLoader.LoadSpriteAsync("SanaeStarImageStand.png", BepinexPlugin.directorySource, ppu: 336));
        }*/

        public override ModelOption LoadModelOptions()
        {
            return new ModelOption(ResourcesHelper.LoadSpineUnitAsync(model_name));
        }


        public override UniTask<Sprite> LoadSpellSprite()
        {
            return ResourceLoader.LoadSpriteAsync("SanaeStarImageWinIcon.png", BepinexPlugin.directorySource, ppu: 336);
        }

        public override UnitModelConfig MakeConfig()
        {
            var config = UnitModelConfig.FromName(model_name).Copy();
            config.Flip = false;
            return config;
        }
    }
}
