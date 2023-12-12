using System.Diagnostics;

namespace AdventOfCode2023.Day12
{
    internal class Solution : ISolution
    {
        private enum SpringCondition
        {
            Operational,
            Damaged,
            Unknown
        }

        private record ConditionRecord(SpringCondition[] RowInfo, int[] DamagedGroupSizes);

        private static readonly string[] lines = File.ReadAllLines("Day12\\Input.txt");

        public static object RunPart1()
        {
            var possibleArrangementCounts = ParseRecords()
                .Select(GetPossibleArrangementCount)
                .ToArray();

            return possibleArrangementCounts.Sum();
        }

        private static ConditionRecord[] ParseRecords()
        {
            return lines
                .Select(l =>
                {
                    var formats = l.Split(' ');
                    var rowInfo = formats[0]
                        .Select(x => x switch
                        {
                            '.' => SpringCondition.Operational,
                            '#' => SpringCondition.Damaged,
                            '?' => SpringCondition.Unknown,
                            _ => throw new UnreachableException("There should not be any other character.")
                        })
                        .ToArray();
                    var damagedGroupSizes = formats[1]
                        .Split(',')
                        .Select(int.Parse)
                        .ToArray();

                    return new ConditionRecord(rowInfo, damagedGroupSizes);
                })
                .ToArray();
        }

        static int GetPossibleArrangementCount(ConditionRecord record)
        {
            return GenerateAllPossibleArrangements(record.RowInfo)
                .Count(a => IsArrangementValid(a, record.DamagedGroupSizes));

            static IEnumerable<SpringCondition[]> GenerateAllPossibleArrangements(SpringCondition[] arrangementWithUnknownConditions)
            {
                var unknownConditionIndexes = arrangementWithUnknownConditions
                    .Select((condition, index) => (condition, index))
                    .Where(tuple => tuple.condition == SpringCondition.Unknown)
                    .Select(tuple => tuple.index)
                    .ToArray();

                return ReplaceUnknownCondition(arrangementWithUnknownConditions, 0);

                IEnumerable<SpringCondition[]> ReplaceUnknownCondition(SpringCondition[] baseArrangement, int index)
                {
                    var indexToReplace = unknownConditionIndexes[index];

                    var arrangementReplacedWithOperational = baseArrangement.ToArray();
                    arrangementReplacedWithOperational[indexToReplace] = SpringCondition.Operational;

                    var arrangementReplacedWithDamaged = baseArrangement.ToArray();
                    arrangementReplacedWithDamaged[indexToReplace] = SpringCondition.Damaged;

                    if (index < unknownConditionIndexes!.Length - 1)
                    {
                        foreach (var arrangement in ReplaceUnknownCondition(arrangementReplacedWithOperational, index + 1))
                        {
                            yield return arrangement;
                        }

                        foreach (var arrangement in ReplaceUnknownCondition(arrangementReplacedWithDamaged, index + 1))
                        {
                            yield return arrangement;
                        }
                    }
                    else
                    {
                        yield return arrangementReplacedWithOperational;
                        yield return arrangementReplacedWithDamaged;
                    }
                }
            }

            static bool IsArrangementValid(SpringCondition[] arrangement, int[] groupSizes)
            {
                var groups = GetGroupsOf(arrangement, r => r == SpringCondition.Damaged).ToArray();

                if (groups.Length != groupSizes.Length)
                {
                    return false;
                }

                for (var i = 0; i < groupSizes.Length; i++)
                {
                    if (groupSizes[i] != groups[i].Length)
                    {
                        return false;
                    }
                }

                return true;

                static IEnumerable<T[]> GetGroupsOf<T>(T[] source, Func<T, bool> conditionToGroup)
                {
                    var list = new List<T>();

                    foreach (var item in source)
                    {
                        if (conditionToGroup(item))
                        {
                            list.Add(item);
                            continue;
                        }

                        if (list.Count > 0)
                        {
                            yield return list.ToArray();
                            list.Clear();
                        }
                    }

                    if (list.Count > 0)
                    {
                        yield return list.ToArray();
                    }
                }
            }
        }

        public static object RunPart2()
        {
            return null;
        }
    }
}
