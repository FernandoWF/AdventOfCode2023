namespace AdventOfCode2023.Day05
{
    internal readonly record struct Map(long SourceRangeStart, long DestinationRangeStart, long RangeLength)
    {
        public bool ContainsSource(long source)
        {
            return SourceRangeStart <= source && SourceRangeStart + RangeLength - 1 >= source;
        }
    }

    internal record class Almanac
    {
        public required IReadOnlyList<long> SeedValues { get; init; }
        public required IReadOnlyList<Map> SeedToSoilMaps { get; init; }
        public required IReadOnlyList<Map> SoilToFertilizerMaps { get; init; }
        public required IReadOnlyList<Map> FertilizerToWaterMaps { get; init; }
        public required IReadOnlyList<Map> WaterToLightMaps { get; init; }
        public required IReadOnlyList<Map> LightToTemperatureMaps { get; init; }
        public required IReadOnlyList<Map> TemperatureToHumidityMaps { get; init; }
        public required IReadOnlyList<Map> HumidityToLocationMaps { get; init; }

        public long GetSoil(long seed) => GetDestination(seed, SeedToSoilMaps);
        public long GetFertilizer(long soil) => GetDestination(soil, SoilToFertilizerMaps);
        public long GetWater(long fertilizer) => GetDestination(fertilizer, FertilizerToWaterMaps);
        public long GetLight(long water) => GetDestination(water, WaterToLightMaps);
        public long GetTemperature(long light) => GetDestination(light, LightToTemperatureMaps);
        public long GetHumidity(long temperature) => GetDestination(temperature, TemperatureToHumidityMaps);
        public long GetLocation(long humidity) => GetDestination(humidity, HumidityToLocationMaps);

        private static long GetDestination(long source, IReadOnlyList<Map> mapList)
        {
            var map = GetMap(source, mapList);
            if (map is null)
            {
                return source;
            }

            return source + map.Value.DestinationRangeStart - map.Value.SourceRangeStart;
        }

        private static Map? GetMap(long source, IReadOnlyList<Map> mapList)
        {
            foreach (var map in mapList)
            {
                if (map.ContainsSource(source))
                {
                    return map;
                }
            }

            return null;
        }
    }
}
