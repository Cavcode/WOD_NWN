using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service.LanguageService;
using WOD.Game.Server.Service.StatusEffectService;
using static WOD.Game.Server.Core.NWScript.NWScript;
using SkillType = WOD.Game.Server.Service.SkillService.SkillType;

namespace WOD.Game.Server.Service
{
    public static class Language
    {
        private static Dictionary<SkillType, ITranslator> _translators = new Dictionary<SkillType, ITranslator>();
        private static readonly TranslatorGeneric _genericTranslator = new TranslatorGeneric();

        /// <summary>
        /// When the module loads, create translators for every language and store them into cache.
        /// </summary>
        [NWNEventHandler("mod_load")]
        public static void LoadTranslators()
        {
            _translators = new Dictionary<SkillType, ITranslator>
            {
                { SkillType.Mandarin, new TranslatorBothese() },
                { SkillType.Hindi, new TranslatorCatharese() },
                { SkillType.Spanish, new TranslatorCheunh() },
                { SkillType.French, new TranslatorDosh() },
                { SkillType.Arabic, new TranslatorHuttese() },
                { SkillType.Russian,  new TranslatorMandoa() },
                { SkillType.Portuguese, new TranslatorShyriiwook() },
                { SkillType.German, new TranslatorTwileki() },
                { SkillType.Dutch, new TranslatorZabraki() },
            };
        }

        public static string TranslateSnippetForListener(uint speaker, uint listener, SkillType language, string snippet)
        {
            var translator = _translators.ContainsKey(language) ? _translators[language] : _genericTranslator;
            var languageSkill = Skill.GetSkillDetails(language);

            if (GetIsPC(speaker) && !GetIsDM(speaker))
            {
                var playerId = GetObjectUUID(speaker);
                var dbSpeaker = DB.Get<Player>(playerId);

                // Get the rank and max rank for the speaker, and garble their English text based on it.
                var speakerSkillRank = dbSpeaker.Skills[language].Rank;

                if (speakerSkillRank != languageSkill.MaxRank)
                {
                    var garbledChance = 100 - (int)((speakerSkillRank / (float)languageSkill.MaxRank) * 100);

                    var split = snippet.Split(' ');
                    for (var i = 0; i < split.Length; ++i)
                    {
                        if (Random.Next(100) <= garbledChance)
                        {
                            split[i] = new string(split[i].ToCharArray().OrderBy(s => (Random.Next(2) % 2) == 0).ToArray());
                        }
                    }

                    snippet = split.Aggregate((a, b) => a + " " + b);
                }
            }

            if (!GetIsPC(listener) || GetIsDM(listener))
            {
                // Short circuit for a DM or NPC - they will always understand the text.
                return snippet;
            }

            // Let's grab the max rank for the listener skill, and then we roll for a successful translate based on that.
            var listenerId = GetObjectUUID(listener);
            var dbListener = DB.Get<Player>(listenerId);
            var rank = dbListener.Skills[language].Rank;
            var maxRank = languageSkill.MaxRank;

            // Check for the Comprehend Speech concentration ability.
            var grantSenseXP = false;
            var statusEffectBonus = 0;
            if (StatusEffect.HasStatusEffect(listener, StatusEffectType.ComprehendSpeech1))
                statusEffectBonus = 5;
            else if (StatusEffect.HasStatusEffect(listener, StatusEffectType.ComprehendSpeech2))
                statusEffectBonus = 10;
            else if (StatusEffect.HasStatusEffect(listener, StatusEffectType.ComprehendSpeech3))
                statusEffectBonus = 15;
            else if (StatusEffect.HasStatusEffect(listener, StatusEffectType.ComprehendSpeech4))
                statusEffectBonus = 20;

            if (statusEffectBonus > 0)
            {
                rank += statusEffectBonus;
                grantSenseXP = true;
            }

            // Ensure we don't go over the maximum.
            if (rank > maxRank)
                rank = maxRank;

            if (rank == maxRank || speaker == listener)
            {
                // Guaranteed success - return original.
                return snippet;
            }

            var textAsForeignLanguage = translator.Translate(snippet);

            if (rank != 0)
            {
                var englishChance = (int)((rank / (float)maxRank) * 100);

                var originalSplit = snippet.Split(' ');
                var foreignSplit = textAsForeignLanguage.Split(' ');

                var endResult = new StringBuilder();

                // WARNING: We're making the assumption that originalSplit.Length == foreignSplit.Length.
                // If this assumption changes, the below logic needs to change too.
                for (var i = 0; i < originalSplit.Length; ++i)
                {
                    if (Random.Next(100) <= englishChance)
                    {
                        endResult.Append(originalSplit[i]);
                    }
                    else
                    {
                        endResult.Append(foreignSplit[i]);
                    }

                    endResult.Append(" ");
                }

                textAsForeignLanguage = endResult.ToString();
            }

            var now = DateTime.Now.Ticks;
            var lastSkillUpLow = GetLocalInt(listener, "LAST_LANGUAGE_SKILL_INCREASE_LOW");
            var lastSkillUpHigh = GetLocalInt(listener, "LAST_LANGUAGE_SKILL_INCREASE_HIGH");
            long lastSkillUp = lastSkillUpHigh;
            lastSkillUp = (lastSkillUp << 32) | (uint)lastSkillUpLow;
            var differenceInSeconds = (now - lastSkillUp) / 10000000;

            if (differenceInSeconds / 60 >= 2)
            {
                // Reward exp towards the language - we scale this with character count, maxing at 50 exp for 150 characters.
                // A bonus is given if listener's Social modifier is greater than zero.
                var amount = Math.Max(10, Math.Min(150, snippet.Length) / 3);
                var socialModifier = GetAbilityModifier(AbilityType.Social, listener);
                if (socialModifier > 0)
                {
                    amount += socialModifier * 10;
                }

                Skill.GiveSkillXP(listener, language, amount);



                SetLocalInt(listener, "LAST_LANGUAGE_SKILL_INCREASE_LOW", (int)(now & 0xFFFFFFFF));
                SetLocalInt(listener, "LAST_LANGUAGE_SKILL_INCREASE_HIGH", (int)((now >> 32) & 0xFFFFFFFF));
            }

            return textAsForeignLanguage;
        }

        public static int GetColor(SkillType language)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;

            switch (language)
            {
                case SkillType.Mandarin: r = 132; g = 56; b = 18; break;
                case SkillType.Hindi: r = 235; g = 235; b = 199; break;
                case SkillType.Spanish: r = 82; g = 143; b = 174; break;
                case SkillType.French: r = 166; g = 181; b = 73; break;
                case SkillType.Arabic: r = 255; g = 215; b = 0; break;
                case SkillType.Russian: r = 65; g = 105; b = 225; break;
                case SkillType.Portuguese: r = 255; g = 102; b = 102; break;
                case SkillType.German: r = 77; g = 230; b = 215; break;
                case SkillType.Dutch: r = 128; g = 128; b = 192; break;
            }

            return r << 24 | g << 16 | b << 8;
        }

        public static string GetName(SkillType language)
        {
            switch (language)
            {
                case SkillType.Mandarin: return "Mandarin";
                case SkillType.Hindi: return "Hindi";
                case SkillType.Spanish: return "Spanish";
                case SkillType.French: return "French";
                case SkillType.Arabic: return "Arabic";
                case SkillType.Russian: return "Russian";
                case SkillType.Portuguese: return "Portuguese";
                case SkillType.German: return "German";
                case SkillType.Dutch: return "Dutch";
            }

            return "English";
        }

        public static SkillType GetActiveLanguage(uint obj)
        {
            var ret = GetLocalInt(obj, "ACTIVE_LANGUAGE");

            if (ret == 0)
            {
                return SkillType.English;
            }

            return (SkillType)ret;
        }

        public static void SetActiveLanguage(uint obj, SkillType language)
        {
            if (language == SkillType.English)
            {
                DeleteLocalInt(obj, "ACTIVE_LANGUAGE");
            }
            else
            {
                SetLocalInt(obj, "ACTIVE_LANGUAGE", (int)language);
            }
        }

        private static IEnumerable<LanguageCommand> _languages;

        public static IEnumerable<LanguageCommand> Languages
        {
            get
            {
                if (_languages == null)
                {
                    var languages = new List<LanguageCommand>
                    {
                        new LanguageCommand("English", SkillType.English, new [] { "english" }),
                        new LanguageCommand("Mandarin", SkillType.Mandarin, new[] {"mandarin"}),
                        new LanguageCommand("Hindi", SkillType.Hindi, new []{"hindi"}),
                        new LanguageCommand("Spanish", SkillType.Spanish, new []{"spanish"}),
                        new LanguageCommand("French", SkillType.French, new []{"french"}),
                        new LanguageCommand("Arabic", SkillType.Arabic, new []{"arabic"}),
                        new LanguageCommand("Russian", SkillType.Russian, new []{"russian"}),
                        new LanguageCommand("Portuguese", SkillType.Portuguese, new []{ "portuguese"}),
                        new LanguageCommand("German", SkillType.German, new []{ "german"}),
                        new LanguageCommand("Dutch", SkillType.Dutch, new []{ "dutch"}),
                    };

                    _languages = languages;
                }

                return _languages;
            }
        }
    }
}
