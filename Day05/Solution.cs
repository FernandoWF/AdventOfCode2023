namespace AdventOfCode2023.Day05
{
    internal class Solution : ISolution
    {
        private static readonly string[] lines = File.ReadAllLines("Day05\\Input.txt");

        public static object RunPart1()
        {
            var almanac = ParseAlmanac();
            return almanac.SeedValues
                .Select(s => GetLocation(s, almanac))
                .Min();
        }

        private static Almanac ParseAlmanac()
        {
            var seeds = lines[0]["seeds: ".Length..]
                .Split(' ')
                .Select(long.Parse)
                .ToList();
            var blankLineIndexes = lines
                .Select((line, index) => (line, index))
                .Where(tuple => tuple.line == string.Empty)
                .Select(tuple => tuple.index)
                .ToList();

            var seedToSoilStartingLineIndex = blankLineIndexes[0] + 2;
            var seedToSoilEndingLineIndex = blankLineIndexes[1] - 1;
            var seedToSoilMaps = lines[seedToSoilStartingLineIndex..(seedToSoilEndingLineIndex + 1)]
                .Select(ParseMap)
                .OrderBy(m => m.SourceRangeStart)
                .ToList();

            var soilToFertilizerStartingLineIndex = blankLineIndexes[1] + 2;
            var soilToFertilizerEndingLineIndex = blankLineIndexes[2] - 1;
            var soilToFertilizerMaps = lines[soilToFertilizerStartingLineIndex..(soilToFertilizerEndingLineIndex + 1)]
                .Select(ParseMap)
                .OrderBy(m => m.SourceRangeStart)
                .ToList();

            var fertilizerToWaterStartingLineIndex = blankLineIndexes[2] + 2;
            var fertilizerToWaterEndingLineIndex = blankLineIndexes[3] - 1;
            var fertilizerToWaterMaps = lines[fertilizerToWaterStartingLineIndex..(fertilizerToWaterEndingLineIndex + 1)]
                .Select(ParseMap)
                .OrderBy(m => m.SourceRangeStart)
                .ToList();

            var waterToLightStartingLineIndex = blankLineIndexes[3] + 2;
            var waterToLightEndingLineIndex = blankLineIndexes[4] - 1;
            var waterToLightMaps = lines[waterToLightStartingLineIndex..(waterToLightEndingLineIndex + 1)]
                .Select(ParseMap)
                .OrderBy(m => m.SourceRangeStart)
                .ToList();

            var lightToTemperatureStartingLineIndex = blankLineIndexes[4] + 2;
            var lightToTemperatureEndingLineIndex = blankLineIndexes[5] - 1;
            var lightToTemperatureMaps = lines[lightToTemperatureStartingLineIndex..(lightToTemperatureEndingLineIndex + 1)]
                .Select(ParseMap)
                .OrderBy(m => m.SourceRangeStart)
                .ToList();

            var temperatureToHumidityStartingLineIndex = blankLineIndexes[5] + 2;
            var temperatureToHumidityEndingLineIndex = blankLineIndexes[6] - 1;
            var temperatureToHumidityMaps = lines[temperatureToHumidityStartingLineIndex..(temperatureToHumidityEndingLineIndex + 1)]
                .Select(ParseMap)
                .OrderBy(m => m.SourceRangeStart)
                .ToList();

            var humidityToLocationStartingLineIndex = blankLineIndexes[6] + 2;
            var humidityToLocationMaps = lines[humidityToLocationStartingLineIndex..]
                .Select(ParseMap)
                .OrderBy(m => m.SourceRangeStart)
                .ToList();

            return new Almanac
            {
                SeedValues = seeds,
                SeedToSoilMaps = seedToSoilMaps,
                SoilToFertilizerMaps = soilToFertilizerMaps,
                FertilizerToWaterMaps = fertilizerToWaterMaps,
                WaterToLightMaps = waterToLightMaps,
                LightToTemperatureMaps = lightToTemperatureMaps,
                TemperatureToHumidityMaps = temperatureToHumidityMaps,
                HumidityToLocationMaps = humidityToLocationMaps
            };

            static Map ParseMap(string line)
            {
                var values = line.Split(' ');

                return new Map(
                    SourceRangeStart: long.Parse(values[1]),
                    DestinationRangeStart: long.Parse(values[0]),
                    RangeLength: long.Parse(values[2]));
            }
        }

        private static long GetLocation(long seed, Almanac almanac)
        {
            var soil = almanac.GetSoil(seed);
            var fertilizer = almanac.GetFertilizer(soil);
            var water = almanac.GetWater(fertilizer);
            var light = almanac.GetLight(water);
            var temperature = almanac.GetTemperature(light);
            var humidity = almanac.GetHumidity(temperature);
            return almanac.GetLocation(humidity);
        }

        public static object RunPart2()
        {
            var almanac = ParseAlmanac();
            var seedRanges = new List<(long start, long length)>();

            for (var i = 0; i < almanac.SeedValues.Count; i += 2)
            {
                seedRanges.Add((almanac.SeedValues[i], almanac.SeedValues[i + 1]));
            }

            var lowestLocations = new long[seedRanges.Count];
            var calculationActions = new Action[seedRanges.Count];
            for (var i = 0; i < lowestLocations.Length; i++)
            {
                lowestLocations[i] = long.MaxValue;
                var index = i;
                calculationActions[i] = () => CalculateLocation(index, seedRanges[index].start, seedRanges[index].length, lowestLocations, almanac);
            }

            Parallel.Invoke(calculationActions);

            return lowestLocations.Min();

            static void CalculateLocation(int index, long rangeStart, long rangeLength, long[] lowestLocations, Almanac almanac)
            {
                for (var i = rangeStart; i < rangeStart + rangeLength; i++)
                {
                    var location = GetLocation(i, almanac);
                    if (location < lowestLocations[index])
                    {
                        lowestLocations[index] = location;
                    }
                }
            }
        }
    }
}
