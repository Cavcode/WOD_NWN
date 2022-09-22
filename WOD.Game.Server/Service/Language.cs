using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Service.LanguageService;
using WOD.Game.Server.Service.StatusEffectService;
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
                { SkillType.Hindi, new TranslatorHindi() },
                { SkillType.Arabic, new TranslatorArabic() },
                { SkillType.Spanish, new TranslatorSpanish() },
                { SkillType.French, new TranslatorFrench() },
                { SkillType.Portuguese,  new TranslatorPortuguese() },
                { SkillType.Finnish, new TranslatorFinnish() },
                { SkillType.Romanian, new TranslatorRomanian() },
                { SkillType.Greek, new TranslatorGreek() },
                { SkillType.German, new TranslatorGerman() },
                { SkillType.Swedish, new TranslatorSwedish() },
                { SkillType.Dutch, new TranslatorDutch() },
                { SkillType.Italian, new TranslatorItalian() },
                { SkillType.Russian, new TranslatorRussian() },
                { SkillType.Czech, new TranslatorCzech() },
                { SkillType.Danish, new TranslatorDanish() },
                { SkillType.Polish, new TranslatorPolish() },
                { SkillType.Mandarin, new TranslatorMandarin()}
            };
        }

        public static string TranslateSnippetForListener(uint speaker, uint listener, SkillType language, string snippet)
        {
            var translator = _translators.ContainsKey(language) ? _translators[language] : _genericTranslator;
            var languageSkill = Skill.GetSkillDetails(language);

            if (GetIsPC(speaker))
            {
                var playerId = GetObjectUUID(speaker);
                var dbSpeaker = DB.Get<Player>(playerId);
                // Get the rank and max rank for the speaker, and garble their English text based on it.
                var speakerSkillRank = dbSpeaker == null ? 
                    languageSkill.MaxRank : 
                    dbSpeaker.Skills[language].Rank;

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
            var rank = dbListener == null ? 
                languageSkill.MaxRank : 
                dbListener.Skills[language].Rank;
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
                case SkillType.Arabic: r = 132; g = 56; b = 18; break;
                case SkillType.Czech: r = 235; g = 235; b = 199; break;
                case SkillType.Danish: r = 82; g = 143; b = 174; break;
                case SkillType.Dutch: r = 166; g = 181; b = 73; break;
                case SkillType.Finnish: r = 192; g = 192; b = 192; break;
                case SkillType.French: r = 162; g = 74; b = 10; break;
                case SkillType.German: r = 162; g = 162; b = 0; break;
                case SkillType.Greek: r = 255; g = 215; b = 0; break;
                case SkillType.Hindi: r = 82; g = 255; b = 82; break;
                case SkillType.Italian: r = 149; g = 125; b = 86; break;
                case SkillType.Mandarin: r = 82; g = 82; b = 255; break;
                case SkillType.Polish: r = 65; g = 105; b = 225; break;
                case SkillType.Portuguese: r = 255; g = 102; b = 102; break;
                case SkillType.Romanian: r = 77; g = 230; b = 215; break;
                case SkillType.Russian: r = 128; g = 128; b = 192; break;
                case SkillType.Spanish: r = 255; g = 193; b = 233; break;
                case SkillType.Swedish: r = 255; g = 80; b = 200; break;
            }

            return r << 24 | g << 16 | b << 8;
        }

        public static string GetName(SkillType language)
        {
            switch (language)
            {
                case SkillType.Arabic: return "Bothese";
                case SkillType.Czech: return "Catharese";
                case SkillType.Danish: return "Cheunh";
                case SkillType.Dutch: return "Dosh";
                case SkillType.Finnish: return "Droidspeak";
                case SkillType.French: return "Huttese";
                case SkillType.German: return "KelDor";
                case SkillType.Greek: return "Mandoa";
                case SkillType.Hindi: return "Rodese";
                case SkillType.Italian: return "Shyriiwook";
                case SkillType.Mandarin: return "Togruti";
                case SkillType.Polish: return "Twi'leki";
                case SkillType.Portuguese: return "Zabraki";
                case SkillType.Romanian: return "Mirialan";
                case SkillType.Russian: return "Mon Calamarian";
                case SkillType.Spanish: return "Ugnaught";
            }

            return "Basic";
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
                        new LanguageCommand("Arabic", SkillType.Arabic, new[] {"arabic"}),
                        new LanguageCommand("Czech", SkillType.Czech, new []{"czech"}),
                        new LanguageCommand("Danish", SkillType.Danish, new []{"danish"}),
                        new LanguageCommand("Dutch", SkillType.Dutch, new []{"dutch"}),
                        new LanguageCommand("Finnish", SkillType.Finnish, new []{"finnish"}),
                        new LanguageCommand("French", SkillType.French, new []{"french"}),
                        new LanguageCommand("German", SkillType.German, new []{"german"}),
                        new LanguageCommand("Greek", SkillType.Greek, new []{"greek"}),
                        new LanguageCommand("Romanian", SkillType.Romanian, new []{"romanian"}),
                        new LanguageCommand("Russian", SkillType.Russian, new []{ "russian"}),
                        new LanguageCommand("Hindi", SkillType.Hindi, new []{ "hindi"}),
                        new LanguageCommand("Italian", SkillType.Italian, new []{"italian"}),
                        new LanguageCommand("Mandarin", SkillType.Mandarin, new []{"mandarin"}),
                        new LanguageCommand("Polish", SkillType.Polish, new []{ "polish"}),
                        new LanguageCommand("Spanish", SkillType.Spanish, new []{"spanish"}),
                        new LanguageCommand("Portuguese", SkillType.Portuguese, new []{ "portuguese"}),
                        new LanguageCommand("Swedish", SkillType.Swedish, new []{"swedish"}),
                    };

                    _languages = languages;
                }

                return _languages;
            }
        }
    }
}
