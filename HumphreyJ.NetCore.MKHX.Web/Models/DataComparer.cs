using HumphreyJ.NetCore.MKHX.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumphreyJ.NetCore.MKHX.Web.Models
{
    public class DataComparer
    {
        public static void CardDataComparer(IEnumerable<ParsedCardData> oldData, IEnumerable<ParsedCardData> newData, out List<string> added, out List<string> deleted, out Dictionary<string, Dictionary<string, string[]>> changed)
        {

            var oldId = oldData.Select(m => m.CardId);
            var newId = newData.Select(m => m.CardId);
            added = newId.Except(oldId).Select(m => m.ToString()).ToList();
            deleted = oldId.Except(newId).Select(m => m.ToString()).ToList();

            changed = new Dictionary<string, Dictionary<string, string[]>>();
            var nList = newData.ToDictionary(m => m.CardId, m => m);
            foreach (var i in oldData)
            {
                var key = i.CardId;
                if (nList.ContainsKey(key))
                {
                    var value = new Dictionary<string, string[]>();
                    var item = nList[key];
                    {
                        if (i.CardName != item.CardName) value.Add(nameof(i.CardName), new string[] { i.CardName.ToString(), item.CardName.ToString() });
                        if (i.Cost != item.Cost) value.Add(nameof(i.Cost), new string[] { i.Cost.ToString(), item.Cost.ToString() });
                        if (i.Color != item.Color) value.Add(nameof(i.Color), new string[] { i.Color.ToString(), item.Color.ToString() });
                        if (i.Race != item.Race) value.Add(nameof(i.Race), new string[] { i.Race.ToString(), item.Race.ToString() });
                        if (i.Attack != item.Attack) value.Add(nameof(i.Attack), new string[] { i.Attack.ToString(), item.Attack.ToString() });
                        if (i.Wait != item.Wait) value.Add(nameof(i.Wait), new string[] { i.Wait.ToString(), item.Wait.ToString() });
                        if (!i.Skill.SequenceEqual(item.Skill)) value.Add(nameof(i.Skill), new string[] { string.Join("_", i.Skill), string.Join("_", item.Skill) });
                        if (!i.LockSkill1.SequenceEqual(item.LockSkill1)) value.Add(nameof(i.LockSkill1), new string[] { string.Join("_", i.LockSkill1), string.Join("_", item.LockSkill1) });
                        if (!i.LockSkill2.SequenceEqual(item.LockSkill2)) value.Add(nameof(i.LockSkill2), new string[] { string.Join("_", i.LockSkill2), string.Join("_", item.LockSkill2) });
                        if (i.ImageId != item.ImageId) value.Add(nameof(i.ImageId), new string[] { i.ImageId.ToString(), item.ImageId.ToString() });
                        if (i.FullImageId != item.FullImageId) value.Add(nameof(i.FullImageId), new string[] { i.FullImageId.ToString(), item.FullImageId.ToString() });
                        if (i.Price != item.Price) value.Add(nameof(i.Price), new string[] { i.Price.ToString(), item.Price.ToString() });
                        if (i.BaseExp != item.BaseExp) value.Add(nameof(i.BaseExp), new string[] { i.BaseExp.ToString(), item.BaseExp.ToString() });
                        if (i.Glory != item.Glory) value.Add(nameof(i.Glory), new string[] { i.Glory.ToString(), item.Glory.ToString() });
                        if (i.EvoCost != item.EvoCost) value.Add(nameof(i.EvoCost), new string[] { i.EvoCost.ToString(), item.EvoCost.ToString() });
                        if (i.RacePacket != item.RacePacket) value.Add(nameof(i.RacePacket), new string[] { i.RacePacket.ToString(), item.RacePacket.ToString() });
                        if (i.RacePacketRoll != item.RacePacketRoll) value.Add(nameof(i.RacePacketRoll), new string[] { i.RacePacketRoll.ToString(), item.RacePacketRoll.ToString() });
                        if (i.MaxInDeck != item.MaxInDeck) value.Add(nameof(i.MaxInDeck), new string[] { i.MaxInDeck.ToString(), item.MaxInDeck.ToString() });
                        if (i.CanDecompose != item.CanDecompose) value.Add(nameof(i.CanDecompose), new string[] { i.CanDecompose.ToString(), item.CanDecompose.ToString() });
                        if (i.Rank != item.Rank) value.Add(nameof(i.Rank), new string[] { i.Rank.ToString(), item.Rank.ToString() });
                        if (i.Fragment != item.Fragment) value.Add(nameof(i.Fragment), new string[] { i.Fragment.ToString(), item.Fragment.ToString() });
                        if (i.ComposePrice != item.ComposePrice) value.Add(nameof(i.ComposePrice), new string[] { i.ComposePrice.ToString(), item.ComposePrice.ToString() });
                        if (i.DecomposeGet != item.DecomposeGet) value.Add(nameof(i.DecomposeGet), new string[] { i.DecomposeGet.ToString(), item.DecomposeGet.ToString() });
                        if (i.DungeonsCard != item.DungeonsCard) value.Add(nameof(i.DungeonsCard), new string[] { i.DungeonsCard.ToString(), item.DungeonsCard.ToString() });
                        if (i.DungeonsFrag != item.DungeonsFrag) value.Add(nameof(i.DungeonsFrag), new string[] { i.DungeonsFrag.ToString(), item.DungeonsFrag.ToString() });
                        if (i.FragMaze != item.FragMaze) value.Add(nameof(i.FragMaze), new string[] { i.FragMaze.ToString(), item.FragMaze.ToString() });
                        if (i.FragRobber != item.FragRobber) value.Add(nameof(i.FragRobber), new string[] { i.FragRobber.ToString(), item.FragRobber.ToString() });
                        if (i.FragSeniorPacket != item.FragSeniorPacket) value.Add(nameof(i.FragSeniorPacket), new string[] { i.FragSeniorPacket.ToString(), item.FragSeniorPacket.ToString() });
                        if (i.FragMasterPacket != item.FragMasterPacket) value.Add(nameof(i.FragMasterPacket), new string[] { i.FragMasterPacket.ToString(), item.FragMasterPacket.ToString() });
                        if (i.FragNewYearPacket != item.FragNewYearPacket) value.Add(nameof(i.FragNewYearPacket), new string[] { i.FragNewYearPacket.ToString(), item.FragNewYearPacket.ToString() });
                        if (i.FragMagicCard != item.FragMagicCard) value.Add(nameof(i.FragMagicCard), new string[] { i.FragMagicCard.ToString(), item.FragMagicCard.ToString() });
                        if (i.FragRacePacket != item.FragRacePacket) value.Add(nameof(i.FragRacePacket), new string[] { i.FragRacePacket.ToString(), item.FragRacePacket.ToString() });
                        if (i.PriceRank != item.PriceRank) value.Add(nameof(i.PriceRank), new string[] { i.PriceRank.ToString(), item.PriceRank.ToString() });
                        if (i.ActivityPacket != item.ActivityPacket) value.Add(nameof(i.ActivityPacket), new string[] { i.ActivityPacket.ToString(), item.ActivityPacket.ToString() });
                        if (i.ActivityPacketRoll != item.ActivityPacketRoll) value.Add(nameof(i.ActivityPacketRoll), new string[] { i.ActivityPacketRoll.ToString(), item.ActivityPacketRoll.ToString() });
                        if (i.BossHelper != item.BossHelper) value.Add(nameof(i.BossHelper), new string[] { i.BossHelper.ToString(), item.BossHelper.ToString() });
                        if (i.FightRank != item.FightRank) value.Add(nameof(i.FightRank), new string[] { i.FightRank.ToString(), item.FightRank.ToString() });
                        if (i.FragmentCanUse != item.FragmentCanUse) value.Add(nameof(i.FragmentCanUse), new string[] { i.FragmentCanUse.ToString(), item.FragmentCanUse.ToString() });
                        if (i.Dust != item.Dust) value.Add(nameof(i.Dust), new string[] { i.Dust.ToString(), item.Dust.ToString() });
                        if (i.DustLevel != item.DustLevel) value.Add(nameof(i.DustLevel), new string[] { i.DustLevel.ToString(), item.DustLevel.ToString() });
                        if (i.DustNumber != item.DustNumber) value.Add(nameof(i.DustNumber), new string[] { i.DustNumber.ToString(), item.DustNumber.ToString() });
                        if (!i.HpArray.SequenceEqual(item.HpArray)) value.Add(nameof(i.HpArray), new string[] { string.Join("_", i.HpArray), string.Join("_", item.HpArray) });
                        if (!i.AttackArray.SequenceEqual(item.AttackArray)) value.Add(nameof(i.AttackArray), new string[] { string.Join("_", i.AttackArray), string.Join("_", item.AttackArray) });
                        if (!i.ExpArray.SequenceEqual(item.ExpArray)) value.Add(nameof(i.ExpArray), new string[] { string.Join("_", i.ExpArray), string.Join("_", item.ExpArray) });
                    }
                    if (value.Count > 0)
                    {
                        if (!value.ContainsKey(nameof(i.CardName)))
                        {
                            value.Add(nameof(i.CardName), new string[] { i.CardName.ToString(), item.CardName.ToString() });
                        }
                        changed.Add(key.ToString(), value);
                    }
                }
            }

            return;

        }
        public static void RuneDataComparer(IEnumerable<ParsedRuneData> oldData, IEnumerable<ParsedRuneData> newData, out List<string> added, out List<string> deleted, out Dictionary<string, Dictionary<string, string[]>> changed)
        {

            var oldId = oldData.Select(m => m.RuneId);
            var newId = newData.Select(m => m.RuneId);
            added = newId.Except(oldId).Select(m => m.ToString()).ToList();
            deleted = oldId.Except(newId).Select(m => m.ToString()).ToList();

            changed = new Dictionary<string, Dictionary<string, string[]>>();
            var nList = newData.ToDictionary(m => m.RuneId, m => m);
            foreach (var i in oldData)
            {
                var key = i.RuneId;
                if (nList.ContainsKey(key))
                {
                    var value = new Dictionary<string, string[]>();
                    var item = nList[key];
                    {
                        if (i.RuneName != item.RuneName) value.Add(nameof(i.RuneName), new string[] { i.RuneName.ToString(), item.RuneName.ToString() });
                        if (i.Property != item.Property) value.Add(nameof(i.Property), new string[] { i.Property.ToString(), item.Property.ToString() });
                        if (i.Color != item.Color) value.Add(nameof(i.Color), new string[] { i.Color.ToString(), item.Color.ToString() });
                        if (!i.LockSkill1.SequenceEqual(item.LockSkill1)) value.Add(nameof(i.LockSkill1), new string[] { string.Join("_", i.LockSkill1), string.Join("_", item.LockSkill1) });
                        if (!i.LockSkill2.SequenceEqual(item.LockSkill2)) value.Add(nameof(i.LockSkill2), new string[] { string.Join("_", i.LockSkill2), string.Join("_", item.LockSkill2) });
                        if (!i.LockSkill3.SequenceEqual(item.LockSkill3)) value.Add(nameof(i.LockSkill3), new string[] { string.Join("_", i.LockSkill3), string.Join("_", item.LockSkill3) });
                        if (!i.LockSkill4.SequenceEqual(item.LockSkill4)) value.Add(nameof(i.LockSkill4), new string[] { string.Join("_", i.LockSkill4), string.Join("_", item.LockSkill4) });
                        if (!i.LockSkill5.SequenceEqual(item.LockSkill5)) value.Add(nameof(i.LockSkill5), new string[] { string.Join("_", i.LockSkill5), string.Join("_", item.LockSkill5) });
                        if (i.Price != item.Price) value.Add(nameof(i.Price), new string[] { i.Price.ToString(), item.Price.ToString() });
                        if (i.SkillTimes != item.SkillTimes) value.Add(nameof(i.SkillTimes), new string[] { i.SkillTimes.ToString(), item.SkillTimes.ToString() });
                        if (i.Condition != item.Condition) value.Add(nameof(i.Condition), new string[] { i.Condition.ToString(), item.Condition.ToString() });
                        if (i.SkillConditionSlide != item.SkillConditionSlide) value.Add(nameof(i.SkillConditionSlide), new string[] { i.SkillConditionSlide.ToString(), item.SkillConditionSlide.ToString() });
                        if (i.SkillConditionType != item.SkillConditionType) value.Add(nameof(i.SkillConditionType), new string[] { i.SkillConditionType.ToString(), item.SkillConditionType.ToString() });
                        if (i.SkillConditionRace != item.SkillConditionRace) value.Add(nameof(i.SkillConditionRace), new string[] { i.SkillConditionRace.ToString(), item.SkillConditionRace.ToString() });
                        if (i.SkillConditionColor != item.SkillConditionColor) value.Add(nameof(i.SkillConditionColor), new string[] { i.SkillConditionColor.ToString(), item.SkillConditionColor.ToString() });
                        if (i.SkillConditionCompare != item.SkillConditionCompare) value.Add(nameof(i.SkillConditionCompare), new string[] { i.SkillConditionCompare.ToString(), item.SkillConditionCompare.ToString() });
                        if (i.SkillConditionValue != item.SkillConditionValue) value.Add(nameof(i.SkillConditionValue), new string[] { i.SkillConditionValue.ToString(), item.SkillConditionValue.ToString() });
                        if (i.ThinkGet != item.ThinkGet) value.Add(nameof(i.ThinkGet), new string[] { i.ThinkGet.ToString(), item.ThinkGet.ToString() });
                        if (i.Fragment != item.Fragment) value.Add(nameof(i.Fragment), new string[] { i.Fragment.ToString(), item.Fragment.ToString() });
                        if (i.BaseExp != item.BaseExp) value.Add(nameof(i.BaseExp), new string[] { i.BaseExp.ToString(), item.BaseExp.ToString() });
                        if (!i.ExpArray.SequenceEqual(item.ExpArray)) value.Add(nameof(i.ExpArray), new string[] { string.Join("_", i.ExpArray), string.Join("_", item.ExpArray) });
                    }
                    if (value.Count > 0)
                    {
                        if (!value.ContainsKey(nameof(i.RuneName)))
                        {
                            value.Add(nameof(i.RuneName), new string[] { i.RuneName.ToString(), item.RuneName.ToString() });
                        }
                        changed.Add(key.ToString(), value);
                    }
                }
            }

            return;

        }
        public static void SkillDataComparer(IEnumerable<ParsedSkillData> oldData, IEnumerable<ParsedSkillData> newData, out List<string> added, out List<string> deleted, out Dictionary<string, Dictionary<string, string[]>> changed)
        {

            var oldId = oldData.Select(m => m.SkillId);
            var newId = newData.Select(m => m.SkillId);
            added = newId.Except(oldId).Select(m => m.ToString()).ToList();
            deleted = oldId.Except(newId).Select(m => m.ToString()).ToList();

            changed = new Dictionary<string, Dictionary<string, string[]>>();
            var nList = newData.ToDictionary(m => m.SkillId, m => m);
            foreach (var i in oldData)
            {
                var key = i.SkillId;
                if (nList.ContainsKey(key))
                {
                    var value = new Dictionary<string, string[]>();
                    var item = nList[key];
                    {
                        if (i.Name != item.Name) value.Add(nameof(i.Name), new string[] { i.Name.ToString(), item.Name.ToString() });
                        if (!i.Type.SequenceEqual(item.Type)) value.Add(nameof(i.Type), new string[] { string.Join("_", i.Type), string.Join("_", item.Type) });
                        if (i.LanchType != item.LanchType) value.Add(nameof(i.LanchType), new string[] { i.LanchType.ToString(), item.LanchType.ToString() });
                        if (i.LanchCondition != item.LanchCondition) value.Add(nameof(i.LanchCondition), new string[] { i.LanchCondition.ToString(), item.LanchCondition.ToString() });
                        if (!i.LanchConditionValue.SequenceEqual(item.LanchConditionValue)) value.Add(nameof(i.LanchConditionValue), new string[] { string.Join("_", i.LanchConditionValue), string.Join("_", item.LanchConditionValue) });
                        if (i.AffectType.SequenceEqual(item.AffectType)) value.Add(nameof(i.AffectType), new string[] { string.Join("_", i.AffectType),string.Join("_", item.AffectType) });
                        if (!i.AffectValue.SequenceEqual(item.AffectValue)) value.Add(nameof(i.AffectValue), new string[] { string.Join("_", i.AffectValue), string.Join("_", item.AffectValue) });
                        if (!i.AffectValue2.SequenceEqual(item.AffectValue2)) value.Add(nameof(i.AffectValue2), new string[] { string.Join("_", i.AffectValue2), string.Join("_", item.AffectValue2) });
                        if (i.SkillCategory != item.SkillCategory) value.Add(nameof(i.SkillCategory), new string[] { i.SkillCategory.ToString(), item.SkillCategory.ToString() });
                        if (i.Desc != item.Desc) value.Add(nameof(i.Desc), new string[] { i.Desc.ToString(), item.Desc.ToString() });
                        if (i.DESCRIBE_NEW != item.DESCRIBE_NEW) value.Add(nameof(i.DESCRIBE_NEW), new string[] { i.DESCRIBE_NEW.ToString(), item.DESCRIBE_NEW.ToString() });
                        if (i.DESCRIBE_EXTRA != item.DESCRIBE_EXTRA) value.Add(nameof(i.DESCRIBE_EXTRA), new string[] { i.DESCRIBE_EXTRA.ToString(), item.DESCRIBE_EXTRA.ToString() });
                    }
                    if (value.Count > 0)
                    {
                        if (!value.ContainsKey(nameof(i.Name)))
                        {
                            value.Add(nameof(i.Name), new string[] { i.Name.ToString(), item.Name.ToString() });
                        }
                        changed.Add(key.ToString(), value);
                    }
                }
            }

            return;

        }
        public static void MapStageDataComparer(IEnumerable<ParsedMapStageData> oldData, IEnumerable<ParsedMapStageData> newData, out List<string> added, out List<string> deleted, out Dictionary<string, List<string>> changed)
        {

            var oldId = oldData.Select(m => m.MapStageId);
            var newId = newData.Select(m => m.MapStageId);
            added = newId.Except(oldId).Select(m => m.ToString()).ToList();
            deleted = oldId.Except(newId).Select(m => m.ToString()).ToList();

            var Changed = new Dictionary<string, List<string>>();
            throw new NotImplementedException();
            //changed = Changed;
            //return;

        }
        public static void KeywordDataComparer(IEnumerable<ParsedKeywordData> oldData, IEnumerable<ParsedKeywordData> newData, out List<string> added, out List<string> deleted, out Dictionary<string, string> changed)
        {

            var oldId = oldData.Select(m => m.key);
            var newId = newData.Select(m => m.key);
            added = newId.Except(oldId).ToList();
            deleted = oldId.Except(newId).ToList();

            var Changed = new Dictionary<string, string>();
            var nList = newData.ToDictionary(m => m.id, m => m);
            foreach (var i in oldData)
            {
                if (nList.ContainsKey(i.id))
                {
                    var item = nList[i.id];
                    if (i.des != item.des)
                    {
                        Changed.Add(i.key, i.des);
                    }
                }
            }
            changed = Changed;

            return;

        }
    }
}